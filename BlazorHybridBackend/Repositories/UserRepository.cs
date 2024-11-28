using BlazorHybrid.Shared.Models;
using BlazorHybridBackend.Data;
using BlazorHybridBackend.Interfaces.Repositories;
using BlazorHybridBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorHybridBackend.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context
                .Users.Include(u => u.ActivationToken)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context
                .Users.Include(u => u.ActivationToken)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveActivationTokenAsync(ActivationToken token)
        {
            _context.ActivationTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }
}
