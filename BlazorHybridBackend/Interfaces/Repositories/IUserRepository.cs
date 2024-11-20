using BlazorHybridBackend.Models;
using BlazorHybrid.Shared.Models;

namespace BlazorHybridBackend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveActivationTokenAsync(ActivationToken token);
    }
}
