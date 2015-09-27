using FirstFloor.ModernUI.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using TestCaseManager.Core;

namespace TestCaseManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Event for logged user
            AuthenticationManager.Instance().Authenticated += (s, e) =>
            {
                var titleLink = this.TitleLinks.Where(link => link.DisplayName.Equals("Login")).First();
                titleLink.DisplayName = "Logout";
            };
        }

        private void Link_MouseClicked(object sender, MouseButtonEventArgs e)
        {
            //FrameworkElement link = e.OriginalSource as FrameworkElement;
            //if (link != null)
            //{
            //    if (link.Name == "BreedLink")
            //    {

            //    }
            //    else if (link.Name == "SpecieLink")
            //    {
            //    }
            //}
        }
    }
}
