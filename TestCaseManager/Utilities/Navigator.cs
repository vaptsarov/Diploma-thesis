using System.Windows;

namespace TestCaseManager.Utilities
{
    public class Navigator : BaseNavigator
    {
        /// <summary>
        ///     The instance
        /// </summary>
        private static Navigator _instance;

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static Navigator Instance
        {
            get
            {
                if (_instance == null) _instance = new Navigator();

                return _instance;
            }
        }

        public void NavigateMainWindowProjectAndTestCases(FrameworkElement source)
        {
            var url = "/Views/MainWindowProjectAndTestCases.xaml";
            Navigate(source, url);
        }

        public void NavigateAuthorization(FrameworkElement source)
        {
            var url = "/Views/AuthorizationHome.xaml";
            Navigate(source, url);
        }
    }
}