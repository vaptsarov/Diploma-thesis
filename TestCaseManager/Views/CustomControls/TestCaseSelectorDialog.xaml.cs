using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using TestCaseManager.Core;
using TestCaseManager.Core.Converters;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.DB;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    ///     Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseSelectorDialog : Window
    {
        public TestCaseSelectorDialog()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            Loaded += PromptDialog_Loaded;

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
            DragWindow.MouseLeftButtonDown += AttachDragDropEvent;
        }

        private static int RunId { get; set; }
        private TestRunProxy TestRunProxy { get; set; }
        private ObservableCollection<ProjectProxy> ProjectProxyList { get; set; }
        private ObservableCollection<ExtendedTestCaseProxy> TestCasesList { get; set; }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public static void Prompt(int testRunId)
        {
            var instance = new TestCaseSelectorDialog();
            RunId = testRunId;

            instance.ShowDialog();
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            var task = Task.Factory.StartNew(() =>
            {
                var testRunManager = new TestRunProxyManager();
                TestRunProxy = testRunManager.GetById(RunId);

                var proxyManager = new ProjectProxyManager();
                ProjectProxyList = proxyManager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                Dispatcher.Invoke(() =>
                {
                    foreach (var testCase in TestRunProxy.TestCasesList)
                    {
                        var projectProxy = ProjectProxyList.Where(proj => proj.Areas.Any(a => a.Id == testCase.AreaId))
                            .FirstOrDefault();
                        if (projectProxy != null)
                        {
                            var areaProxy = projectProxy.Areas.Where(a => a.Id == testCase.AreaId).FirstOrDefault();
                            if (areaProxy != null)
                            {
                                var testCaseToRemove = areaProxy.TestCasesList.Where(tc => tc.Id == testCase.Id)
                                    .FirstOrDefault();
                                if (testCaseToRemove != null)
                                    areaProxy.TestCasesList.Remove(testCaseToRemove);
                            }
                        }
                    }

                    SetCurrentAccentColor();
                    ProjectTreeView.ItemsSource = ProjectProxyList;

                    TestCasesList = TestRunProxy.TestCasesList;
                    SelectedTestCasesList.ItemsSource = TestCasesList;

                    TestRunList.Visibility = Visibility.Visible;
                    progressBar.Visibility = Visibility.Hidden;
                });
            });
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor") SetCurrentAccentColor();
        }

        private void SetCurrentAccentColor()
        {
            BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
                Background = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void SaveRun(object sender, RoutedEventArgs e)
        {
            ICollection<TestCase> testCasesModelList = new Collection<TestCase>();
            TestCasesList.ToList().ForEach(x => { testCasesModelList.Add(ModelConverter.TestCaseProxyToModel(x)); });

            var manager = new TestRunManager();
            manager.RelateTestCaseToTestRun(TestRunProxy.Id, testCasesModelList);

            CancelDialog();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            CancelDialog();
        }

        private void CancelDialog()
        {
            Close();
        }

        private void AddToTestList(object sender, RoutedEventArgs e)
        {
            if (ProjectTreeView.SelectedItem is TestCaseProxy)
            {
                var testCase =
                    new ExtendedTestCaseProxy(ProjectTreeView.SelectedItem as TestCaseProxy, Status.NotExecuted);
                var projectProxy = ProjectProxyList.Where(proj => proj.Areas.Any(a => a.Id == testCase.AreaId))
                    .FirstOrDefault();
                if (projectProxy != null)
                {
                    var areaProxy = projectProxy.Areas.Where(a => a.Id == testCase.AreaId).FirstOrDefault();
                    if (areaProxy != null)
                        areaProxy.TestCasesList.Remove(ProjectTreeView.SelectedItem as TestCaseProxy);
                }

                if (TestCasesList.Any(x => x.Id == testCase.Id) == false)
                    TestCasesList.Add(testCase);
            }
        }

        private void RemoveFromTestList(object sender, RoutedEventArgs e)
        {
            if (SelectedTestCasesList.SelectedItem != null)
            {
                TestCaseProxy testCase = SelectedTestCasesList.SelectedItem as ExtendedTestCaseProxy;
                var projectProxy = ProjectProxyList.Where(proj => proj.Areas.Any(a => a.Id == testCase.AreaId))
                    .FirstOrDefault();
                if (projectProxy != null)
                {
                    var areaProxy = projectProxy.Areas.Where(a => a.Id == testCase.AreaId).FirstOrDefault();
                    if (areaProxy != null) areaProxy.TestCasesList.Add(testCase);
                }

                TestCasesList.Remove(SelectedTestCasesList.SelectedItem as ExtendedTestCaseProxy);
            }
        }
    }
}