using BlazorHybrid.Interfaces.Services;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        public User GetById(object id) => _userRepository.GetById((Guid)id);
        public List<User> GetAll() => _userRepository.GetAll();
        public void Add(User user) => _userRepository.Add(user);
        public void Update(User user) => _userRepository.Update(user);
        public void Delete(object id) => _userRepository.Delete((Guid)id);
    }
}