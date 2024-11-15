using BlazorHybrid.Models;

namespace BlazorHybrid.Interfaces.Repos
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
