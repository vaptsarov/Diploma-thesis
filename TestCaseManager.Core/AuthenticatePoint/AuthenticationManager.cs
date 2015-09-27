using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.ApplicationUsers;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.DB;

namespace TestCaseManager.Core
{
    public class AuthenticationManager : IAuthenticate, IAdmin
    {
        private static bool IsAnAdmin;
        private static bool IsReadOnly;
        private static AuthenticationManager instance = null;  
        private static Object lockedObj = new Object();

        public event EventHandler Authenticated;

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
            AppUserManager userManager = new AppUserManager();
            var currentApplicationUser = userManager.GetUser(username, password);

            AuthenticationManager.IsAnAdmin = currentApplicationUser.IsAdmin;
            AuthenticationManager.IsReadOnly = currentApplicationUser.IsReadOnly;

            if (this.Authenticated != null)
                this.Authenticated(this, EventArgs.Empty);
        }

        public bool IsAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
