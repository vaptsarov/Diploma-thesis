using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using FirstFloor.ModernUI.Presentation;
using TestCaseManager.GitHub;
using TestCaseManager.Utilities;
using TestCaseManager.Utilities.StringUtility;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    ///     Interaction logic for GitHubRepoSelectorDialog.xaml
    /// </summary>
    public partial class GitHubRepoSelectorDialog : Window
    {
        private static int _testCaseId;
        private IssueManager _manager;

        public GitHubRepoSelectorDialog()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            DragWindow.MouseLeftButtonDown += AttachDragDropEvent;
            BorderBrush = new SolidColorBrush(AppearanceManager.Current.AccentColor);

            Topmost = true;
        }

        public static string Prompt(int testCaseId)
        {
            var inst = new GitHubRepoSelectorDialog();
            _testCaseId = testCaseId;

            inst.ShowDialog();

            return null;
        }

        private void AttachDragDropEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoadRepositories_Click(object sender, RoutedEventArgs e)
        {
            InvalidCredentials.Visibility = Visibility.Hidden;
            GitHubRepositoryList.ItemsSource = null;

            if (!(string.IsNullOrWhiteSpace(GitHubUsername.Text) || GitHubPassword.SecurePassword.Length == 0))
            {
                _manager = IssueManager.Instance(GitHubUsername.Text, GitHubPassword.SecurePassword);
                List<Tuple<string, string>> list = null;
                var isAuthenticationCorrect = true;

                var task = Task.Factory.StartNew(() =>
                {
                    Action action = () => list = _manager.GetRepositories();
                    isAuthenticationCorrect = AreCredentialsCorrectForAction(action);
                });
                task.ContinueWith(next =>
                {
                    // Update the main Thread as it is the owner of the UI elements
                    Dispatcher.Invoke(() =>
                    {
                        if (isAuthenticationCorrect)
                        {
                            GitHubRepositoryList.SelectedIndex = 0;
                            GitHubRepositoryList.ItemsSource = list;
                            CreateIssue.IsEnabled = true;
                        }
                        else
                        {
                            InvalidCredentials.Visibility = Visibility.Visible;
                            CreateIssue.IsEnabled = false;
                        }
                    });
                });
            }
        }

        private void CreateIssue_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GitHubRepositoryList.SelectedItem as Tuple<string, string>;
            Uri createdIssueUri = null;
            if (selectedItem != null)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    createdIssueUri = _manager.CreateIssue(selectedItem.Item1, selectedItem.Item2, _testCaseId);
                });
                task.ContinueWith(next =>
                {
                    // Update the main Thread as it is the owner of the UI elements
                    Dispatcher.Invoke(() =>
                    {
                        IssueRegistrationPanel.Visibility = Visibility.Hidden;
                        GitHubSuccessfulyCreatedPanel.Visibility = Visibility.Visible;

                        CreatedLabel.Margin = new Thickness(200, 5, 10, 20);
                        CreatedLabel.Width = 180;

                        CopyToClipboard.Width = 160;
                        CopyToClipboard.Height = 30;
                        CopyToClipboard.Margin = new Thickness(0, -11, 0, 0);

                        CopyToClipboard.ToolTip = createdIssueUri.OriginalString;
                        ClipboardUrl.NavigateUri = createdIssueUri;
                    });
                });
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(ClipboardUrl.NavigateUri.ToString());

            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            CancelDialog();
        }

        private void CancelDialog()
        {
            Close();
        }

        private bool AreCredentialsCorrectForAction(Action action)
        {
            var areCorrect = true;
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToDetailedString().Contains("Bad credentials")) areCorrect = false;
            }

            return areCorrect;
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CopyToClipboard.ToolTip.ToString());

            Close();
        }
    }
}