using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Services
{
    public class AuthService(IHttpService httpService, UserState userState) : IAuthService
    {
        private readonly IHttpService _httpService = httpService;
        private readonly UserState _userState = userState;

        public async Task<string> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                Console.WriteLine($"POSTing to: {_httpService.BaseAddress}api/Auth/register");
                await _httpService.PostAsync("api/Auth/register", request);
                return null; // No error
            }
            catch (HttpRequestException httpEx)
            {
                // Extract HTTP-specific error details
                return $"HTTP Error: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // General error fallback
                return $"An unexpected error occurred: {ex.Message}";
            }
        }

        public async Task<(bool IsSuccess, string Message, UserDto? User)> LoginAsync(
            LoginRequestDto loginRequest
        )
        {
            try
            {
                // Send login request to the backend and get the response
                var response = await _httpService.PostAsync<LoginRequestDto, LoginResponseDto>(
                    "api/Auth/login",
                    loginRequest
                );

                // Update the user state with the returned user and token
                _userState.SetAuthToken(response.Token);
                _userState.SetLoggedInUser(response.User);

                return (true, "Login successful", response.User);
            }
            catch (HttpRequestException httpEx)
            {
                return (false, $"HTTP Error: {httpEx.Message}", null);
            }
            catch (Exception ex)
            {
                return (false, $"An unexpected error occurred: {ex.Message}", null);
            }
        }

        public async Task LogoutAsync()
        {
            _userState.ClearAuthToken();
            _userState.Logout();
        }
    }
}