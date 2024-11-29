using System;
using System.Linq;

namespace BlazorHybrid.Shared.DTO
{
    public class UserState
    {
        public UserDto? LoggedInUser { get; private set; }
        public bool IsLoggedIn => LoggedInUser != null;
        public bool IsTrainer => LoggedInUser?.IsTrainer == true;
        public string Email { get; private set; } = string.Empty;
        public event Action? OnChange;
        public bool IsModalOpen {get;set;} = false;
        public string AuthToken { get; private set; } = string.Empty;

        public void SetEmail(string email)
        {
            Email = email;
            NotifyStateChanged();
        }

        public void ClearEmail()
        {
            Email = string.Empty;
            NotifyStateChanged();
        }

        public void SetAuthToken(string token)
        {
            AuthToken = token;
            NotifyStateChanged();
        }

        public void ClearAuthToken()
        {
            AuthToken = string.Empty;
            NotifyStateChanged();
        }

        public void SetLoggedInUser(UserDto user)
        {
            LoggedInUser = user;
            NotifyStateChanged();
        }

        public void Logout()
        {
            LoggedInUser = null;
            AuthToken = string.Empty;
            Email = string.Empty;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        public void OpenModal()
        {
            IsModalOpen = true;
            NotifyStateChanged();
        }
        public void CloseModal()
        {
            IsModalOpen = false;
            NotifyStateChanged();
        }
    }
}
