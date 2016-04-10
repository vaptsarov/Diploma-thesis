using FirstFloor.ModernUI.Presentation;
using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy.TestRun;
using System.Collections.Generic;
using TestCaseManager.Core.Proxy.TestStatus;
using System.Windows.Controls;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseRunDialog : Window
    {
        private static int RunId { get; set; }
        private int CurrentTestCaseIndex { get; set; }
        private ExtendedTestCaseProxy CurrentSelectedTestCase { get; set; }

        private TestRunProxy TestRunProxy { get; set; }
        private Dictionary<ExtendedTestCaseProxy, Status> runStatus = new Dictionary<ExtendedTestCaseProxy, Status>();

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
            // Initial DB data retrieve
            Task task = Task.Factory.StartNew(() =>
            {
                TestRunProxyManager testRunManager = new TestRunProxyManager();
                this.TestRunProxy = testRunManager.GetById(RunId);
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.CurrentTestCaseLabel.Content = string.Format("1/{0}", this.TestRunProxy.TestCasesList.Count());

                    // Map to dictionary for easier manipulation
                    foreach (ExtendedTestCaseProxy testCase in this.TestRunProxy.TestCasesList)
                    {
                        runStatus[testCase] = testCase.Status;
                    }

                    // Starting run point
                    this.CurrentSelectedTestCase = this.TestRunProxy.TestCasesList.FirstOrDefault();
                    this.CurrentTestCaseIndex = 0; // Index
                    this.SetCurrentTestCase(CurrentSelectedTestCase);

                    this.runStatus[CurrentSelectedTestCase] = CurrentSelectedTestCase.Status;
                    this.StatusComboBox.SelectedIndex = (int)this.runStatus[CurrentSelectedTestCase];
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
            this.TestCasePanelBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.TestCaseEditViewBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

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

        private void SetCurrentTestCase(ExtendedTestCaseProxy testCaseProxy)
        {
            if(testCaseProxy != null)
            {
                this.TestCaseTitle.Text = testCaseProxy.Title;
                this.PriorityComboBox.SelectedIndex = (int)testCaseProxy.Priority;
                this.SeverityComboBox.SelectedIndex = (int)testCaseProxy.Severity;
                this.IsAutomatedCheckBox.IsChecked = testCaseProxy.IsAutomated;

                this.TestStepList.ItemsSource = testCaseProxy.StepDefinitionList;
            }
        }

        private void NextTestCase_Click(object sender, RoutedEventArgs e)
        {
            this.SetPreviousTestCaseStatus();

            int testCasesCount = this.TestRunProxy.TestCasesList.Count();
            if (this.CurrentTestCaseIndex >= 0 && this.CurrentTestCaseIndex < testCasesCount - 1)
            {
                this.CurrentTestCaseIndex++;
                if (this.CurrentTestCaseIndex < testCasesCount)
                {
                    this.SetCurrentSelectedTestCase(testCasesCount);
                }
            }
        }
        
        private void PreviousTestCase_Click(object sender, RoutedEventArgs e)
        {
            this.SetPreviousTestCaseStatus();

            int testCasesCount = this.TestRunProxy.TestCasesList.Count();
            if (this.CurrentTestCaseIndex > 0 && this.CurrentTestCaseIndex < testCasesCount)
            {
                this.CurrentTestCaseIndex--;
                if (this.CurrentTestCaseIndex >= 0)
                {
                    this.SetCurrentSelectedTestCase(testCasesCount);
                }
            }
        }

        private void RegisterIssue_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            Application.Current.MainWindow.Topmost = true;

            GitHubRepoSelectorDialog.Prompt(this.CurrentSelectedTestCase.Id);
            Application.Current.MainWindow.Topmost = false;
            this.WindowState = WindowState.Normal;
        }

        private void SaveRunStatus_Click(object sender, RoutedEventArgs e)
        {
            this.SetPreviousTestCaseStatus();
            foreach (KeyValuePair<ExtendedTestCaseProxy, Status> item in runStatus)
            {
                TestRunManager manager = new TestRunManager();
                manager.UpdateTestCaseStatus(RunId, item.Key.Id, item.Value);
            }

            this.CancelDialog();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.CancelDialog();
        }

        private void CancelDialog()
        {
            this.Close();
        }

        private void SetCurrentSelectedTestCase(int testCasesCount)
        {
            this.CurrentSelectedTestCase = this.TestRunProxy.TestCasesList[this.CurrentTestCaseIndex];
            this.CurrentTestCaseLabel.Content = string.Format("{0}/{1}", CurrentTestCaseIndex + 1, testCasesCount);

            if (this.runStatus.ContainsKey(this.CurrentSelectedTestCase) == false)
            {
                this.StatusComboBox.SelectedIndex = 0;
            }
            else
            {
                this.StatusComboBox.SelectedIndex = (int)this.runStatus[this.CurrentSelectedTestCase];
            }

            this.SetCurrentTestCase(this.CurrentSelectedTestCase);
        }

        private void SetPreviousTestCaseStatus()
        {
            this.runStatus[this.CurrentSelectedTestCase] = EnumUtil.ParseEnum<Status>((this.StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString().Replace(" ", string.Empty));
        }
    }
}
