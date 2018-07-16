using System.ComponentModel;
using System.Security;

namespace TestCaseManager.Models
{
    public class PasswordViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            get => _securePassword;
            set
            {
                if (value != _securePassword)
                {
                    _securePassword = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Error => string.Empty;

        /// <summary>
        ///     Will be called for each and every property when ever it's value is changed
        /// </summary>
        /// <param name="columnName">Name of the property whose value is changed</param>
        /// <returns></returns>
        public string this[string columnName] => Validate(columnName);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string Validate(string properyName)
        {
            var validationMessage = string.Empty;
            switch (properyName)
            {
                case "Password":
                {
                    if (string.IsNullOrEmpty(_securePassword.ToString()))
                        validationMessage = "Username can't be null or empty.";
                    break;
                }
            }

            return validationMessage;
        }
    }
}