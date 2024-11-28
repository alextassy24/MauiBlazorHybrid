using BlazorHybrid.Shared.DTO;
using BlazorHybrid.Shared.Models;
using BlazorHybridBackend.Interfaces.Repositories;
using BlazorHybridBackend.Interfaces.Services;
using BlazorHybridBackend.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;

namespace BlazorHybridBackend.Services
{
    public class UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null || user.PasswordHash == null)
            {
                return false;
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            return verificationResult == PasswordVerificationResult.Success;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<string> GenerateConfirmationTokenAsync(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email))
                return string.Empty;

            var random = new Random();
            var token = new ActivationToken
            {
                UserId = user.Id,
                Email = user.Email,
                Token = random.Next(100000, 999999).ToString(),
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(15),
            };

            user.ActivationToken = token;
            await UpdateUserAsync(user);

            return token.Token;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.ActivationToken is null)
                return false;

            if (
                user.ActivationToken.Token == token
                && DateTime.UtcNow <= user.ActivationToken.ExpirationDate
            )
            {
                user.EmailConfirmed = true;
                await _userRepository.RemoveActivationTokenAsync(user.ActivationToken);
                user.ActivationToken = null;
                await UpdateUserAsync(user);
                return true;
            }

            return false;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                return new RegisterResponseDto
                {
                    IsSuccess = false,
                    Message = "Email is already registered.",
                };
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth.ToUniversalTime(),
                IsVerified = false,
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

            await _userRepository.AddAsync(user);

            var token = new ActivationToken
            {
                UserId = user.Id,
                Token = new Random().Next(100000, 999999).ToString(),
                Email = request.Email,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(60),
            };

            await _tokenRepository.AddAsync(token, user);

            return new RegisterResponseDto { IsSuccess = true, VerificationCode = token.Token };
        }

        public async Task<VerificationResponseDto> VerifyAccountAsync(
            string email,
            string verificationCode
        )
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return new VerificationResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found.",
                };
            }

            if (user.ActivationToken.Token != verificationCode)
            {
                return new VerificationResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid verification code.",
                };
            }

            user.IsVerified = true;
            user.ActivationToken = null;
            await _userRepository.UpdateAsync(user);

            return new VerificationResponseDto
            {
                IsSuccess = true,
                Message = "Account verified successfully.",
            };
        }
    }
}
