using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using QSync.Utils;

namespace QSync.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Command LoginCommand { get; set; }

        public UserManager UserManager { get; } = UserManager.Instance;

        private void Login()
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("User name field is empty");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Password field is empty");
                return;
            }
            if (UserManager.Login(Username, Password))
            {
                MessageBox.Show("Login successful");
                new QSync.MainWindow().Show();
            }
            else
            {
                MessageBox.Show("No user found in database");
                return;
            } 
            

            /*   if (UserManager.CurrentUser.IsAdmin)
               {
                   new AdminWindow().Show();
               } */

        }
        public event PropertyChangedEventHandler PropertyChanged;

     /*   [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } */
    }
}
