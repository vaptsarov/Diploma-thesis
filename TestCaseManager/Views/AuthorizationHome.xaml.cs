using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.Core.Managers;
using TestCaseManager.Models;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views
{
    /// <summary>
    ///     Interaction logic for AuthorizationHome.xaml
    /// </summary>
    public partial class AuthorizationPage : UserControl
    {
        private TextboxViewModel _textboxViewModel;

        public AuthorizationPage()
        {
            InitializeComponent();
            RegisterEvents();
            SetVisibilityForInvalidCredentialsLabel(false);
        }

        private void RegisterEvents()
        {
            // Event for logged user
            AuthenticationManager.Instance().ValidAuthenticationEvent += (s, e) =>
            {
                Visibility = Visibility.Hidden;
                Username.Clear();
                Password.Clear();

                Navigator.Instance.NavigateMainWindowProjectAndTestCases(this);
            };

            AuthenticationManager.Instance().LogoutEvent += (s, e) => { Visibility = Visibility.Visible; };
        }

        private void AuthorizeCredentials_Button(object sender, RoutedEventArgs e)
        {
            RegisterUsernameValidation();
            SetVisibilityForInvalidCredentialsLabel(false);

            // Verification if both of the properties are null or empty
            if ((string.IsNullOrEmpty(Username.Text)) == false)
            {
                // Check whether the credentials are correct or not
                var isUserCorrect = IsUserCredentialsCorrect(Username.Text, Password.SecurePassword);
                if (isUserCorrect == false)
                    SetVisibilityForInvalidCredentialsLabel();
                else
                    AuthenticationManager.Instance()
                        .RegisterUserForAuthentication(Username.Text, Password.SecurePassword);
            }
        }

        private void RegisterUsernameValidation()
        {
            _textboxViewModel = new TextboxViewModel
            {
                Text = Username.Text
            };
            DataContext = _textboxViewModel;
        }

        private bool IsUserCredentialsCorrect(string username, SecureString password)
        {
            var userManager = new UserManager();
            var isUserCredentialsCorrect = true;
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
            InvalidCredentials.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}