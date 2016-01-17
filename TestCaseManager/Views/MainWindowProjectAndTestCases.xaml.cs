using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCaseManager.Core;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Views.CustomControls;

namespace TestCaseManager.Pages
{
    /// <summary>
    /// Interaction logic for MainWindowProjectAndTestCases.xaml
    /// </summary>
    public partial class MainWindowProjectAndTestCases : UserControl
    {
        private ObservableCollection<ProjectProxy> projectList = new ObservableCollection<ProjectProxy>();
        private readonly Timer timer = new Timer();

        public MainWindowProjectAndTestCases()
        {
            InitializeComponent();
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void ObtainDbRecords(object source, ElapsedEventArgs e)
        {
            ProxyManager manager = new ProxyManager();
            this.Dispatcher.Invoke((Action)(() =>
            {
               bool isHardUIRefreshRequired = false;
               var dbCollection = manager.GetAll();
               foreach (var dbItem in dbCollection)
               {
                   bool doesExist = false;
                   foreach (var uiItem in this.projectList)
                   {                        
                       if(uiItem.ID == dbItem.ID)
                       {
                           if (uiItem.Title != dbItem.Title)
                           {
                               uiItem.Title = dbItem.Title;
                               isHardUIRefreshRequired = true;
                           }
                           uiItem.UpdatedBy = dbItem.UpdatedBy;

                           doesExist = true;
                           break;
                       }
                   }

                   if (!doesExist)
                       this.projectList.Add(dbItem);
               }

               if (isHardUIRefreshRequired)
                   this.projects.Items.Refresh();

            }));
        }

        private void MainWindowProjectAndTestCases_Loaded(object sensder, RoutedEventArgs e)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                ProxyManager manager = new ProxyManager();
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

                    // Register timer event
                    timer.Elapsed += new ElapsedEventHandler(this.ObtainDbRecords);
                    // 3 minutes = 180000
                    timer.Interval = 4000;
                    timer.Start();
                }));
            });
        }

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object currentSelectedItem = this.projects.SelectedItem;
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
            string projectTitle = PromptDialog.Prompt("Project name", "Create new project");

            ProjectManager projManager = new ProjectManager();
            ProjectProxy proxyProject = ProxyConverter.ProjectModelToProxy(projManager.Create(projectTitle));
            this.projectList.Add(proxyProject);
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            string projectTitle = PromptDialog.Prompt("Type the name of this project (to be sure you delete the right project)", "Delete project");
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            if (projectTitle.Equals(proxy.Title))
            {
                ProjectManager projManager = new ProjectManager();
                projManager.DeleteById(proxy.ID);

                this.projectList.Remove(proxy);
            }
        }

        private void EditProject(object sender, RoutedEventArgs e)
        {
            //string projectTitle = PromptDialog.Prompt("Type the name of this project (to be sure you delete the right project)", "Delete project");
            //ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            //if (projectTitle.Equals(proxy.Title))
            //{
            //    ProjectManager projManager = new ProjectManager();
            //    projManager.DeleteById(proxy.ID);

            //    this.projectList.Remove(proxy);
            //}
        }

        private void AddArea(object sender, RoutedEventArgs e)
        {
            string areaTitle = PromptDialog.Prompt("Area name", "Create new area");
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            AreaManager areaManager = new AreaManager();
            AreaProxy areaProxy = ProxyConverter.AreaModelToProxy(areaManager.Create(areaTitle, proxy.ID));
            proxy.Areas.Add(areaProxy);
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
