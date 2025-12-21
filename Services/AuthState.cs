using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JournalApp.Services;


namespace JournalApp.Services
{
    public class AuthState
    {
        public bool IsLoggedIn { get; private set; }
        public int UserId { get; private set; }
        public string Username { get; private set; } = "";

        public event Action? OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void Login(int userId, string username)
        {
            IsLoggedIn = true;
            UserId = userId;
            Username = username;
            NotifyStateChanged();
        }

        public void Logout()
        {
            IsLoggedIn = false;
            UserId = 0;
            Username = "";
            NotifyStateChanged();
        }
    }
}
