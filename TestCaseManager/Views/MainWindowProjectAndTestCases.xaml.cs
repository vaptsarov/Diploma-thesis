using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using TestCaseManager.Core.Models;

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

            this.projects.ItemsSource = new Test().tt;
            this.listBoxStations.ItemsSource = new StepDefinitionCollection().stepDefinitionCollection;

            //Demo
            this.TestCaseIdLabel.Content = "99999";
            this.TestCaseNameLabel.Text = "Selected test case name Selected test case name Selected test case nameSelected test case name name na asdsadsadsa ds adsame";
            this.TestCasePriorityLabel.Content = "Critical";
            this.TestCaseSeverityLabel.Content = "Blocking";
            this.TestCaseAutomatedLabel.Content = "False";
        }

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object current;

            current = projects.SelectedItem;
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
            this.BorderTestCaseId.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                //this.projects.ItemsSource = new Test().tt;
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
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

    public sealed class Test
    {
        public ObservableCollection<Project> tt { get; private set; }
        public Test()
        {
            tt = new ObservableCollection<Project>();
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            var proj = new Project("Priject");
            var area = new Area("area");
            area.TestCasesList.Add(new TestCase("Testcase1"));
            area.TestCasesList.Add(new TestCase("Testcase2"));
            area.TestCasesList.Add(new TestCase("Testcase3"));
            proj.Areas.Add(area);
            tt.Add(proj);
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
        }
    }
}
