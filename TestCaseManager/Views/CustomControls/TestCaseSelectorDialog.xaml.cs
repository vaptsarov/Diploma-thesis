using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestCaseManager.Core;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseSelectorDialog : Window
    {
        private static int RunId { get; set; }
        private TestRunProxy proxy { get; set; }
        private ObservableCollection<ProjectProxy> UIProjectProxyList = new ObservableCollection<ProjectProxy>();

        public TestCaseSelectorDialog()
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
            TestCaseSelectorDialog inst = new TestCaseSelectorDialog();
            RunId = testRunId;

            inst.ShowDialog();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.CancelDialog();
        }

        private void CancelDialog()
        {
            this.Close();
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            Task task = Task.Factory.StartNew(() =>
            {
                TestRunProxyManager testRunManager = new TestRunProxyManager();
                this.proxy = testRunManager.GetById(RunId);

                ProjectProxyManager proxyManager = new ProjectProxyManager();
                this.UIProjectProxyList = proxyManager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();

                    this.projects.ItemsSource = UIProjectProxyList;
                    this.SelectedTestCasesList.ItemsSource = this.proxy.TestCasesList;

                    this.TestRunList.Visibility = Visibility.Visible;
                    this.progressBar.Visibility = Visibility.Hidden;

                }));
            });
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                this.SetCurrentAccentColor();
            }
        }

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object currentSelectedItem = this.projects.SelectedItem;
            switch (currentSelectedItem.GetType().Name.ToLower())
            {
                case "testcaseproxy":
                    {
                        //this.SetCurrentTestCase(currentSelectedItem);
                        break;
                    }
                default:
                    break;
            }
        }

        private void SetCurrentAccentColor()
        {
            this.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }
    }
}
