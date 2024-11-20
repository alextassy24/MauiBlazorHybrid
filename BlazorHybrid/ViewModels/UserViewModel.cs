using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.Models;
using BlazorHybrid.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class UserViewModel(IUserRepository userRepository, NavigationManager navigationManager)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly NavigationManager _navigationManager = navigationManager;
        public UserDto? SelectedUser { get; private set; }
        public ObservableCollection<UserDto> Users { get; private set; } = [];

        public void LoadUser(Guid id)
        {
            SelectedUser = _userRepository.GetById(id);
        }

        public void RemoveUser(Guid userId)
        {
            _userRepository.Delete(userId);

            var userToRemove = Users.FirstOrDefault(u => u.Id == userId);
            if (userToRemove != null)
            {
                Users.Remove(userToRemove);
            }
        }

        public void NavigateToEdit(Guid id)
        {
            _navigationManager.NavigateTo($"/edit-user/{id}");
        }

        public void NavigateBack()
        {
            _navigationManager.NavigateTo("/users");
        }
    }
}
