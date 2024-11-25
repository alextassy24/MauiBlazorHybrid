using BlazorHybridBackend.Models;
using BlazorHybrid.Shared.DTO;

namespace BlazorHybridBackend.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<VerifyResponseDto> VerifyAccountAsync(string email, string verificationCode);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(User user);
        Task<string> GenerateConfirmationTokenAsync(User user);
        Task<bool> ConfirmEmailAsync(string email, string token);
    }
}