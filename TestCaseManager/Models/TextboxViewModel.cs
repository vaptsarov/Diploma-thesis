using System.ComponentModel;

namespace TestCaseManager.Models
{
    public class TextboxViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private static string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("Text");
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
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string Validate(string properyName)
        {
            var validationMessage = string.Empty;
            switch (properyName)
            {
                case "Text":
                {
                    if (string.IsNullOrEmpty(_text))
                        validationMessage = "Value can't be null or empty.";
                    break;
                }
            }

            return validationMessage;
        }
    }
}