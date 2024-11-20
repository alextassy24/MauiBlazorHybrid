using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Interfaces.Repos
{
    public interface IUserRepository : IRepository<UserDto>
    {
        UserDto GetByEmail(string email);
    }
}
