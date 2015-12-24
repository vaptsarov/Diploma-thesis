using FirstFloor.ModernUI.Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

            this.listBoxStations.ItemsSource = new StepDefinitionCollection().stepDefinitionCollection;
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
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
            tt.Add(new Project("Name"));
        }
    }
}
