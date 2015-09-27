﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

namespace TestCaseManager.Core.ApplicationUsers
{
    public class AppUserManager
    {
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
                user = db.ApplicationUsers.Where(usr => usr.Username == username &&
                    usr.Password == unsecuredPasswordString).FirstOrDefault();
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
                user.Password = password;

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
