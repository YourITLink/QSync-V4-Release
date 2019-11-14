using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Microsoft.AppCenter.Analytics;


namespace QSync.Controls
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        
        AbleDBEntity context = new AbleDBEntity();
     //   CollectionViewSource stViewSource;
        AbleDBEntity _db = new AbleDBEntity();
        public LoginPage()
        {
            InitializeComponent();
         //   stViewSource = ((CollectionViewSource)(FindResource("staffViewSource")));
            DataContext = this;
            txtUserName.Text = Properties.Settings.Default.MyUser;
            this.Closed += new EventHandler(LoginWindow_Closed);

        }
        void LoginWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Login - User Cancel");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource staffViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("staffViewSource")));
        }
        AbleDBEntity ef = new AbleDBEntity();
        private void LoginBtn1_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Login - Login Attempt");
            using (var context = new AbleDBEntity())
            {
                // Load all staff who are current ONLY 
                var cudb = context.staffs.Where(a => a.GrantAccess == 1)
                             .ToList();
                var user = cudb.Where(i => i.UserName == this.txtUserName.Text).FirstOrDefault();
                if (user == null)
                {
                    Analytics.TrackEvent("Login - Incorrect Username Entry");
                    MessageBox.Show("Please check your Username (this is your email address) is correct - Remember usernames are case sensitive", "USERNAME Incorrect");
                }
                else if (this.txtUserName.Text == user.UserName && this.txtPassword.Password == user.Password)
                {
                    Analytics.TrackEvent("Login - Successful Login to QSync");
                    Properties.Settings.Default.MyUser = txtUserName.Text;
                    Properties.Settings.Default.Name = user.Name;
                    Properties.Settings.Default.Accesslevel = user.QSyncAccess;
                    Properties.Settings.Default.Qid = user.QuoteID;
                    Properties.Settings.Default.Save();
                    string currentUser = txtUserName.Text;
                    string passAccessLevel = user.QSyncAccess;
                    string passName = user.Name;
                    string currentQid = user.QuoteID;
                    var Home = new QSync.MainWindow(currentUser, passAccessLevel, passName, currentQid);
                    Home.DataContext = this;
                    this.Visibility = Visibility.Hidden;
                    Home.Show();

                }
                else
                {
                    Analytics.TrackEvent("Login - Incorrect Password Entry");
                    MessageBox.Show("Please check your password is correct - Remember passwords are case sensitive", "PASSWORD Incorrect");
                }

            }


        }     
    }
}
