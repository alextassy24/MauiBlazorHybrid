using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequestDto registerRequest);
    }
}