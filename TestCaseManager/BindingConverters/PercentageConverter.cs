namespace TestCaseManager.BindingConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        private static PercentageConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new PercentageConverter());
        }

        private string StripRoundCharacters(object value)
        {
            return value.ToString().Replace("\"", string.Empty);
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedValue = double.Parse(StripRoundCharacters(value), NumberStyles.AllowDecimalPoint,
                                     NumberFormatInfo.InvariantInfo) *
                                 double.Parse(StripRoundCharacters(parameter), NumberStyles.AllowDecimalPoint,
                                     NumberFormatInfo.InvariantInfo);
            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}