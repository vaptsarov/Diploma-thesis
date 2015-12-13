using FirstFloor.ModernUI.Presentation;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestCaseManager.Pages
{
    /// <summary>
    /// Interaction logic for MainWindowProjectAndTestCases.xaml
    /// </summary>
    public partial class MainWindowProjectAndTestCases : UserControl
    {
        public MainWindowProjectAndTestCases()
        {
            InitializeComponent();
            this.SetCurrentAccentColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }
        
        private void tvEmps_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object current;

            current = tvEmps.SelectedItem;
            switch (current.GetType().Name.ToLower())
            {
                case "employee":
                    break;
                case "employeetype":
                    break;
                case "subemployee":
                    break;
                default:
                    break;
            }
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                this.SetCurrentAccentColor();
            }
        }

        private void SetCurrentAccentColor()
        {
            this.ProjectsBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.TestCasePanelBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
        }
    }
}
