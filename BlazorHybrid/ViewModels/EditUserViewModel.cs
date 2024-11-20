using System.Collections.ObjectModel;
using BlazorHybrid.Interfaces.Repos;
using BlazorHybrid.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.ViewModels
{
    public class EditUserViewModel(IUserRepository userRepository, NavigationManager navigationManager)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly NavigationManager _navigationManager = navigationManager;

        public UserDto SelectedUser { get; private set; }
        public ObservableCollection<UserDto> Users { get; private set; } = [];

        public void LoadUser(Guid id)
        {
            SelectedUser = _userRepository.GetById(id);
        }

        public void SaveUser()
        {
            if (SelectedUser is not null)
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
