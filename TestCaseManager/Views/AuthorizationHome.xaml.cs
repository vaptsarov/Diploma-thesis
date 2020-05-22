namespace TestCaseManager.Views
{
    using System;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using Core.AuthenticatePoint;
    using Core.Managers;
    using Models;
    using Utilities;

    /// <summary>
    ///     Interaction logic for AuthorizationHome.xaml
    /// </summary>
    public partial class AuthorizationPage : UserControl
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            RegisterEvents();
            SetVisibilityForInvalidCredentialsLabel(false);
        }

        private void RegisterEvents()
        {
            // Event for logged user
            AuthenticationManager.SingletonInstance().ValidAuthenticationEvent += (s, e) =>
            {
                Visibility = Visibility.Hidden;
                Username.Clear();
                Password.Clear();

                Navigator.Instance.NavigateMainWindowProjectAndTestCases(this);
            };

            AuthenticationManager.SingletonInstance().LogoutEvent += (s, e) => { Visibility = Visibility.Visible; };
        }

        private void AuthorizeCredentials_Button(object sender, RoutedEventArgs e)
        {
            RegisterUsernameValidation();
            SetVisibilityForInvalidCredentialsLabel(false);

            // Verification if both of the properties are null or empty
            if (string.IsNullOrEmpty(Username.Text))
                return;

            // Check whether the credentials are correct or not
            var isUserCorrect = IsUserCredentialsCorrect(Username.Text, Password.SecurePassword);
            if (isUserCorrect == false)
                SetVisibilityForInvalidCredentialsLabel();
            else
                AuthenticationManager.SingletonInstance()
                    .RegisterUserForAuthentication(Username.Text, Password.SecurePassword);
        }

        private void RegisterUsernameValidation()
        {
            DataContext = new TextboxViewModel
            {
                Text = Username.Text
            };
        }

        private static bool IsUserCredentialsCorrect(string username, SecureString password)
        {
            try
            {
                new UserManager().GetUser(username, password);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private void SetVisibilityForInvalidCredentialsLabel(bool isVisible = true)
        {
            InvalidCredentials.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}