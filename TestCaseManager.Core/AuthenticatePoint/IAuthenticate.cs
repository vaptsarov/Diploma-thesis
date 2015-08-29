using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.AuthenticatePoint
{
    public interface IAuthenticate
    {
        bool IsUserAuthenticated();
        void RegisterUserForAuthentication(string username, string password);
    }
}
