using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Views
{
    public class TextboxViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private static string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
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
                case "Username":
                    {
                        if(string.IsNullOrEmpty(username))
                            validationMessage = "Username can't be null or empty.";
                        break;
                    }
            }
            return validationMessage;
        }
    }
}
