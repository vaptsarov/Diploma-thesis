﻿using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCaseManager.Core;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.Views.CustomControls;

namespace TestCaseManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindowTestRuns.xaml
    /// </summary>
    public partial class MainWindowTestRuns : UserControl
    {
        private ObservableCollection<TestRunProxy> UITestRunList = new ObservableCollection<TestRunProxy>();
        private readonly Timer dbCallback = new Timer();

        public MainWindowTestRuns()
        {
            this.InitializeComponent();
            AppearanceManager.Current.PropertyChanged += this.OnAppearanceManagerPropertyChanged;
        }

        private void MainWindowTestRuns_Loaded(object sensder, RoutedEventArgs e)
        {
            // Initial DB data retrieve
            Task task = Task.Factory.StartNew(() =>
            {
                TestRunProxyManager manager = new TestRunProxyManager();
                this.UITestRunList = new ObservableCollection<TestRunProxy>(manager.GetAll().OrderBy(x=>x.Name));
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.SetCurrentAccentColor();
                    this.TestRunListBox.ItemsSource = this.UITestRunList;

                    this.MainTable.Visibility = Visibility.Visible;
                    this.progressBar.Visibility = Visibility.Hidden;
                }));
            });
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
            this.TestRunBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.CreateTestRunButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.TestCasesBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseTotal.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCasePassed.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseFailed.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseNotRan.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseCreatedBy.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.BorderTestCaseCreatedOn.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.TestCasesListBorder.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.AddTestsButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);
            this.RunButton.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            // If theme set is light version, the font color should be black, if dark - should be white.
            Color white = Color.FromRgb(255, 255, 255);
            Color semiDark = Color.FromRgb(51, 51, 51);
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.TestCasesList.Foreground = new SolidColorBrush(white);
                this.TestRunBorder.Background = new SolidColorBrush(semiDark);
                this.TestCasesBorder.Background = new SolidColorBrush(semiDark);
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.TestCasesList.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.TestRunBorder.Background = new SolidColorBrush(white);
                this.TestCasesBorder.Background = new SolidColorBrush(white);
            }
        }

        private void AddTestRun(object sender, RoutedEventArgs e)
        {
            string projectTitle = PromptDialog.Prompt("Test Run name", "Create new test run");
            if (!string.IsNullOrWhiteSpace(projectTitle))
            {
                TestRunManager runManager = new TestRunManager();
                TestRunProxy proxyProject = ProxyConverter.TestRunModelToProxy(runManager.Create(projectTitle));
                this.UITestRunList.Add(proxyProject);
            }
        }

        private void AddTests(object sender, RoutedEventArgs e)
        {
            TestRunProxy selectedTestRun = this.TestRunListBox.SelectedItem as TestRunProxy;
            if (selectedTestRun != null)
            {
                TestCaseSelectorDialog.Prompt(selectedTestRun.ID);
                this.UpdateTestRun(selectedTestRun);
            }
        }

        private void RunTests(object sender, RoutedEventArgs e)
        {
            TestRunProxy selectedTestRun = this.TestRunListBox.SelectedItem as TestRunProxy;
            if (selectedTestRun != null)
            {
                TestCaseRunDialog.Prompt(selectedTestRun.ID);
                this.UpdateTestRun(selectedTestRun);
            }
        }

        private void UpdateTestRun(TestRunProxy selectedTestRun)
        {
            TestRunProxyManager manager = new TestRunProxyManager();
            TestRunProxy updatedTestRun = manager.GetById(selectedTestRun.ID);

            selectedTestRun.TestCasesList = updatedTestRun.TestCasesList;
            this.OnSelectedItem(this, null);
        }

        private void OnSelectedItem(object sender, SelectionChangedEventArgs args)
        {
            TestRunProxy currentSelectedItem = this.TestRunListBox.SelectedItem as TestRunProxy;
            if (currentSelectedItem != null)
            {
                this.TotalLabel.Content = currentSelectedItem.TestCasesList.Count;
                this.PassedLabel.Content = currentSelectedItem.TestCasesList.Where(x => x.Status.Equals(Status.Passed)).Count();
                this.FailedLabel.Content = currentSelectedItem.TestCasesList.Where(x => x.Status.Equals(Status.Failed)).Count();
                this.NotRanLabel.Content = currentSelectedItem.TestCasesList.Where(x => x.Status.Equals(Status.NotExecuted)).Count();
                this.CreatedBy.Content = currentSelectedItem.CreatedBy;
                this.CreatedOn.Content = currentSelectedItem.CreatedOn;

                this.TestCasesList.ItemsSource = currentSelectedItem.TestCasesList;
            }     
        }

        private void SelectWholeLine(object sender, RoutedEventArgs e)
        {
            var initialSelectedItem = sender as DependencyObject;
            while (initialSelectedItem != null)
            {
                if (initialSelectedItem is ListBoxItem)
                {
                    var selectedListBoxItem = (initialSelectedItem as ListBoxItem);
                    this.TestRunListBox.SelectedItem = selectedListBoxItem.Content;

                    break;
                }

                initialSelectedItem = VisualTreeHelper.GetParent(initialSelectedItem);
            }
        }
    }
}
