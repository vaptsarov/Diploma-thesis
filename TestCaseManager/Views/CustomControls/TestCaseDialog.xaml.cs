using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCaseManager.Core;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseDialog : Window
    {
        private TextboxViewModel textboxViewModel;
        private AreaProxy relatedArea;
        private bool isEditingExistingTestCase;
        private int testCaseId;

        private readonly ObservableCollection<StepDefinitionProxy> stepDefinitionList = new ObservableCollection<StepDefinitionProxy>();
        private static TestCaseProxy testCase;

        public TestCaseDialog()
        {
            this.InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.Loaded += new RoutedEventHandler(PromptDialog_Loaded);
        }

        public TestCaseDialog(AreaProxy area) : this()
        {
            this.relatedArea = area;
            this.TestStepList.ItemsSource = this.stepDefinitionList;
        }

        public TestCaseDialog(TestCaseProxy editTestCase) : this()
        {
            this.CreateOrEditTestCase.Content = "Save Test Case";

            this.TestCaseTitleValidation(editTestCase.Title);
            this.testCaseId = editTestCase.Id;
            this.PriorityComboBox.SelectedIndex = (int)editTestCase.Priority;
            this.SeverityComboBox.SelectedIndex = (int)editTestCase.Severity;
            this.IsAutomatedCheckBox.IsChecked = editTestCase.IsAutomated;

            // Deep clone is needed in case of step definitions changes, which are not saved in the db.
            this.stepDefinitionList = DeepCloneUtility.DeepClone(editTestCase.StepDefinitionList);
            this.TestStepList.ItemsSource = this.stepDefinitionList;

            this.isEditingExistingTestCase = true;
        }

        public static TestCaseProxy Prompt(AreaProxy relatedArea)
        {
            TestCaseDialog inst = new TestCaseDialog(relatedArea);
            inst.ShowDialog();

            return testCase;
        }

        public static TestCaseProxy Prompt(TestCaseProxy editTestCase)
        {
            TestCaseDialog inst = new TestCaseDialog(editTestCase);
            inst.ShowDialog();

            return testCase;
        }

        private void CreateTestCase(object sender, RoutedEventArgs e)
        {
            this.TestCaseTitleValidation(this.TestCaseTitle.Text);

            if (!string.IsNullOrWhiteSpace(this.TestCaseTitle.Text))
            {
                TestCaseProxy testCase = new TestCaseProxy();
                testCase.Id = this.testCaseId;
                testCase.Title = this.TestCaseTitle.Text;
                testCase.Priority = EnumUtil.ParseEnum<Priority>((this.PriorityComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                testCase.Severity = EnumUtil.ParseEnum<Severity>((this.SeverityComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                testCase.IsAutomated = this.IsAutomatedCheckBox.IsChecked ?? false;

                testCase.StepDefinitionList = new ObservableCollection<StepDefinitionProxy>();
                foreach (var item in this.TestStepList.ItemsSource)
                {
                    StepDefinitionProxy stepDefinition = new StepDefinitionProxy();
                    stepDefinition.ID = (item as StepDefinitionProxy).ID;
                    stepDefinition.Step = (item as StepDefinitionProxy).Step;
                    stepDefinition.ExpectedResult = (item as StepDefinitionProxy).ExpectedResult;

                    testCase.StepDefinitionList.Add(stepDefinition);
                }

                TestManager manager = new TestManager();

                if (!this.isEditingExistingTestCase)
                {
                    TestCaseDialog.testCase = ProxyConverter.TestCaseModelToProxy(manager.Create(relatedArea.ID, testCase));
                }
                else
                {
                    TestCaseDialog.testCase = ProxyConverter.TestCaseModelToProxy(manager.Update(testCase));
                }

                this.CancelDialog();
            }
        }

        private void TestCaseTitleValidation(string title)
        {
            this.textboxViewModel = new TextboxViewModel();
            this.textboxViewModel.Text = title;
            this.DataContext = this.textboxViewModel;
        }

        private void AddTestStep(object sender, RoutedEventArgs e)
        {
            this.stepDefinitionList.Add(new StepDefinitionProxy());
        }

        private void DeleteTestStep(object sender, RoutedEventArgs e)
        {
            StepDefinitionProxy stepDefinitionToDelete = this.TestStepList.SelectedItem as StepDefinitionProxy;
            if (stepDefinitionToDelete != null)
            {
                this.stepDefinitionList.Remove(stepDefinitionToDelete);
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

        private void OnSelected(object sender, RoutedEventArgs e)
        {
            var initialSelectedItem = sender as DependencyObject;
            while (initialSelectedItem != null)
            {
                if (initialSelectedItem is ListBoxItem)
                {
                    var selectedListBoxItem = (initialSelectedItem as ListBoxItem);
                    this.TestStepList.SelectedItem = selectedListBoxItem.Content;
                    break;
                }

                initialSelectedItem = VisualTreeHelper.GetParent(initialSelectedItem);
            }
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TestCaseTitle.Focus();
        }

        private void OnSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
