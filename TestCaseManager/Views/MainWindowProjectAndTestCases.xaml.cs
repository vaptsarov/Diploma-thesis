using FirstFloor.ModernUI.Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            this.SetCurrentAccentColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;

            this.listBoxStations.ItemsSource = new StepDefinitionCollection().stepDefinitionCollection;
        }

        private void tvEmps_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object current;

            current = tvEmps.SelectedItem;
            switch (current.GetType().Name.ToLower())
            {
                case "employee":
                    break;
                case "employeetype":
                    break;
                case "subemployee":
                    break;
                default:
                    break;
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
        }
    }

    public sealed class StepDefinition
    {
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
    }

    public sealed class StepDefinitionCollection
    {
        public ObservableCollection<StepDefinition> stepDefinitionCollection { get; private set; }
        public StepDefinitionCollection()
        {
            stepDefinitionCollection = new ObservableCollection<StepDefinition>();
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
            stepDefinitionCollection.Add(new StepDefinition { Step = "One", ExpectedResult = "15" });
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
