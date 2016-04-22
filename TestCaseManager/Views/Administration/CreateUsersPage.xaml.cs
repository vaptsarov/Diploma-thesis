using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCaseManager.Core.Managers;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views.Administration
{
    /// <summary>
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : UserControl
    {
        public CreateUserPage()
        {
            this.InitializeComponent();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            UserManager manager = new UserManager();
            bool userExists = manager.CheckUsernameExists(this.Username.Text);
            if (string.IsNullOrWhiteSpace(this.Username.Text) == false &&
                userExists == false &&
                string.IsNullOrWhiteSpace(this.Password.SecurePassword.ConvertToUnsecureString()) == false)
            {
                manager.CreateUser(this.Username.Text, this.Password.SecurePassword, this.IsAdminCheckBox.IsChecked.Value);
                this.MessageLabel.Content = "Successfuly created the application user.";
                this.MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(0, 204, 0));

                this.ClearFields();
            }
            else if (userExists)
            {
                this.MessageLabel.Content = "The application user already exists.";
                this.MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                this.ClearFields();
            }
        }

        private void ClearFields()
        {
            this.Username.Clear();
            this.Password.Clear();
            this.IsAdminCheckBox.IsChecked = false;
        }
    }
}
