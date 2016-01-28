using FirstFloor.ModernUI.Presentation;
using System;
using System.Linq;
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
            this.Dispatcher.Invoke((Action)(() =>
            {
                // Hard UI reset is needed here
                ProxyManager manager = new ProxyManager();
                this.projectList = manager.GetAll();
                this.projects.ItemsSource = this.projectList;
            }));
        }

        private void MainWindowProjectAndTestCases_Loaded(object sensder, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            Task task = Task.Factory.StartNew(() =>
            {
                ProxyManager manager = new ProxyManager();
                this.projectList = manager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.projects.ItemsSource = projectList;

                    this.MainTable.Visibility = Visibility.Visible;
                    this.progressBar.Visibility = Visibility.Hidden;

                    // Register timer event
                    timer.Elapsed += new ElapsedEventHandler(this.ObtainDbRecords);
                    // 30 minutes = 1800000
                    timer.Interval = 1800000;
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

                // Setting the steps for the current test case.
                this.listBoxStations.ItemsSource = testCase.StepDefinitionList;
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

        #region Project CRUD

        private void AddProject(object sender, RoutedEventArgs e)
        {
            string projectTitle = PromptDialog.Prompt("Project name", "Create new project");

            if (!string.IsNullOrWhiteSpace(projectTitle))
            {
                ProjectManager projManager = new ProjectManager();
                ProjectProxy proxyProject = ProxyConverter.ProjectModelToProxy(projManager.Create(projectTitle));
                this.projectList.Add(proxyProject);
            }
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            string projectTitle = PromptDialog.Prompt("Type the name of this project (to be sure you delete the right project)", "Delete project");
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            if (!string.IsNullOrWhiteSpace(projectTitle) && projectTitle.Equals(proxy.Title))
            {
                ProjectManager projManager = new ProjectManager();
                projManager.DeleteById(proxy.ID);

                this.projectList.Remove(proxy);
            }
        }

        private void EditProject(object sender, RoutedEventArgs e)
        {
            string projectTitle = PromptDialog.Prompt("New project name", "Edit project name");
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            if (!string.IsNullOrWhiteSpace(projectTitle) && !projectTitle.Equals(proxy.Title))
            {
                ProjectManager projManager = new ProjectManager();
                projManager.Update(proxy.ID, projectTitle);

                proxy.Title = projectTitle;
                this.projectList.Remove(proxy);
                this.projectList.Add(proxy);
            }
        }

        #endregion

        #region Area CRUD

        private void AddArea(object sender, RoutedEventArgs e)
        {
            string areaTitle = PromptDialog.Prompt("Area name", "Create new area");
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            if (!string.IsNullOrWhiteSpace(areaTitle) && areaTitle != null)
            {
                AreaManager areaManager = new AreaManager();
                AreaProxy areaProxy = ProxyConverter.AreaModelToProxy(areaManager.Create(areaTitle, proxy.ID));
                proxy.Areas.Add(areaProxy);
            }
        }

        private void EditArea(object sender, RoutedEventArgs e)
        {
            string areaTitle = PromptDialog.Prompt("New are name", "Edit area name");
            AreaProxy areaProxy = ((MenuItem)sender).DataContext as AreaProxy;

            if (!string.IsNullOrWhiteSpace(areaTitle) && !areaTitle.Equals(areaProxy.Title))
            {
                ProjectProxy projectProxy = this.projectList.Where(proj => proj.Areas.Any(a => a.ID == areaProxy.ID)).FirstOrDefault();

                if (projectProxy != null)
                {
                    AreaManager areaManager = new AreaManager();
                    areaManager.Update(areaProxy.ID, areaTitle);

                    areaProxy.Title = areaTitle;
                    projectProxy.Areas.Remove(areaProxy);
                    projectProxy.Areas.Add(areaProxy);
                }
            }
        }

        private void DeleteArea(object sender, RoutedEventArgs e)
        {
            string areaTitle = PromptDialog.Prompt("Type the name of this area (to be sure you delete the right area)", "Delete area");
            AreaProxy proxy = ((MenuItem)sender).DataContext as AreaProxy;

            if (!string.IsNullOrWhiteSpace(areaTitle) && areaTitle.Equals(proxy.Title))
            {
                ProjectProxy projectProxy = this.projectList.Where(proj => proj.Areas.Any(a => a.ID == proxy.ID)).FirstOrDefault();

                if (projectProxy != null)
                {
                    AreaManager areaManager = new AreaManager();
                    areaManager.DeleteById(proxy.ID);

                    projectProxy.Areas.Remove(proxy);
                }
            }
        }

        #endregion

        #region TestCase CRUD

        public void CreateTestCase(object sender, RoutedEventArgs e)
        {
            AreaProxy areaproxy = ((MenuItem)sender).DataContext as AreaProxy;
            TestCaseProxy createdTestCaseProxy = CreateTestCaseDialog.Prompt(areaproxy);

            if (createdTestCaseProxy != null)
            {
                areaproxy.TestCasesList.Add(createdTestCaseProxy);
            }
        }

        #endregion
    }
}
