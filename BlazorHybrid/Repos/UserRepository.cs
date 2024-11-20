using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.DTO;
using BlazorHybrid.Shared.Models;

namespace BlazorHybrid.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserDto> _users = [];
        public UserRepository()
        {
            _users =
            [
                new UserDto { FirstName = "Ana", LastName = "Banana"},
                new UserDto { FirstName = "Tanță", LastName = "Prestanță"},
                new UserDto { FirstName = "Tanase", LastName = "Alexandru"},
                new UserDto { FirstName = "Gruia", LastName = "Cristian"},
                new UserDto { FirstName = "Sirbu", LastName = "Gabriel"},
            ];
            foreach(var user in _users)
            {
                user.Id = Guid.NewGuid();
                user.Email = $"{user.FirstName.ToLower()}.{user.LastName.ToLower()}@example.com";
            }
        }
        public UserDto GetById(object id) => _users.FirstOrDefault(u => u.Id == (Guid)id) ?? new UserDto();
        public List<UserDto> GetAll() => _users;
        public void Add(UserDto user) => _users.Add(user);
        public void Update(UserDto user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1) _users[index] = user;
        }
        public void Delete(object id) => _users.RemoveAll(u => u.Id == (Guid)id);
        public UserDto GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email) ?? new UserDto();
    }
}