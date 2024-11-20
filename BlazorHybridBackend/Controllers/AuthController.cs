using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazorHybrid.Shared.DTO;
using BlazorHybridBackend.Interfaces.Services;
using BlazorHybridBackend.Models;
using BlazorHybridBackend.Services;
using BlazorHybridBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlazorHybridBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        IEmailService emailService,
        TokenUtils tokenUtils,
        IUserService userService
    ) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly TokenUtils _tokenUtils = tokenUtils;
        private readonly IUserService _userService = userService;

        // 1. Register Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(registerRequest);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            await _emailService.SendEmailAsync(
                registerRequest.Email,
                "Verify Your Email",
                $"Your verification code is: {result.VerificationCode}"
            );

            return Ok(
                new
                {
                    Message = "Registration successful. Check your email for the verification code.",
                }
            );
        }

        // 2. Verify Account Endpoint
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAccount([FromBody] VerifyRequestDto verifyRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.VerifyAccountAsync(
                verifyRequest.Email,
                verifyRequest.VerificationCode
            );

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(new { Message = "Account verified successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("send-confirmation-email")]
        public async Task<IActionResult> SendConfirmationEmail([FromBody] string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return BadRequest("User not found.");

            var token = await _userService.GenerateConfirmationTokenAsync(user);
            await _emailService.SendConfirmationEmail(user.Email, token);

            return Ok("Confirmation email sent.");
        }

        [HttpPost("resend-confirmation-token")]
        public async Task<IActionResult> ResendToken([FromBody] string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return BadRequest("User not found.");

            var token = await _userService.GenerateConfirmationTokenAsync(user);
            await _emailService.SendConfirmationEmail(user.Email, token);

            return Ok("Confirmation token resent.");
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel request)
        {
            var result = await _userService.ConfirmEmailAsync(request.Email, request.Token);

            if (!result)
                return BadRequest("Invalid token or email confirmation failed.");

            return Ok("Email confirmed successfully.");
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
