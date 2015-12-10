using System.Windows;

namespace TestCaseManager.Utilities
{
    public class Navigator : BaseNavigator
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static Navigator instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Navigator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Navigator();
                }

                return instance;
            }
        }

        public void NavigateMainWindowProjectAndTestCases(FrameworkElement source)
        {
            string url = "/Views/MainWindowProjectAndTestCases.xaml";
            this.Navigate(source, url);
        }

        public void NavigateAuthorization(FrameworkElement source)
        {
            string url = "/Views/AuthorizationHome.xaml";
            this.Navigate(source, url);
        }
    }
}
