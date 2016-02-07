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
using TestCaseManager.Views.CustomControls;
using System.Windows.Input;

namespace TestCaseManager.Pages
{
    /// <summary>
    /// Interaction logic for MainWindowProjectAndTestCases.xaml
    /// </summary>
    public partial class MainWindowProjectAndTestCases : UserControl
    {
        private ObservableCollection<ProjectProxy> UIProjectProxyList = new ObservableCollection<ProjectProxy>();
        private readonly Timer dbCallback = new Timer();

        public MainWindowProjectAndTestCases()
        {
            this.InitializeComponent();
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void MainWindowProjectAndTestCases_Loaded(object sensder, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            Task task = Task.Factory.StartNew(() =>
            {
                ProjectProxyManager manager = new ProjectProxyManager();
                this.UIProjectProxyList = manager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.projects.ItemsSource = UIProjectProxyList;

                    this.MainTable.Visibility = Visibility.Visible;
                    this.progressBar.Visibility = Visibility.Hidden;

                    // Register timer event
                    dbCallback.Elapsed += new ElapsedEventHandler(this.ObtainDbRecords);
                    // 30 minutes = 1800000
                    dbCallback.Interval = 1800000;
                    dbCallback.Start();
                }));
            });
        }

        private void ObtainDbRecords(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                // Hard UI reset is needed here
                ProjectProxyManager manager = new ProjectProxyManager();
                this.UIProjectProxyList = manager.GetAll();
                this.projects.ItemsSource = this.UIProjectProxyList;
            }));
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
            this.CreateProjectButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            Color white = Color.FromRgb(255, 255, 255);
            Color semiDark = Color.FromRgb(51, 51, 51);
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(white);

                this.ProjectsBorder.Background = new SolidColorBrush(semiDark);
                this.TestCasePanelBorder.Background = new SolidColorBrush(semiDark);
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.ProjectsBorder.Background = new SolidColorBrush(white);
                this.TestCasePanelBorder.Background = new SolidColorBrush(white);
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
                this.UIProjectProxyList.Add(proxyProject);
            }
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            string projectTitle = PromptDialog.Prompt("Type the name of this project (to be sure you delete the right project)", "Delete project");
            if (!string.IsNullOrWhiteSpace(projectTitle) && projectTitle.Equals(proxy.Title))
            {
                ProjectManager projManager = new ProjectManager();
                projManager.DeleteById(proxy.ID);

                this.UIProjectProxyList.Remove(proxy);
            }
        }

        private void EditProject(object sender, RoutedEventArgs e)
        {
            ProjectProxy proxy = ((MenuItem)sender).DataContext as ProjectProxy;

            string projectTitle = PromptDialog.Prompt("New project name", "Edit project name");
            if (!string.IsNullOrWhiteSpace(projectTitle) && !projectTitle.Equals(proxy.Title))
            {
                proxy.Title = projectTitle;

                ProjectManager projManager = new ProjectManager();
                projManager.Update(proxy);

                this.UIProjectProxyList.Remove(proxy);
                this.UIProjectProxyList.Add(proxy);
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
            AreaProxy areaProxy = ((MenuItem)sender).DataContext as AreaProxy;

            string areaTitle = PromptDialog.Prompt("New area name", "Edit area name");
            if (!string.IsNullOrWhiteSpace(areaTitle) && !areaTitle.Equals(areaProxy.Title))
            {
                ProjectProxy projectProxy = this.UIProjectProxyList.Where(proj => proj.Areas.Any(a => a.ID == areaProxy.ID)).FirstOrDefault();

                if (projectProxy != null)
                {
                    areaProxy.Title = areaTitle;

                    AreaManager areaManager = new AreaManager();
                    areaManager.Update(areaProxy);

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
                ProjectProxy projectProxy = this.UIProjectProxyList.Where(proj => proj.Areas.Any(a => a.ID == proxy.ID)).FirstOrDefault();

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

        private void CreateTestCase(object sender, RoutedEventArgs e)
        {
            AreaProxy areaproxy = ((MenuItem)sender).DataContext as AreaProxy;
            TestCaseProxy createdTestCaseProxy = TestCaseDialog.Prompt(areaproxy);

            if (createdTestCaseProxy != null)
            {
                areaproxy.TestCasesList.Add(createdTestCaseProxy);
            }
        }

        private void EditTestCase(object sender, RoutedEventArgs e)
        {
            TestCaseProxy testCase = ((MenuItem)sender).DataContext as TestCaseProxy;
            this.EditTestCaseFromProxy(testCase);
        }

        private void EditTestCase_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                TestCaseProxy testCase = ((TextBlock)sender).DataContext as TestCaseProxy;
                this.EditTestCaseFromProxy(testCase);
            }
        }

        private void EditTestCaseFromProxy(TestCaseProxy testCase)
        {
            TestCaseProxy editedTestCase = TestCaseDialog.Prompt(testCase);

            if (editedTestCase != null)
            {
                ProjectProxy projectProxy = this.UIProjectProxyList.Where(proj => proj.Areas.Any(a => a.ID == testCase.AreaID)).FirstOrDefault();
                if (UIProjectProxyList != null)
                {
                    AreaProxy areaProxy = projectProxy.Areas.Where(a => a.ID == testCase.AreaID).FirstOrDefault();
                    if (areaProxy != null)
                    {
                        areaProxy.TestCasesList.Remove(testCase);
                        areaProxy.TestCasesList.Add(editedTestCase);
                    }
                }
            }
        }

        private void DeleteTestCase(object sender, RoutedEventArgs e)
        {
            TestCaseProxy testCaseToDelete = ((MenuItem)sender).DataContext as TestCaseProxy;
            TestManager manager = new TestManager();
            manager.DeleteById(testCaseToDelete.Id);

            ProjectProxy projectProxy = this.UIProjectProxyList.Where(proj => proj.Areas.Any(a => a.ID == testCaseToDelete.AreaID)).FirstOrDefault();
            if (UIProjectProxyList != null)
            {
                AreaProxy areaProxy = projectProxy.Areas.Where(a => a.ID == testCaseToDelete.AreaID).FirstOrDefault();
                if(areaProxy != null)
                    areaProxy.TestCasesList.Remove(testCaseToDelete);
            }
        }

        #endregion
    }
}
