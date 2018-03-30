using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using TestCaseManager.GitHub;
using TestCaseManager.Utilities;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for GitHubRepoSelectorDialog.xaml
    /// </summary>
    public partial class GitHubRepoSelectorDialog : Window
    {
        private IssueManager manager;
        private static int TestCaseId;

        public GitHubRepoSelectorDialog()
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;
            this.DragWindow.MouseLeftButtonDown += new MouseButtonEventHandler(this.AttachDragDropEvent);
            this.BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            this.Topmost = true;
        }

        public static string Prompt(int testCaseId)
        {
            GitHubRepoSelectorDialog inst = new GitHubRepoSelectorDialog();
            TestCaseId = testCaseId;

            inst.ShowDialog();

            return null;
        }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadRepositories_Click(object sender, RoutedEventArgs e)
        {
            this.InvalidCredentials.Visibility = Visibility.Hidden;
            this.GitHubRepositoryList.ItemsSource = null;

            if (!(string.IsNullOrWhiteSpace(this.GitHubUsername.Text) || this.GitHubPassword.SecurePassword.Length == 0))
            {
                manager = IssueManager.Instance(this.GitHubUsername.Text, this.GitHubPassword.SecurePassword);
                List<Tuple<string, string>> list = null;
                bool isAuthenticationCorrect = true;

                Task task = Task.Factory.StartNew(() =>
                {
                    Action action = () => list = manager.GetRepositories();
                    isAuthenticationCorrect = AreCredentialsCorrectForAction(action);

                });
                task.ContinueWith(next =>
                {
                    // Update the main Thread as it is the owner of the UI elements
                    this.Dispatcher.Invoke((Action)(() =>
                        {
                            if (isAuthenticationCorrect)
                            {
                                this.GitHubRepositoryList.SelectedIndex = 0;
                                this.GitHubRepositoryList.ItemsSource = list;
                                this.CreateIssue.IsEnabled = true;
                            }
                            else
                            {
                                this.InvalidCredentials.Visibility = Visibility.Visible;
                                this.CreateIssue.IsEnabled = false;
                            }
                        }));
                });
            }
        }

        private void CreateIssue_Click(object sender, RoutedEventArgs e)
        {
            Tuple<string, string> selectedItem = this.GitHubRepositoryList.SelectedItem as Tuple<string, string>;
            Uri createdIssueUri = null;
            if (selectedItem != null)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    createdIssueUri = this.manager.CreateIssue(selectedItem.Item1, selectedItem.Item2, TestCaseId);
                });
                task.ContinueWith(next =>
                {
                    // Update the main Thread as it is the owner of the UI elements
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.IssueRegistrationPanel.Visibility = Visibility.Hidden;
                        this.GitHubSuccessfulyCreatedPanel.Visibility = Visibility.Visible;

                        this.CreatedLabel.Margin = new Thickness(200, 5, 10, 20);
                        this.CreatedLabel.Width = 180;

                        this.CopyToClipboard.Width = 160;
                        this.CopyToClipboard.Height = 30;
                        this.CopyToClipboard.Margin = new Thickness(0,-11,0,0);

                        this.CopyToClipboard.ToolTip = createdIssueUri.OriginalString;
                        this.ClipboardUrl.NavigateUri = createdIssueUri;
                    }));
                });
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(this.ClipboardUrl.NavigateUri.ToString());

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.CancelDialog();
        }

        private void CancelDialog()
        {
            this.Close();
        }

        private bool AreCredentialsCorrectForAction(Action action)
        {
            bool areCorrect = true;
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToDetailedString().Contains("Bad credentials"))
                {
                    areCorrect = false;
                }
            }

            return areCorrect;
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.CopyToClipboard.ToolTip.ToString());

            this.Close();
        }
    }
}
