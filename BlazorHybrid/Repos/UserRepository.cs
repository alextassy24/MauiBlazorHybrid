using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = [];
        public UserRepository()
        {
            _users =
            [
                new User { FirstName = "John", LastName = "Doe"},
                new User { FirstName = "Jane", LastName = "Doe"},
                new User { FirstName = "Tanase", LastName = "Alexandru"},
                new User { FirstName = "Gruia", LastName = "Cristian"},
                new User { FirstName = "Sirbu", LastName = "Gabriel"},
            ];
            foreach(var user in _users)
            {
                user.Id = Guid.NewGuid();
                user.Email = $"{user.FirstName.ToLower()}.{user.LastName.ToLower()}@example.com";
            }
        }
        public User GetById(object id) => _users.FirstOrDefault(u => u.Id == (Guid)id) ?? new User();
        public List<User> GetAll() => _users;
        public void Add(User user) => _users.Add(user);
        public void Update(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1) _users[index] = user;
        }
        public void Delete(object id) => _users.RemoveAll(u => u.Id == (Guid)id);
        public User GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email) ?? new User();
    }
}