using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using TestCaseManager.Core.CryptoService;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

namespace TestCaseManager.Core.Managers
{
    public class UserManager
    {
        private readonly AppConfigManager appConfigManager;
        private readonly X509Certificate2CryptoService cryptoService;

        public UserManager()
        {
            this.appConfigManager = new AppConfigManager();

            X509Certificate2FromStoreResolver certificateResolver = 
                new X509Certificate2FromStoreResolver(appConfigManager.GetCertificateThumbprint);

            cryptoService = new X509Certificate2CryptoService(certificateResolver);
        }

        public ApplicationUser GetUser(int userId)
        {
            if(userId < 0)
                throw new ArgumentException("User ID must be a positive number.");

            ApplicationUser user = null;
            using (var db = new TestcaseManagerDB())
            {
                user = db.ApplicationUsers.Where(usr => usr.UserId == userId).FirstOrDefault();
            }

            if (user == null)
                throw new ArgumentNullException(
                    string.Format("User with Id:{0} , was not found.", userId));

            return user;
        }

        public ICollection<ApplicationUser> GetAll()
        {
            ICollection<ApplicationUser> users = new Collection<ApplicationUser>();
            using (var db = new TestcaseManagerDB())
            {
                users = db.ApplicationUsers.ToList();
            }

            return users;
        }

        public ApplicationUser GetUser(string username, SecureString password)
        {
            string unsecuredPasswordString = password.ConvertToUnsecureString();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(unsecuredPasswordString))
                throw new ArgumentException("Username or password was empty or null.");

            ApplicationUser user = null;
            using (var db = new TestcaseManagerDB())
            {
                var users = db.ApplicationUsers.Where(usr => usr.Username == username).ToList();

                foreach (var usr in users)
                {
                    string decryptedPassword = cryptoService.Decrypt(usr.Password);
                    if (decryptedPassword == unsecuredPasswordString)
                    {
                        user = usr;
                        break;
                    }
                }
            }

            if (user == null)
                throw new ArgumentNullException("User with the provided credentials was not found.");

            return user;
        }

        public ApplicationUser CreateUser(string username, SecureString password, bool isAdmin = false, bool isReadOnly = false)
        {
            string unsecuredPasswordString = password.ConvertToUnsecureString();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(unsecuredPasswordString))
                return null;

            ApplicationUser user = new ApplicationUser();
            using (var db = new TestcaseManagerDB())
            {
                user.Username = username;
                string encryptedValue = cryptoService.Encrypt(unsecuredPasswordString);
                user.Password = encryptedValue;

                if(isReadOnly)
                    user.IsReadOnly = true;

                if(isAdmin)
                    user.IsAdmin = true;

                user.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername;
                user.CreatedOn = DateTime.UtcNow;

                db.ApplicationUsers.Add(user);
                db.SaveChanges();
            }

            return user;
        }

        public ApplicationUser UpdateUser(int id, string username, SecureString password, bool isAdmin = false, bool isReadOnly = false)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            ApplicationUser user;
            using (var db = new TestcaseManagerDB())
            {
                user = db.ApplicationUsers.Where(u => u.UserId.Equals(id)).FirstOrDefault();
                if (user != null)
                {
                    user.Username = username;

                    string unsecuredPasswordString = password.ConvertToUnsecureString();
                    if (string.IsNullOrWhiteSpace(unsecuredPasswordString) == false)
                    {
                        string encryptedValue = cryptoService.Encrypt(unsecuredPasswordString);
                        user.Password = encryptedValue;
                    }

                    user.IsAdmin = isAdmin;
                    user.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername;

                    db.SaveChanges();
                }
            }

            return user;
        }

        public void DeleteUser(int id)
        {
            using (var db = new TestcaseManagerDB())
            {
                var user = db.ApplicationUsers.Where(u => u.UserId.Equals(id)).FirstOrDefault();
                if (user != null)
                {
                    db.ApplicationUsers.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public bool CheckUsernameExists(string username)
        {
            bool userExists = false;
            using (var db = new TestcaseManagerDB())
            {
                userExists = db.ApplicationUsers.Any(user => user.Username.Equals(username));
            }

            return userExists;
        }
    }
}
