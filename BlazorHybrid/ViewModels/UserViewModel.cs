using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;

namespace BlazorHybrid.ViewModels
{
    public class UserViewModel
    {
        private readonly IUserRepository _userRepository;

        public ObservableCollection<User> Users { get; private set; } = [];

        public UserViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _userRepository.GetAll();
            Users = [.. users];
        }
        
        public void AddUser()
        {
            Users.Add(new User
            {
                FirstName = "New",
                LastName = "User",
                Email = "newuser@example.com"
            });
        }
    }
}
