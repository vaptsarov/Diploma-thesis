namespace TestCaseManager.Utilities
{
    using System.Windows;

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
        public static Navigator Instance => _instance ?? (_instance = new Navigator());

        public void NavigateMainWindowProjectAndTestCases(FrameworkElement source)
        {
            const string url = "/Views/MainWindowProjectAndTestCases.xaml";
            Navigate(source, url);
        }

        public void NavigateAuthorization(FrameworkElement source)
        {
            const string url = "/Views/AuthorizationHome.xaml";
            Navigate(source, url);
        }
    }
}