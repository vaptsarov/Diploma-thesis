using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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

            var isUserCorrect = this.IsUserCredentialsCorrect();
            if (isUserCorrect == false)
                this.SetVisibilityForInvalidCredentialsLabel();
            else
                this.Visibility = Visibility.Hidden;
        }

        private void RegisterUsernameValidation()
        {
            this.textboxViewModel = new TextboxViewModel();
            this.textboxViewModel.Username = this.Username.Text;
            this.DataContext = this.textboxViewModel;
        }

        private bool IsUserCredentialsCorrect()
        {
            using (var db = new TestcaseManagerDB())
            {
                ApplicationUser user = new ApplicationUser() { Username = "Prdophian", Password = "1" };
                db.ApplicationUsers.Add(user);
                db.SaveChanges();
            }

            return true;
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
