using FirstFloor.ModernUI.Presentation;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TestCaseManager.Core.Proxy.TestRun;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseRunDialog : Window
    {
        private static int RunId { get; set; }
        private TestRunProxy TestRunProxy { get; set; }

        public TestCaseRunDialog()
        {
            this.InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.Loaded += new RoutedEventHandler(this.PromptDialog_Loaded);

            AppearanceManager.Current.PropertyChanged += this.OnAppearanceManagerPropertyChanged;
            this.DragWindow.MouseLeftButtonDown += new MouseButtonEventHandler(this.AttachDragDropEvent);
        }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public static void Prompt(int testRunId)
        {
            TestCaseRunDialog instance = new TestCaseRunDialog();
            RunId = testRunId;
            instance.ShowDialog();
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetCurrentAccentColor();
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
            this.TestCasePanelBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.TestCaseEditViewBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.TestStepList.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                this.Background = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.TestStepList.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.CancelDialog();
        }

        private void CancelDialog()
        {
            this.Close();
        }
    }
}
