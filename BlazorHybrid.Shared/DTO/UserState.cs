using System;

namespace BlazorHybrid.Shared.DTO
{
    public class UserState
    {
        public UserDto? LoggedInUser { get; private set; }
        public bool IsLoggedIn => LoggedInUser != null;
        public event Action? OnChange;
        public string AuthToken { get; private set; } = string.Empty;

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
            LoggedInUser = null; // Explicitly clear the user
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}