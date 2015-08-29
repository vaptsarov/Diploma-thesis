using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.AuthenticatePoint;

namespace TestCaseManager.Core
{
    public class AuthenticationManager : IAuthenticate, IAdmin
    {
        private static bool IsAdmin;
        private static bool IsAuthenticated;
        private static AuthenticationManager instance = null;  
        private static Object lockedObj = new Object();

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

        public bool IsUserAuthenticated()
        {
            return IsAuthenticated;
        }

        public void RegisterUserForAuthentication(string username, string password)
        {
            // Call DB to check if the user is there with admin role - set IsAuthenticated.
        }

        public bool IsAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
