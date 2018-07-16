using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using TestCaseManager.Core.AuthenticatePoint;

namespace TestCaseManager
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private const string LogoutButtonContent = "Logout";
        private const string LoginButtonContent = "Login";

        private readonly Link _administrationLink = new Link
        {
            DisplayName = "Administrate users",
            Source = new Uri(@"Views/Administration/UserManagementPage.xaml", UriKind.Relative)
        };

        private readonly Link _emptyLink = new Link {DisplayName = string.Empty};

        private readonly Link _testCasesLink = new Link
        {
            DisplayName = "Test Cases",
            Source = new Uri(@"Views/MainWindowProjectAndTestCases.xaml", UriKind.Relative)
        };

        private readonly Link _testRunLink = new Link
        {
            DisplayName = "Test Runs",
            Source = new Uri(@"Views/MainWindowTestRuns.xaml", UriKind.Relative)
        };

        public MainWindow()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Event for logged user
            AuthenticationManager.Instance().ValidAuthenticationEvent += (s, e) =>
            {
                var titleLink = TitleLinks.First(link => link.DisplayName.Equals(LoginButtonContent));
                titleLink.DisplayName = LogoutButtonContent;

                TitleLinks.Insert(0, _testCasesLink);
                TitleLinks.Insert(1, _testRunLink);

                if (AuthenticationManager.Instance().IsUserAnAdmin())
                {
                    TitleLinks.Insert(2, _administrationLink);
                    TitleLinks.Insert(3, _emptyLink);
                }
                else
                {
                    TitleLinks.Insert(2, _emptyLink);
                }
            };

            AuthenticationManager.Instance().LogoutEvent += (s, e) =>
            {
                var titleLink = TitleLinks.First(link => link.DisplayName.Equals(LogoutButtonContent));
                titleLink.DisplayName = LoginButtonContent;

                TitleLinks.Remove(_testCasesLink);
                TitleLinks.Remove(_testRunLink);
                TitleLinks.Remove(_administrationLink);
                TitleLinks.Remove(_emptyLink);
            };
        }

        private void Link_MouseClicked(object sender, MouseButtonEventArgs e)
        {
            if (!((e.OriginalSource as FrameworkElement) is Button link) || link.Content == null)
                return;

            if (link.Content.ToString().Equals(LogoutButtonContent))
                AuthenticationManager.Instance().RemoveAuthenticatedUser();
        }
    }
}