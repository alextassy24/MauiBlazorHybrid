using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto registerRequest);
        Task<(bool IsSuccess, string Message, UserDto? User)> LoginAsync(LoginRequestDto loginRequest);
        Task LogoutAsync();
    }
}