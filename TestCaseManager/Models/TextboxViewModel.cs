using System.ComponentModel;

namespace TestCaseManager.Views
{
    public class TextboxViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private static string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text");
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
                case "Text":
                    {
                        if(string.IsNullOrEmpty(text))
                            validationMessage = "Value can't be null or empty.";
                        break;
                    }
            }
            return validationMessage;
        }
    }
}
