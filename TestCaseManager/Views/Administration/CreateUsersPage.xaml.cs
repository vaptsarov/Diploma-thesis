namespace TestCaseManager.Views.Administration
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Core.Managers;
    using Utilities.StringUtility;

    /// <summary>
    ///     Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : UserControl
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            var manager = new UserManager();
            var userExists = manager.CheckUsernameExists(Username.Text);
            if (string.IsNullOrWhiteSpace(Username.Text) == false &&
                userExists == false &&
                string.IsNullOrWhiteSpace(Password.SecurePassword.ConvertToUnsecureString()) == false)
            {
                manager.CreateUser(Username.Text, Password.SecurePassword, IsAdminCheckBox.IsChecked.Value);
                MessageLabel.Content = "Successful creation of the application user.";
                MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(0, 204, 0));

                ClearFields();
            }
            else if (userExists)
            {
                MessageLabel.Content = "The application user already exists.";
                MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                ClearFields();
            }
        }

        private void ClearFields()
        {
            Username.Clear();
            Password.Clear();
            IsAdminCheckBox.IsChecked = false;
        }
    }
}