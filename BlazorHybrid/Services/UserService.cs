using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Shared.Models;
using BlazorHybrid.Shared.DTO;
using System.Net.Http.Json;

namespace BlazorHybrid.Services
{
    public class UserService(IHttpService httpService) : IUserService
    {
        private readonly IHttpService _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));

        public async Task<UserDto> GetByIdAsync(object id)
        {
            if (id is not Guid guidId || guidId == Guid.Empty)
                throw new ArgumentException("ID must be a non-empty GUID", nameof(id));

            return await _httpService.GetAsync<UserDto>($"api/users/{guidId}");
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _httpService.GetAsync<List<UserDto>>("api/users");
        }

        public async Task AddAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _httpService.PostAsync("api/users", user);
        }

        public async Task UpdateAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _httpService.PutAsync($"api/users/{user.Id}", user);
        }

        public async Task DeleteAsync(object id)
        {
            if (id is not Guid guidId || guidId == Guid.Empty)
                throw new ArgumentException("ID must be a non-empty GUID", nameof(id));

            await _httpService.DeleteAsync($"api/users/{guidId}");
        }
    }
}
