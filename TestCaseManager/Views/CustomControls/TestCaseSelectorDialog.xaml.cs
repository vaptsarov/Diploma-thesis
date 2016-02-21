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
    public partial class TestCaseSelectorDialog : Window
    {
        private readonly ObservableCollection<StepDefinitionProxy> stepDefinitionList = new ObservableCollection<StepDefinitionProxy>();

        public TestCaseSelectorDialog()
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

        private void ProjectSelected_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object currentSelectedItem = this.projects.SelectedItem;
            switch (currentSelectedItem.GetType().Name.ToLower())
            {
                case "testcaseproxy":
                    {
                        //this.SetCurrentTestCase(currentSelectedItem);
                        break;
                    }
                default:
                    break;
            }
        }

        public static void Prompt()
        {
            TestCaseSelectorDialog inst = new TestCaseSelectorDialog();
            inst.ShowDialog();
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
        }

        private void SetCurrentAccentColor()
        {
            this.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            // If theme set is light version, the font color should be black, if dark - should be white.
            if (AppearanceManager.LightThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            }
            else if (AppearanceManager.DarkThemeSource != AppearanceManager.Current.ThemeSource)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }
    }
}
