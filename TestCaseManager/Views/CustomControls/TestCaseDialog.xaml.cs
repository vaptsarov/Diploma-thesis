namespace TestCaseManager.Views.CustomControls
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Core.Converters;
    using Core.Managers;
    using Core.Proxy;
    using Core.Proxy.TestDefinition;
    using FirstFloor.ModernUI.Presentation;
    using Models;
    using Utilities;
    using Utilities.StringUtility;

    /// <summary>
    ///     Interaction logic for CreateTestCaseDialog.xaml
    /// </summary>
    public partial class TestCaseDialog : Window
    {
        private static TestCaseProxy _testCase;

        private readonly bool _isEditingExistingTestCase;
        private readonly AreaProxy _relatedArea;

        private readonly ObservableCollection<StepDefinitionProxy> _stepDefinitionList =
            new ObservableCollection<StepDefinitionProxy>();

        private readonly int _testCaseId;
        private TextboxViewModel _textboxViewModel;

        public TestCaseDialog()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            Loaded += PromptDialog_Loaded;
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;

            DragWindow.MouseLeftButtonDown += AttachDragDropEvent;
        }

        public TestCaseDialog(AreaProxy area) : this()
        {
            _relatedArea = area;
            TestStepList.ItemsSource = _stepDefinitionList;
        }

        public TestCaseDialog(TestCaseProxy editTestCase) : this()
        {
            CreateOrEditTestCase.Content = "Save Test Case";

            TestCaseTitleValidation(editTestCase.Title);
            _testCaseId = editTestCase.Id;
            PriorityComboBox.SelectedIndex = (int) editTestCase.Priority;
            SeverityComboBox.SelectedIndex = (int) editTestCase.Severity;
            IsAutomatedCheckBox.IsChecked = editTestCase.IsAutomated;

            // Deep clone is needed in case of step definitions changes, which are not saved in the db.
            _stepDefinitionList = DeepCloneUtility.DeepClone(editTestCase.StepDefinitionList);
            TestStepList.ItemsSource = _stepDefinitionList;

            _isEditingExistingTestCase = true;
        }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        public static TestCaseProxy Prompt(AreaProxy relatedArea)
        {
            var inst = new TestCaseDialog(relatedArea);
            inst.ShowDialog();

            return _testCase;
        }

        public static TestCaseProxy Prompt(TestCaseProxy editTestCase)
        {
            var inst = new TestCaseDialog(editTestCase);
            inst.ShowDialog();

            return _testCase;
        }

        private void CreateTestCase(object sender, RoutedEventArgs e)
        {
            TestCaseTitleValidation(TestCaseTitle.Text);

            if (!string.IsNullOrWhiteSpace(TestCaseTitle.Text))
            {
                var testCase = new TestCaseProxy
                {
                    Id = _testCaseId,
                    Title = TestCaseTitle.Text,
                    Priority =
                        EnumUtil.ParseEnum<Priority>((PriorityComboBox.SelectedItem as ComboBoxItem).Content
                            .ToString()),
                    Severity =
                        EnumUtil.ParseEnum<Severity>((SeverityComboBox.SelectedItem as ComboBoxItem).Content
                            .ToString()),
                    IsAutomated = IsAutomatedCheckBox.IsChecked ?? false,

                    StepDefinitionList = new ObservableCollection<StepDefinitionProxy>()
                };
                foreach (var item in TestStepList.ItemsSource)
                {
                    var stepDefinition = new StepDefinitionProxy
                    {
                        Id = (item as StepDefinitionProxy).Id,
                        Step = (item as StepDefinitionProxy).Step,
                        ExpectedResult = (item as StepDefinitionProxy).ExpectedResult,
                        TestCaseId = testCase.Id
                    };

                    testCase.StepDefinitionList.Add(stepDefinition);
                }

                var manager = new TestManager();

                if (!_isEditingExistingTestCase)
                    _testCase = ProxyConverter.TestCaseModelToProxy(manager.Create(_relatedArea.Id,
                        ModelConverter.TestCaseProxyToModel(testCase)));
                else
                    _testCase =
                        ProxyConverter.TestCaseModelToProxy(
                            manager.Update(ModelConverter.TestCaseProxyToModel(testCase)));

                CancelDialog();
            }
        }

        private void TestCaseTitleValidation(string title)
        {
            _textboxViewModel = new TextboxViewModel
            {
                Text = title
            };
            DataContext = _textboxViewModel;
        }

        private void AddTestStep(object sender, RoutedEventArgs e)
        {
            _stepDefinitionList.Add(new StepDefinitionProxy());
        }

        private void DeleteTestStep(object sender, RoutedEventArgs e)
        {
            var stepDefinitionToDelete = TestStepList.SelectedItem as StepDefinitionProxy;
            if (stepDefinitionToDelete != null) _stepDefinitionList.Remove(stepDefinitionToDelete);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            CancelDialog();
        }

        private void CancelDialog()
        {
            Close();
        }

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            SetCurrentAccentColor();
            TestCaseTitle.Focus();
        }

        private void OnSelected(object sender, MouseButtonEventArgs e)
        {
            var initialSelectedItem = sender as DependencyObject;
            while (initialSelectedItem != null)
            {
                if (initialSelectedItem is ListBoxItem)
                {
                    var selectedListBoxItem = initialSelectedItem as ListBoxItem;
                    TestStepList.SelectedItem = selectedListBoxItem.Content;
                    break;
                }

                initialSelectedItem = VisualTreeHelper.GetParent(initialSelectedItem);
            }
        }
    }
}