using FirstFloor.ModernUI.Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using System;
using System.Threading.Tasks;
using TestCaseManager.Views.CustomControls;

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
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void MainWindowProjectAndTestCases_Loaded(object sender, RoutedEventArgs e)
        {
            List<ProjectProxy> projectList = null;
            Task task = Task.Factory.StartNew(() =>
            {
                CaseManager manager = new CaseManager();
                projectList = manager.GetAll();
  
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.projects.ItemsSource = projectList;
                    this.listBoxStations.ItemsSource = new StepDefinitionCollection().stepDefinitionCollection;

                    this.MainTable.Visibility = Visibility.Visible;
                    this.progressBar.Visibility = Visibility.Hidden;
                }));
            });
        }

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object currentSelectedItem = projects.SelectedItem;
            switch (currentSelectedItem.GetType().Name.ToLower())
            {
                case "testcaseproxy":
                    {
                        this.SetCurrentTestCase(currentSelectedItem);
                        break;
                    }
                default:
                    break;
            }
        }

        private void SetCurrentTestCase(object selectedItem)
        {
            TestCaseProxy testCase = selectedItem as TestCaseProxy;
            if (testCase != null)
            {
                this.TestCaseIdLabel.Content = testCase.Id;
                this.TestCaseNameLabel.Text = testCase.Title;
                this.TestCasePriorityLabel.Content = testCase.Priority;
                this.TestCaseSeverityLabel.Content = testCase.Severity;
                this.TestCaseAutomatedLabel.Content = testCase.IsAutomated;
                this.TestCaseCreatedByLabel.Content = testCase.CreatedBy;
                this.TestCaseUpdatedByLabel.Content = testCase.UpdatedBy;
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
            this.TestCaseEditViewBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseId.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseCreatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseUpdatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseStatusRun.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            string repeatPassword = PromptDialog.Prompt("Create new project", "Project name");
        }
    }

    public sealed class StepDefinitionCollection
    {
        public ObservableCollection<StepDefinition> stepDefinitionCollection { get; private set; }
        public StepDefinitionCollection()
        {
            stepDefinitionCollection = new ObservableCollection<StepDefinition>();
            stepDefinitionCollection.Add(new StepDefinition { Step = "OneOneOneOneOneOneOneOneOneOneOneOneOneOneOneOneOneOneOneOne", ExpectedResult = "1151515151515151515151515151515151515155" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One1", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One2", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One3", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One4", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One5", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One6", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One7", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One8", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One9", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One10", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
        }
    }
}
