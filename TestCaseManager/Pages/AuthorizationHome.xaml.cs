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
            InitializeComponent();
        }

        private void AuthorizeCredentials_Button(object sender, RoutedEventArgs e)
        {
            textboxViewModel = new TextboxViewModel();
            textboxViewModel.Username = this.Username.Text;
            DataContext = textboxViewModel;

            this.ValidatePassword();
        }

        private void ValidatePassword()
        {
            using (var db = new TestcaseManagerDB())
            {
                ApplicationUser user = new ApplicationUser() { Username = "Prdophian", Password = "1" };
                db.ApplicationUsers.Add(user);
                db.SaveChanges();
            }
        }
    }
}
