using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.Models;
using BlazorHybrid.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class UsersViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly NavigationManager _navigationManager;
        public UserDto SelectedUser { get; set; } = new UserDto();
        public ObservableCollection<UserDto> Users { get; set; } = [];

        public UsersViewModel(IUserRepository userRepository, NavigationManager navigationManager)
        {
            _userRepository = userRepository;
            _navigationManager = navigationManager;
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _userRepository.GetAll();
            Users = [.. users];
        }

        public void LoadUser(Guid id)
        {
            SelectedUser = _userRepository.GetById(id);
        }

        public void AddUser()
        {
            Users.Add(
                new UserDto
                {
                    FirstName = "New",
                    LastName = "User",
                    Email = "newuser@example.com",
                }
            );
        }

        public void RemoveUser(string userId)
        {
            _userRepository.Delete(userId);

            var userToRemove = Users.FirstOrDefault(u => u.Id == userId);
            if (userToRemove != null)
            {
                Users.Remove(userToRemove);
            }
        }

        public void GoToUserPage(string id)
        {
            _navigationManager.NavigateTo($"/user/{id}");
        }

        public void NavigateToEdit(string id)
        {
            _navigationManager.NavigateTo($"/edit-user/{id}");
        }

        public void NavigateBack()
        {
            _navigationManager.NavigateTo("/users");
        }
    }
}
