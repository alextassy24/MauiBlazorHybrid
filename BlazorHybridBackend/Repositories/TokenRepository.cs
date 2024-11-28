using BlazorHybrid.Shared.Models;
using BlazorHybridBackend.Data;
using BlazorHybridBackend.Interfaces.Repositories;
using BlazorHybridBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorHybridBackend.Repositories
{
    public class TokenRepository(ApplicationDbContext context) : ITokenRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.ActivationTokens.AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(ActivationToken token, User user)
        {
            await _context.ActivationTokens.AddAsync(token);
            user.ActivationToken = token;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ActivationToken token)
        {
            _context.ActivationTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveActivationTokenAsync(ActivationToken token)
        {
            _context.ActivationTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }
}
