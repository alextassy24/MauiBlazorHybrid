using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class EditUserViewModel(IUserRepository userRepository, NavigationManager navigationManager)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly NavigationManager _navigationManager = navigationManager;

        public User? SelectedUser { get; private set; }
        public ObservableCollection<User> Users { get; private set; } = [];

        public void LoadUser(Guid id)
        {
            SelectedUser = _userRepository.GetById(id);
        }

        public void SaveUser()
        {
            if (SelectedUser != null)
            {
                _userRepository.Update(SelectedUser);
            }
        }

        public void NavigateBack()
        {
            _navigationManager.NavigateTo("/users");
        }
    }
}
