using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Services
{
    public class AuthService(IHttpService httpService) : IAuthService
    {
        private readonly IHttpService _httpService = httpService;

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                await _httpService.PostAsync("api/auth/register", request);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
