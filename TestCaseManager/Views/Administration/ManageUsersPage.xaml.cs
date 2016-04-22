using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using TestCaseManager.Core.Managers;
using TestCaseManager.DB;
using TestCaseManager.Models;
using System.Windows.Media;

namespace TestCaseManager.Views.Administration
{
    /// <summary>
    /// Interaction logic for ManageUsersPage.xaml
    /// </summary>
    public partial class ManageUsersPage : UserControl
    {
        private ICollection<ApplicationUser> users;
        private ApplicationUser selectedUser;

        public ManageUsersPage()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                UserManager manager = new UserManager();
                users = manager.GetAll();
            });
            task.ContinueWith(next =>
            {
                // Update the main Thread as it is the owner of the UI elements
                this.Dispatcher.Invoke((Action)(() =>
                {
                    foreach (var user in users)
                    {
                        this.AutoCompleteBox.AddItem(new AutoCompleteModel(user.Username, user.Username));
                    }
                }));
            });
        }

        private void SelectUser_Click(object sender, RoutedEventArgs e)
        {
            this.MessageLabel.Content = string.Empty;

            string username = this.AutoCompleteBox.Text;
            if (string.IsNullOrWhiteSpace(username) == false)
            {
                if (this.users.Any(user => user.Username.Equals(username)))
                {
                    selectedUser = this.users.Where(user => user.Username.Equals(username)).First();
                    this.Username.Text = selectedUser.Username;
                    this.IsAdminCheckBox.IsChecked = selectedUser.IsAdmin;
                }
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            string username = this.Username.Text;
            if(string.IsNullOrWhiteSpace(username) == false && this.selectedUser != null)
            {
                UserManager manager = new UserManager();
                var updatedUser = manager.UpdateUser(this.selectedUser.UserId, username, this.Password.SecurePassword, this.IsAdminCheckBox.IsChecked.Value);

                this.users.Remove(selectedUser);
                this.users.Add(updatedUser);
                this.selectedUser = updatedUser;

                this.MessageLabel.Content = "User is successfuly updated.";
                this.MessageLabel.Foreground = new SolidColorBrush(Color.FromRgb(0, 204, 0));

                this.ClearFields();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(this.selectedUser != null)
            {
                UserManager manager = new UserManager();
                manager.DeleteUser(this.selectedUser.UserId);

                this.users.Remove(selectedUser);
                this.AutoCompleteBox.RemoveItem(this.selectedUser.Username);
                this.selectedUser = null;

                this.ClearFields();
            }
        }

        private void ClearFields()
        {
            this.Username.Text = string.Empty;
            this.IsAdminCheckBox.IsChecked = false;
            this.Password.Clear();
        }
    }
}
