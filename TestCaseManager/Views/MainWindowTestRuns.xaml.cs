using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using TestCaseManager.Core.Converters;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.Views.CustomControls;

namespace TestCaseManager.Views
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaction logic for MainWindowTestRuns.xaml
    /// </summary>
    public partial class MainWindowTestRuns : UserControl
    {
        private readonly Timer _dbCallback = new Timer();
        private ObservableCollection<TestRunProxy> _uiTestRunList = new ObservableCollection<TestRunProxy>();

        public MainWindowTestRuns()
        {
            InitializeComponent();
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void MainWindowTestRuns_Loaded(object sensder, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            var task = Task.Factory.StartNew(() =>
            {
                var manager = new TestRunProxyManager();
                _uiTestRunList = new ObservableCollection<TestRunProxy>(manager.GetAll().OrderBy(x => x.Name));
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                Dispatcher.Invoke(() =>
                {
                    SetCurrentAccentColor();
                    TestRunListBox.ItemsSource = _uiTestRunList;

                    MainTable.Visibility = Visibility.Visible;
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
            TestRunBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            CreateTestRunButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            TestCasesBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseTotal.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCasePassed.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseFailed.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseNotRan.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseCreatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseCreatedOn.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            TestCasesListBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            AddTestsButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            RunButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            var white = Color.FromRgb(255, 255, 255);
            var semiDark = Color.FromRgb(51, 51, 51);
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                TestCasesList.Foreground = new SolidColorBrush(white);
                TestRunBorder.Background = new SolidColorBrush(semiDark);
                TestCasesBorder.Background = new SolidColorBrush(semiDark);
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                TestCasesList.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                TestRunBorder.Background = new SolidColorBrush(white);
                TestCasesBorder.Background = new SolidColorBrush(white);
            }
        }

        private void AddTestRun(object sender, RoutedEventArgs e)
        {
            this.RunWithOverlayer(() =>
            {
                var projectTitle = PromptDialog.Prompt("Test Run name", "Create new test run");
                if (!string.IsNullOrWhiteSpace(projectTitle))
                {
                    var runManager = new TestRunManager();
                    var proxyProject = ProxyConverter.TestRunModelToProxy(runManager.Create(projectTitle));
                    _uiTestRunList.Add(proxyProject);
                }
            });
        }

        private void AddTests(object sender, RoutedEventArgs e)
        {
            this.RunWithOverlayer(() =>
            {
                if (TestRunListBox.SelectedItem is TestRunProxy selectedTestRun)
                {
                    TestCaseSelectorDialog.Prompt(selectedTestRun.Id);
                    UpdateTestRun(selectedTestRun);
                }
            });
        }

        private void RunTests(object sender, RoutedEventArgs e)
        {
            this.RunWithOverlayer(() =>
            {
                if (TestRunListBox.SelectedItem is TestRunProxy selectedTestRun && selectedTestRun.TestCasesList.Count > 0)
                {
                    TestCaseRunDialog.Prompt(selectedTestRun.Id);
                    UpdateTestRun(selectedTestRun);
                }
            });
        }

        private void UpdateTestRun(TestRunProxy selectedTestRun)
        {
            this.RunWithOverlayer(() =>
            {
                var manager = new TestRunProxyManager();
                var updatedTestRun = manager.GetById(selectedTestRun.Id);

                selectedTestRun.TestCasesList = updatedTestRun.TestCasesList;
                OnSelectedItem(this, null);
            });
        }

        private void OnSelectedItem(object sender, SelectionChangedEventArgs args)
        {
            if (TestRunListBox.SelectedItem is TestRunProxy currentSelectedItem)
            {
                TotalLabel.Content = currentSelectedItem.TestCasesList.Count;

                PassedLabel.Content = currentSelectedItem.TestCasesList.Count(x => x.Status.Equals(Status.Passed));

                FailedLabel.Content = currentSelectedItem.TestCasesList.Count(x => x.Status.Equals(Status.Failed));

                NotRanLabel.Content = currentSelectedItem.TestCasesList
                    .Count(x => x.Status.Equals(Status.NotExecuted));

                CreatedBy.Content = currentSelectedItem.CreatedBy;

                CreatedOn.Content = currentSelectedItem.CreatedOn;

                TestCasesList.ItemsSource = currentSelectedItem.TestCasesList;
            }
        }

        private void SelectWholeLine(object sender, RoutedEventArgs e)
        {
            var initialSelectedItem = sender as DependencyObject;
            while (initialSelectedItem != null)
            {
                if (initialSelectedItem is ListBoxItem selectedListBoxItem)
                {
                    TestRunListBox.SelectedItem = selectedListBoxItem.Content;

                    break;
                }

                initialSelectedItem = VisualTreeHelper.GetParent(initialSelectedItem);
            }
        }

        private void RunWithOverlayer(Action action)
        {
            Overlay.Visibility = Visibility.Visible;
            try
            {
                action.Invoke();
            }
            finally
            {
                Overlay.Visibility = Visibility.Collapsed;
            }
        }
    }
}