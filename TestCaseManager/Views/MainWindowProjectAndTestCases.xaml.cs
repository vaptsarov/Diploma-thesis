namespace TestCaseManager.Views
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Core.Converters;
    using Core.Managers;
    using Core.Managers.ProxyManagers;
    using Core.Proxy;
    using CustomControls;
    using FirstFloor.ModernUI.Presentation;

    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaction logic for MainWindowProjectAndTestCases.xaml
    /// </summary>
    public partial class MainWindowProjectAndTestCases : UserControl
    {
        private readonly Timer _dbCallback = new Timer();
        private ObservableCollection<ProjectProxy> _uiProjectProxyList = new ObservableCollection<ProjectProxy>();

        public MainWindowProjectAndTestCases()
        {
            InitializeComponent();
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void MainWindowProjectAndTestCases_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            var task = Task.Factory.StartNew(() =>
            {
                var manager = new ProjectProxyManager();
                _uiProjectProxyList = manager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                Dispatcher.Invoke(() =>
                {
                    SetCurrentAccentColor();
                    projects.ItemsSource = _uiProjectProxyList;

                    MainTable.Visibility = Visibility.Visible;
                    progressBar.Visibility = Visibility.Hidden;

                    // Register timer event
                    _dbCallback.Elapsed += ObtainDbRecords;
                    // 30 minutes = 1800000
                    _dbCallback.Interval = 1800000;
                    _dbCallback.Start();
                });
            });
        }

        private void ObtainDbRecords(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Hard UI reset is needed here
                var manager = new ProjectProxyManager();
                _uiProjectProxyList = manager.GetAll();
                projects.ItemsSource = _uiProjectProxyList;
            });
        }

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (projects.SelectedItem == null) return;

            var currentSelectedItem = projects.SelectedItem;
            if (currentSelectedItem.GetType().Name.ToLower() == "testcaseproxy")
                SetCurrentTestCase(currentSelectedItem);
        }

        private void SetCurrentTestCase(object selectedItem)
        {
            if (!(selectedItem is TestCaseProxy testCase))
                return;

            TestCaseIdLabel.Content = testCase.Id;
            TestCaseNameLabel.Text = testCase.Title;
            TestCasePriorityLabel.Content = testCase.Priority;
            TestCaseSeverityLabel.Content = testCase.Severity;
            TestCaseAutomatedLabel.Content = testCase.IsAutomated;
            TestCaseCreatedByLabel.Content = testCase.CreatedBy;
            TestCaseUpdatedByLabel.Content = testCase.UpdatedBy;

            // Setting the steps for the current test case.
            listBoxStations.ItemsSource = testCase.StepDefinitionList;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor") SetCurrentAccentColor();
        }

        private void SetCurrentAccentColor()
        {
            ProjectsBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            TestCasePanelBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            TestCaseEditViewBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseId.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseName.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCasePriority.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseSeverity.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseAutomated.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseCreatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseUpdatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            BorderTestCaseStatusRun.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            CreateProjectButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            var white = Color.FromRgb(255, 255, 255);
            var semiDark = Color.FromRgb(51, 51, 51);
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                listBoxStations.Foreground = new SolidColorBrush(white);

                ProjectsBorder.Background = new SolidColorBrush(semiDark);
                TestCasePanelBorder.Background = new SolidColorBrush(semiDark);
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                listBoxStations.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                ProjectsBorder.Background = new SolidColorBrush(white);
                TestCasePanelBorder.Background = new SolidColorBrush(white);
            }
        }

        private void RunWithOverlayer(Action action)
        {
            Overlay.Visibility = Visibility.Visible;
            try
            {
                action.Invoke();
            }
            finally
            {
                Overlay.Visibility = Visibility.Collapsed;
            }
        }

        #region Project CRUD

        private void AddProject(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var projectTitle = PromptDialog.Prompt("Project name", "Create new project");
                if (!string.IsNullOrWhiteSpace(projectTitle))
                {
                    var projManager = new ProjectManager();
                    var proxyProject = ProxyConverter.ProjectModelToProxy(projManager.Create(projectTitle));
                    _uiProjectProxyList.Add(proxyProject);
                }
            });
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var proxy = ((MenuItem) sender).DataContext as ProjectProxy;

                var projectTitle =
                    PromptDialog.Prompt("Type the name of this project (to be sure you delete the right project)",
                        "Delete project");

                if (string.IsNullOrWhiteSpace(projectTitle) || !projectTitle.Equals(proxy?.Title)) 
                    return;
                
                var projManager = new ProjectManager();
                projManager.DeleteById(proxy.Id);

                _uiProjectProxyList.Remove(proxy);
            });
        }

        private void EditProject(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var proxy = ((MenuItem) sender).DataContext as ProjectProxy;

                var projectTitle = PromptDialog.Prompt("New project name", "Edit project name");
                if (string.IsNullOrWhiteSpace(projectTitle) || projectTitle.Equals(proxy?.Title)) 
                    return;

                proxy.Title = projectTitle;

                var projManager = new ProjectManager();
                projManager.Update(ModelConverter.ProjectProxyToModel(proxy));

                _uiProjectProxyList.Remove(proxy);
                _uiProjectProxyList.Add(proxy);
            });
        }

        #endregion

        #region Area CRUD

        private void AddArea(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var areaTitle = PromptDialog.Prompt("Area name", "Create new area");
                var proxy = ((MenuItem) sender).DataContext as ProjectProxy;

                if (!string.IsNullOrWhiteSpace(areaTitle))
                {
                    var areaManager = new AreaManager();
                    var areaProxy = ProxyConverter.AreaModelToProxy(areaManager.Create(areaTitle, proxy.Id));
                    proxy.Areas.Add(areaProxy);
                }
            });
        }

        private void EditArea(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var areaProxy = ((MenuItem) sender).DataContext as AreaProxy;

                var areaTitle = PromptDialog.Prompt("New area name", "Edit area name");
                if (!string.IsNullOrWhiteSpace(areaTitle) && !areaTitle.Equals(areaProxy.Title))
                {
                    var projectProxy = _uiProjectProxyList
                        .FirstOrDefault(proj => proj.Areas.Any(a => a.Id == areaProxy.Id));

                    if (projectProxy != null)
                    {
                        areaProxy.Title = areaTitle;

                        var areaManager = new AreaManager();
                        areaManager.Update(ModelConverter.AreaProxyToModel(areaProxy));

                        projectProxy.Areas.Remove(areaProxy);
                        projectProxy.Areas.Add(areaProxy);
                    }
                }
            });
        }

        private void DeleteArea(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var areaTitle = PromptDialog.Prompt("Type the name of this area (to be sure you delete the right area)",
                    "Delete area");

                if (((MenuItem) sender).DataContext is AreaProxy proxy && !string.IsNullOrWhiteSpace(areaTitle) &&
                    areaTitle.Equals(proxy.Title))
                {
                    var projectProxy = _uiProjectProxyList
                        .FirstOrDefault(proj => proj.Areas.Any(a => a.Id == proxy.Id));

                    if (projectProxy != null)
                    {
                        var areaManager = new AreaManager();
                        areaManager.DeleteById(proxy.Id);

                        projectProxy.Areas.Remove(proxy);
                    }
                }
            });
        }

        #endregion

        #region TestCase CRUD

        private void CreateTestCase(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var areaproxy = ((MenuItem) sender).DataContext as AreaProxy;
                var createdTestCaseProxy = TestCaseDialog.Prompt(areaproxy);

                if (createdTestCaseProxy != null) areaproxy?.TestCasesList.Add(createdTestCaseProxy);
            });
        }

        private void EditTestCase(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var testCase = ((MenuItem) sender).DataContext as TestCaseProxy;
                EditTestCaseFromProxy(testCase);
            });
        }

        private void EditTestCase_Click(object sender, MouseButtonEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
                {
                    var testCase = ((TextBlock) sender).DataContext as TestCaseProxy;
                    EditTestCaseFromProxy(testCase);
                }
            });
        }

        private void EditTestCaseFromProxy(TestCaseProxy testCase)
        {
            RunWithOverlayer(() =>
            {
                var editedTestCase = TestCaseDialog.Prompt(testCase);
                if (editedTestCase != null)
                {
                    var projectProxy = _uiProjectProxyList
                        .FirstOrDefault(proj => proj.Areas.Any(a => a.Id == testCase.AreaId));

                    if (_uiProjectProxyList != null)
                    {
                        var areaProxy = projectProxy?.Areas.FirstOrDefault(a => a.Id == testCase.AreaId);
                        if (areaProxy != null)
                        {
                            areaProxy.TestCasesList.Remove(testCase);
                            areaProxy.TestCasesList.Add(editedTestCase);

                            SetCurrentTestCase(editedTestCase);
                        }
                    }
                }
            });
        }

        private void DeleteTestCase(object sender, RoutedEventArgs e)
        {
            RunWithOverlayer(() =>
            {
                var manager = new TestManager();
                if (((MenuItem) sender).DataContext is TestCaseProxy testCaseToDelete)
                {
                    manager.DeleteById(testCaseToDelete.Id);

                    var projectProxy = _uiProjectProxyList
                        .FirstOrDefault(proj => proj.Areas.Any(a => a.Id == testCaseToDelete.AreaId));

                    var areaProxy = projectProxy?.Areas.FirstOrDefault(a => a.Id == testCaseToDelete.AreaId);
                    if (areaProxy != null)
                    {
                        areaProxy.TestCasesList.Remove(testCaseToDelete);
                        SetCurrentTestCase(new TestCaseProxy());
                    }
                }
            });
        }

        #endregion
    }
}