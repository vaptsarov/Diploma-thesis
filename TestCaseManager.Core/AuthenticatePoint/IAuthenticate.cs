using System.Security;

namespace TestCaseManager.Core.AuthenticatePoint
{
    public interface IAuthenticate
    {
        bool IsUserReadOnly();
        void RegisterUserForAuthentication(string username, SecureString password);
    }
}