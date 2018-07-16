using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.Utilities;
using TestCaseManager.Utilities.StringUtility;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    ///     Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseRunDialog : Window
    {
        private readonly Dictionary<ExtendedTestCaseProxy, Status> _runStatus =
            new Dictionary<ExtendedTestCaseProxy, Status>();

        public TestCaseRunDialog()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            Loaded += PromptDialog_Loaded;

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
            DragWindow.MouseLeftButtonDown += AttachDragDropEvent;
        }

        private static int RunId { get; set; }
        private int CurrentTestCaseIndex { get; set; }
        private ExtendedTestCaseProxy CurrentSelectedTestCase { get; set; }

        private TestRunProxy TestRunProxy { get; set; }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public static void Prompt(int testRunId)
        {
            var instance = new TestCaseRunDialog();
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
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                Dispatcher.Invoke(() =>
                {
                    SetCurrentAccentColor();
                    CurrentTestCaseLabel.Content = $"1/{TestRunProxy.TestCasesList.Count()}";

                    // Map to dictionary for easier manipulation
                    foreach (var testCase in TestRunProxy.TestCasesList) _runStatus[testCase] = testCase.Status;

                    // Starting run point
                    CurrentSelectedTestCase = TestRunProxy.TestCasesList.FirstOrDefault();
                    CurrentTestCaseIndex = 0; // Index
                    SetCurrentTestCase(CurrentSelectedTestCase);

                    _runStatus[CurrentSelectedTestCase] = CurrentSelectedTestCase.Status;
                    StatusComboBox.SelectedIndex = (int) _runStatus[CurrentSelectedTestCase];
                });
            });
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor") SetCurrentAccentColor();
        }

        private void SetCurrentAccentColor()
        {
            TestCasePanelBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            TestCaseEditViewBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                TestStepList.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                Background = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                TestStepList.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void SetCurrentTestCase(ExtendedTestCaseProxy testCaseProxy)
        {
            if (testCaseProxy != null)
            {
                TestCaseTitle.Text = testCaseProxy.Title;
                PriorityComboBox.SelectedIndex = (int) testCaseProxy.Priority;
                SeverityComboBox.SelectedIndex = (int) testCaseProxy.Severity;
                IsAutomatedCheckBox.IsChecked = testCaseProxy.IsAutomated;

                TestStepList.ItemsSource = testCaseProxy.StepDefinitionList;
            }
        }

        private void NextTestCase_Click(object sender, RoutedEventArgs e)
        {
            SetPreviousTestCaseStatus();

            var testCasesCount = TestRunProxy.TestCasesList.Count();
            if (CurrentTestCaseIndex >= 0 && CurrentTestCaseIndex < testCasesCount - 1)
            {
                CurrentTestCaseIndex++;
                if (CurrentTestCaseIndex < testCasesCount) SetCurrentSelectedTestCase(testCasesCount);
            }
        }

        private void PreviousTestCase_Click(object sender, RoutedEventArgs e)
        {
            SetPreviousTestCaseStatus();

            var testCasesCount = TestRunProxy.TestCasesList.Count();
            if (CurrentTestCaseIndex > 0 && CurrentTestCaseIndex < testCasesCount)
            {
                CurrentTestCaseIndex--;
                if (CurrentTestCaseIndex >= 0) SetCurrentSelectedTestCase(testCasesCount);
            }
        }

        private void RegisterIssue_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            Application.Current.MainWindow.Topmost = true;

            GitHubRepoSelectorDialog.Prompt(CurrentSelectedTestCase.Id);
            Application.Current.MainWindow.Topmost = false;
            WindowState = WindowState.Normal;
        }

        private void SaveRunStatus_Click(object sender, RoutedEventArgs e)
        {
            SetPreviousTestCaseStatus();
            foreach (var item in _runStatus)
            {
                var manager = new TestRunManager();
                manager.UpdateTestCaseStatus(RunId, item.Key.Id, item.Value);
            }

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

        private void SetCurrentSelectedTestCase(int testCasesCount)
        {
            CurrentSelectedTestCase = TestRunProxy.TestCasesList[CurrentTestCaseIndex];
            CurrentTestCaseLabel.Content = $"{CurrentTestCaseIndex + 1}/{testCasesCount}";

            if (_runStatus.ContainsKey(CurrentSelectedTestCase) == false)
                StatusComboBox.SelectedIndex = 0;
            else
                StatusComboBox.SelectedIndex = (int) _runStatus[CurrentSelectedTestCase];

            SetCurrentTestCase(CurrentSelectedTestCase);
        }

        private void SetPreviousTestCaseStatus()
        {
            _runStatus[CurrentSelectedTestCase] = EnumUtil.ParseEnum<Status>(
                (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString().Replace(" ", string.Empty));
        }
    }
}