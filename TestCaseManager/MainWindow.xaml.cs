using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestCaseManager.Core;

namespace TestCaseManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private const string LogoutButtonContent = "Logout";
        private const string LoginButtonContent = "Login";

        private readonly Link TestCasesLink = new Link() { DisplayName = "Test Cases", Source = new Uri(@"Views/MainWindowProjectAndTestCases.xaml", UriKind.Relative) };
        private readonly Link TestRunLink = new Link() { DisplayName = "Test Runs", Source = new Uri(@"Views/MainWindowTestRuns.xaml", UriKind.Relative) };
        private readonly Link AdministrationLink = new Link() { DisplayName = "Administrate users", Source = new Uri(@"Views/Administration/UserManagementPage.xaml", UriKind.Relative) };
        private readonly Link EmptyLink = new Link() { DisplayName = string.Empty };

        public MainWindow()
        {
            this.InitializeComponent();
            this.RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Event for logged user
            AuthenticationManager.Instance().ValidAuthenticationEvent += (s, e) =>
            {
                var titleLink = this.TitleLinks.Where(link => link.DisplayName.Equals(LoginButtonContent)).First();
                titleLink.DisplayName = LogoutButtonContent;

                this.TitleLinks.Insert(0, TestCasesLink);
                this.TitleLinks.Insert(1, TestRunLink);

                if (AuthenticationManager.Instance().IsUserAnAdmin())
                {
                    this.TitleLinks.Insert(2, AdministrationLink);
                    this.TitleLinks.Insert(3, EmptyLink);
                }
                else
                    this.TitleLinks.Insert(2, EmptyLink);
            };

            AuthenticationManager.Instance().LogoutEvent += (s, e) =>
            {
                var titleLink = this.TitleLinks.Where(link => link.DisplayName.Equals(LogoutButtonContent)).First();
                titleLink.DisplayName = LoginButtonContent;

                this.TitleLinks.Remove(TestCasesLink);
                this.TitleLinks.Remove(TestRunLink);
                this.TitleLinks.Remove(AdministrationLink);
                this.TitleLinks.Remove(EmptyLink);
            };
        }

        private void Link_MouseClicked(object sender, MouseButtonEventArgs e)
        {
            var link = e.OriginalSource as FrameworkElement as Button;
            if (link != null && link.Content != null)
            {
                if (link.Content.ToString().Equals(LogoutButtonContent))
                {
                    AuthenticationManager.Instance().RemoveAuthenticatedUser();
                }
            }
        }
    }
}
