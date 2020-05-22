namespace TestCaseManager.Core.AuthenticatePoint
{
    using System.Security;

    public interface IAuthenticate
    {
        bool IsUserReadOnly();
        void RegisterUserForAuthentication(string username, SecureString password);
    }
}