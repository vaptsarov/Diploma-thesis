using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
        }
    }
}
