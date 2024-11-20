using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.DTO;

namespace BlazorHybrid.Interfaces.Services
{
    public interface IUserService : IAsyncRepository<UserDto> { }
}
