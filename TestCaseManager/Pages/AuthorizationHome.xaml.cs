using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TestCaseManager.Core;
using TestCaseManager.Core.ApplicationUsers;
using TestCaseManager.DB;
using TestCaseManager.Views;

namespace TestCaseManager.Pages
{
    /// <summary>
    /// Interaction logic for AuthorizationHome.xaml
    /// </summary>
    public partial class AuthorizationPage : UserControl
    {
        private TextboxViewModel textboxViewModel = null;

        public AuthorizationPage()
        {        
            this.InitializeComponent();
            this.SetVisibilityForInvalidCredentialsLabel(false);
        }

        private void AuthorizeCredentials_Button(object sender, RoutedEventArgs e)
        {
            this.RegisterUsernameValidation();
            this.SetVisibilityForInvalidCredentialsLabel(false);

            // Verification if both of the properties are null or empty
            if((string.IsNullOrEmpty(this.Username.Text) && this.Password.SecurePassword != null) == false)
            {
                // Check whether the credentials are correct or not
                var isUserCorrect = this.IsUserCredentialsCorrect(this.Username.Text, this.Password.SecurePassword);
                if (isUserCorrect == false)
                    this.SetVisibilityForInvalidCredentialsLabel();
                else
                {
                    AuthenticationManager.Instance().RegisterUserForAuthentication(this.Username.Text, this.Password.SecurePassword);
                    this.Visibility = Visibility.Hidden;
                }
            }
        }

        private void RegisterUsernameValidation()
        {
            this.textboxViewModel = new TextboxViewModel();
            this.textboxViewModel.Username = this.Username.Text;
            this.DataContext = this.textboxViewModel;
        }

        private bool IsUserCredentialsCorrect(string username, SecureString password)
        {
            UserManager userManager = new UserManager();

            bool isUserCredentialsCorrect = true;
            try
            {
                userManager.GetUser(username, password);
            }
            catch (ArgumentNullException)
            {
                isUserCredentialsCorrect = false;
            }
            catch (ArgumentException)
            {
                isUserCredentialsCorrect = false;
            }

            return isUserCredentialsCorrect;
        }

        private void SetVisibilityForInvalidCredentialsLabel(bool isVisible = true)
        {
            if (isVisible)
                this.InvalidCredentials.Visibility = Visibility.Visible;
            else
                this.InvalidCredentials.Visibility = Visibility.Hidden;
        }
    }
}
