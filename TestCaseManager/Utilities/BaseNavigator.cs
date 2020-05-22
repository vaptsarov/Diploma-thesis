namespace TestCaseManager.Utilities
{
    using System;
    using System.Windows;
    using FirstFloor.ModernUI.Windows.Navigation;

    /// <summary>
    ///     Contains methods which navigate to different views with option to set different parameters
    /// </summary>
    public class BaseNavigator
    {
        /// <summary>
        ///     Navigates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="url">The URL.</param>
        public void Navigate(FrameworkElement source, string url)
        {
            var navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Relative), source);
        }

        /// <summary>
        ///     Navigates the back.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateBack(FrameworkElement source)
        {
            const string url = "cmd://browseback";
            var navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Absolute), source, "_self");
        }
    }
}