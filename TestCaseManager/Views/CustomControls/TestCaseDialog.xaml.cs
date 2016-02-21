using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            this.Loaded += new RoutedEventHandler(this.PromptDialog_Loaded);
            AppearanceManager.Current.PropertyChanged += this.OnAppearanceManagerPropertyChanged;

            this.DragWindow.MouseLeftButtonDown += new MouseButtonEventHandler(this.AttachDragDropEvent);
        }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
                this.Background = new SolidColorBrush(Color.FromRgb(37,37,38));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.TestStepList.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
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

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetCurrentAccentColor();
            this.TestCaseTitle.Focus();
        }

        private void OnSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
    }
}
