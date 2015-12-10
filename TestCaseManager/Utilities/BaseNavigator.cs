﻿using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Windows;

namespace TestCaseManager.Utilities
{
    /// <summary>
    /// Contains methods which navigate to different views with option to set different parameters
    /// </summary>
    public class BaseNavigator
    {
        /// <summary>
        /// Navigates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="url">The URL.</param>
        public void Navigate(FrameworkElement source, string url)
        {
            DefaultLinkNavigator navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Relative), source, null);
        }

        /// <summary>
        /// Navigates the back.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateBack(FrameworkElement source)
        {
            string url = "cmd://browseback";
            DefaultLinkNavigator navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Absolute), source, "_self");
        }
    }
}
