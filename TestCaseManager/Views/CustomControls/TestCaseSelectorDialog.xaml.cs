using FirstFloor.ModernUI.Presentation;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseSelectorDialog : Window
    {
        private static int RunId { get; set; }
        private TestRunProxy TestRunProxy { get; set; }
        private ObservableCollection<ProjectProxy> ProjectProxyList { get; set; }
        private ObservableCollection<ExtendedTestCaseProxy> TestCasesList { get; set; }

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
                this.TestRunProxy = testRunManager.GetById(RunId);

                ProjectProxyManager proxyManager = new ProjectProxyManager();
                this.ProjectProxyList = proxyManager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.ProjectTreeView.ItemsSource = this.ProjectProxyList;

                    this.TestCasesList = this.TestRunProxy.TestCasesList;
                    this.SelectedTestCasesList.ItemsSource = this.TestCasesList;

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

        private void AddToTestList(object sender, RoutedEventArgs e)
        {
            if (this.ProjectTreeView.SelectedItem is TestCaseProxy)
            {
                ExtendedTestCaseProxy testCase = new ExtendedTestCaseProxy(this.ProjectTreeView.SelectedItem as TestCaseProxy, Status.Unknown);
                ProjectProxy projectProxy = this.ProjectProxyList.Where(proj => proj.Areas.Any(a => a.ID == testCase.AreaID)).FirstOrDefault();
                if (projectProxy != null)
                {
                    AreaProxy areaProxy = projectProxy.Areas.Where(a => a.ID == testCase.AreaID).FirstOrDefault();
                    if (areaProxy != null)
                        areaProxy.TestCasesList.Remove(this.ProjectTreeView.SelectedItem as TestCaseProxy);
                }

                if (this.TestCasesList.Any(x=>x.Id == testCase.Id) == false)
                    this.TestCasesList.Add(testCase);
            }
        }
    }
}
