namespace TestCaseManager.Core.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using AuthenticatePoint;
    using CryptoService;
    using CryptoService.CertificateRelated;
    using DB;
    using Utilities;
    using Utilities.StringUtility;

    public class UserManager
    {
        private readonly X509Certificate2CryptoService _cryptoService;

        public UserManager()
        {
            var appConfigManager = new AppConfigManager();

            var certificateResolver =
                new X509Certificate2FromStoreResolver(appConfigManager.GetCertificateThumbprint);

            _cryptoService = new X509Certificate2CryptoService(certificateResolver);
        }

        public ApplicationUser GetUser(int userId)
        {
            if (userId < 0)
                throw new ArgumentException("User ID must be a positive number.");

            ApplicationUser user;
            using (var db = new TestcaseManagerDB())
            {
                user = db.ApplicationUsers.FirstOrDefault(usr => usr.UserId == userId);
            }

            if (user == null)
                throw new ArgumentNullException(
                    $"User with Id:{userId} , was not found.");

            return user;
        }

        public ICollection<ApplicationUser> GetAll()
        {
            ICollection<ApplicationUser> users;
            using (var db = new TestcaseManagerDB())
            {
                users = db.ApplicationUsers.ToList();
            }

            return users;
        }

        public ApplicationUser GetUser(string username, SecureString password)
        {
            var unsecuredPasswordString = password.ConvertToUnsecureString();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(unsecuredPasswordString))
                throw new ArgumentException("Username or password was empty or null.");

            ApplicationUser user = null;
            using (var db = new TestcaseManagerDB())
            {
                var users = db.ApplicationUsers.Where(usr => usr.Username == username).ToList();
                foreach (var usr in users)
                {
                    var decryptedPassword = _cryptoService.Decrypt(usr.Password);
                    if (decryptedPassword == unsecuredPasswordString)
                    {
                        user = usr;
                        break;
                    }
                }
            }

            if (user == null)
                throw new ArgumentNullException($"User with the provided credentials was not found.");

            return user;
        }

        public ApplicationUser CreateUser(string username, SecureString password, bool isAdmin = false,
            bool isReadOnly = false)
        {
            var unsecuredPasswordString = password.ConvertToUnsecureString();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(unsecuredPasswordString))
                return null;

            var user = new ApplicationUser();
            using (var db = new TestcaseManagerDB())
            {
                user.Username = username;
                var encryptedValue = _cryptoService.Encrypt(unsecuredPasswordString);
                user.Password = encryptedValue;

                if (isReadOnly)
                    user.IsReadOnly = true;

                if (isAdmin)
                    user.IsAdmin = true;

                user.CreatedBy = "SelfAdmin";
                user.CreatedOn = DateTime.UtcNow;

                db.ApplicationUsers.Add(user);
                db.SaveChanges();
            }

            return user;
        }

        public ApplicationUser UpdateUser(int id, string username, SecureString password, bool isAdmin = false,
            bool isReadOnly = false)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            ApplicationUser user;
            using (var db = new TestcaseManagerDB())
            {
                user = db.ApplicationUsers.FirstOrDefault(u => u.UserId.Equals(id));
                if (user != null)
                {
                    user.Username = username;

                    var unsecuredPasswordString = password.ConvertToUnsecureString();
                    if (string.IsNullOrWhiteSpace(unsecuredPasswordString) == false)
                    {
                        var encryptedValue = _cryptoService.Encrypt(unsecuredPasswordString);
                        user.Password = encryptedValue;
                    }

                    user.IsAdmin = isAdmin;
                    user.UpdatedBy = AuthenticationManager.SingletonInstance().GetCurrentUsername;

                    db.SaveChanges();
                }
            }

            return user;
        }

        public void DeleteUser(int id)
        {
            using (var db = new TestcaseManagerDB())
            {
                var user = db.ApplicationUsers.FirstOrDefault(u => u.UserId.Equals(id));
                if (user != null)
                {
                    db.ApplicationUsers.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public bool CheckUsernameExists(string username)
        {
            using (var db = new TestcaseManagerDB())
            {
                return db.ApplicationUsers.Any(user => user.Username.Equals(username));
            }
        }
    }
}