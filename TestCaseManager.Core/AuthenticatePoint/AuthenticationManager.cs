using System;
using System.Security;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.Core.Managers;

namespace TestCaseManager.Core
{
    public class AuthenticationManager : IAuthenticate, IAdmin
    {
        private static bool IsAnAdmin;
        private static bool IsReadOnly;
        private static string CurrentUser;

        private static AuthenticationManager instance = null;
        private static Object lockedObj = new Object();

        public event EventHandler Authenticated;

        public string GetCurrentUsername
        {
            get { return CurrentUser; }
        }

        public static AuthenticationManager Instance()
        {
            if (instance == null)
            {
                lock (lockedObj)
                {
                    if (instance == null)
                    {
                        instance = new AuthenticationManager();
                    }
                }
            }

            return instance;
        }

        public bool IsUserReadOnly()
        {
            return IsReadOnly;
        }

        public void RegisterUserForAuthentication(string username, SecureString password)
        {
            UserManager userManager = new UserManager();
            var currentApplicationUser = userManager.GetUser(username, password);

            AuthenticationManager.IsAnAdmin = currentApplicationUser.IsAdmin;
            AuthenticationManager.IsReadOnly = currentApplicationUser.IsReadOnly;
            AuthenticationManager.CurrentUser = currentApplicationUser.Username;

            if (this.Authenticated != null)
                this.Authenticated(this, EventArgs.Empty);
        }

        public bool IsAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
