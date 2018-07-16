using System;
using System.Security;
using TestCaseManager.Core.Managers;

namespace TestCaseManager.Core.AuthenticatePoint
{
    public class AuthenticationManager : IAuthenticate, IAdmin
    {
        private static bool _isAnAdmin;
        private static bool _isReadOnly;
        private static string _currentUser;

        private static AuthenticationManager _instance;
        private static readonly object LockedObj = new object();

        public string GetCurrentUsername => _currentUser;

        public bool IsUserAnAdmin()
        {
            return _isAnAdmin;
        }

        public bool IsUserReadOnly()
        {
            return _isReadOnly;
        }

        public void RegisterUserForAuthentication(string username, SecureString password)
        {
            var userManager = new UserManager();
            var currentApplicationUser = userManager.GetUser(username, password);

            _isAnAdmin = currentApplicationUser.IsAdmin;
            _isReadOnly = currentApplicationUser.IsReadOnly;
            _currentUser = currentApplicationUser.Username;

            ValidAuthenticationEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ValidAuthenticationEvent;
        public event EventHandler LogoutEvent;

        public static AuthenticationManager Instance()
        {
            if (_instance != null) return _instance;
            lock (LockedObj)
            {
                if (_instance == null) _instance = new AuthenticationManager();
            }

            return _instance;
        }

        public void RemoveAuthenticatedUser()
        {
            _isAnAdmin = false;
            _isReadOnly = true;
            _currentUser = null;

            LogoutEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}