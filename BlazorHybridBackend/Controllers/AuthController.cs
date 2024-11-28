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
        UserManager<User> userManager,
        IConfiguration configuration,
        IEmailService emailService,
        IUserService userService,
        ITokenService tokenService
    ) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

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

            return Ok(result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAccount(
            [FromBody] VerificationRequestDto verifyRequest
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.VerifyAccountAsync(
                verifyRequest.Email,
                verifyRequest.Code
            );

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
                return NotFound();

            if (!await _userService.CheckPasswordAsync(loginRequest.Email, loginRequest.Password))
                return Unauthorized();

            if(!user.IsVerified)
                return Forbid();

            return Ok(
                new LoginResponseDto
                {
                    Token = _tokenService.GenerateJwtToken(user),
                    User = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsTrainer = user.IsTrainer
                    },
                }
            );
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
    }
}
