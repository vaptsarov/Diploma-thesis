namespace TestCaseManager.Views.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Core.Managers;
    using DB;
    using Models;

    /// <summary>
    ///     Interaction logic for ManageUsersPage.xaml
    /// </summary>
    public partial class ManageUsersPage : UserControl
    {
        private ApplicationUser _selectedUser;
        private ICollection<ApplicationUser> _users;

        public ManageUsersPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var manager = new UserManager();
                _users = manager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                Dispatcher.Invoke(() =>
                {
                    foreach (var user in _users)
                        AutoCompleteBox.AddItem(new AutoCompleteModel(user.Username, user.Username));
                });
            });
        }

        private void SelectUser_Click(object sender, RoutedEventArgs e)
        {
            MessageLabel.Content = string.Empty;

            var username = AutoCompleteBox.Text;
            if (string.IsNullOrWhiteSpace(username) == false)
                if (_users.Any(user => user.Username.Equals(username)))
                {
                    _selectedUser = _users.First(user => user.Username.Equals(username));
                    Username.Text = _selectedUser.Username;
                    IsAdminCheckBox.IsChecked = _selectedUser.IsAdmin;
                }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            var username = Username.Text;
            if (string.IsNullOrWhiteSpace(username) == false && _selectedUser != null)
            {
                var manager = new UserManager();
                var updatedUser = manager.UpdateUser(_selectedUser.UserId, username, Password.SecurePassword,
                    IsAdminCheckBox.IsChecked.Value);

                _users.Remove(_selectedUser);
                _users.Add(updatedUser);
                _selectedUser = updatedUser;

                MessageLabel.Content = "User is successfully updated.";
                MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(0, 204, 0));

                ClearFields();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser == null) 
                return;

            var manager = new UserManager();
            manager.DeleteUser(_selectedUser.UserId);

            _users.Remove(_selectedUser);
            AutoCompleteBox.RemoveItem(_selectedUser.Username);
            _selectedUser = null;

            ClearFields();
        }

        private void ClearFields()
        {
            Username.Text = string.Empty;
            IsAdminCheckBox.IsChecked = false;
            Password.Clear();
        }
    }
}