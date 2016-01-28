using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
    public partial class CreateTestCaseDialog : Window
    {
        private TextboxViewModel textboxViewModel = null;

        // Dummy list
        private readonly ObservableCollection<StepDefinitionProxy> stepList = new ObservableCollection<StepDefinitionProxy>();
        private static AreaProxy area;
        private static TestCaseProxy createdTestCase;

        public CreateTestCaseDialog()
        {
            this.InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.Loaded += new RoutedEventHandler(PromptDialog_Loaded);

            // Register dummy list
            this.TestStepList.ItemsSource = this.stepList;
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.textBox.Focus();
        }

        public static TestCaseProxy Prompt(AreaProxy relatedArea)
        {
            CreateTestCaseDialog inst = new CreateTestCaseDialog();
            area = relatedArea;
            inst.ShowDialog();

            return createdTestCase;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.RegisterUsernameValidation();

            if (!string.IsNullOrWhiteSpace(this.TestCaseTitle.Text))
            {
                TestCaseProxy testCase = new TestCaseProxy();
                testCase.Title = this.TestCaseTitle.Text;
                testCase.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";
                testCase.Priority = EnumUtil.ParseEnum<Priority>((this.PriorityComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                testCase.Severity = EnumUtil.ParseEnum<Severity>((this.SeverityComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                testCase.IsAutomated = this.IsAutomatedCheckBox.IsChecked ?? false;

                testCase.StepDefinitionList = new ObservableCollection<StepDefinitionProxy>();
                foreach (var item in this.TestStepList.ItemsSource)
                {
                    StepDefinitionProxy stepDefinition = new StepDefinitionProxy();
                    stepDefinition.Step = (item as StepDefinitionProxy).Step;
                    stepDefinition.ExpectedResult = (item as StepDefinitionProxy).ExpectedResult;

                    testCase.StepDefinitionList.Add(stepDefinition);
                }

                TestManager manager = new TestManager();
                createdTestCase = ProxyConverter.TestCaseModelToProxy(manager.Create(area.ID, testCase));

                this.CancelDialog();
            }
        }
        private void RegisterUsernameValidation()
        {
            this.textboxViewModel = new TextboxViewModel();
            this.textboxViewModel.Text = this.TestCaseTitle.Text;
            this.DataContext = this.textboxViewModel;
        }

        private void AddTestStep_Click(object sender, RoutedEventArgs e)
        {
            this.stepList.Add(new StepDefinitionProxy());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.CancelDialog();
        }

        private void CancelDialog()
        {
            this.Close();
        }
    }
}
