using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.CryptoService;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

namespace TestCaseManager.Core.ApplicationUsers
{
    public class UserManager
    {
        private readonly AppConfigManager appConfigManager;
        private readonly X509Certificate2CryptoService cryptoService;

        public UserManager()
        {
            this.appConfigManager = new AppConfigManager();

            X509Certificate2FromStoreResolver certificateResolver = new X509Certificate2FromStoreResolver(appConfigManager.GetCertificateThumbprint);
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

        public ApplicationUser CreateUser(string username, string password, bool isAdmin = false, bool isReadOnly = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username or password was empty or null.");

            ApplicationUser user = new ApplicationUser();
            using (var db = new TestcaseManagerDB())
            {
                user.Username = username;
             
                string encryptedValue = cryptoService.Encrypt(password);
                user.Password = encryptedValue;

                if(isReadOnly)
                    user.IsReadOnly = true;

                if(isAdmin)
                    user.IsAdmin = true;

                db.ApplicationUsers.Add(user);
                db.SaveChanges();
            }

            return user;
        }
    }
}
