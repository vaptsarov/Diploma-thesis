using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Views
{
    public class PasswordViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private SecureString securePassword;
        public SecureString SecurePassword
        {
            get

            { return securePassword; }
            set
            {
                if (value != securePassword)
                {
                    securePassword = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public string Error
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Will be called for each and every property when ever it's value is changed
        /// </summary>
        /// <param name="columnName">Name of the property whose value is changed</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string properyName)
        {
            string validationMessage = string.Empty;
            switch (properyName)
            {
                case "Password":
                    {
                        if (string.IsNullOrEmpty(securePassword.ToString()))
                            validationMessage = "Username can't be null or empty.";
                        break;
                    }
            }
            return validationMessage;
        }
    }
}
