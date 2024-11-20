using BlazorHybrid.Shared.DTO;
using BlazorHybrid.Shared.Models;
using BlazorHybridBackend.Interfaces.Repositories;
using BlazorHybridBackend.Interfaces.Services;
using BlazorHybridBackend.Models;
using Org.BouncyCastle.Crypto.Generators;

namespace BlazorHybridBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
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

            var verificationCode = new Random().Next(100000, 999999).ToString();

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                IsVerified = false,
            };

            await _userRepository.AddAsync(user);

            return new RegisterResponseDto
            {
                IsSuccess = true,
                VerificationCode = verificationCode,
            };
        }

        public async Task<VerifyResponseDto> VerifyAccountAsync(
            string email,
            string verificationCode
        )
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                return new VerifyResponseDto { IsSuccess = false, Message = "User not found." };
            }

            if (user.ActivationToken.Token != verificationCode)
            {
                return new VerifyResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid verification code.",
                };
            }

            // Mark user as verified
            user.IsVerified = true;
            user.ActivationToken = null; // Clear the verification code
            await _userRepository.UpdateAsync(user);

            return new VerifyResponseDto
            {
                IsSuccess = true,
                Message = "Account verified successfully.",
            };
        }
    }
}
