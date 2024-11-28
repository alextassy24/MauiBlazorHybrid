using BlazorHybridBackend.Models;
using BlazorHybrid.Shared.Models;

namespace BlazorHybridBackend.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task<bool> ExistsAsync(string email);
        Task AddAsync(ActivationToken token, User user);
        Task UpdateAsync(ActivationToken token);
        Task RemoveActivationTokenAsync(ActivationToken token);
    }
}
