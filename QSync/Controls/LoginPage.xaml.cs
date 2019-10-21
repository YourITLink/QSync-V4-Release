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
using log4net;


namespace QSync.Controls
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        AbleDBEntity context = new AbleDBEntity();
        AbleDBEntity _db = new AbleDBEntity();
        public LoginPage()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log.Info("in Controls/LoginPage.xaml");
            DataContext = this;
            txtUserName.Text = Properties.Settings.Default.MyUser;
            this.Closed += new EventHandler(LoginWindow_Closed);
            // Setting Focus on textBox on startup
            FocusManager.SetFocusedElement(this, txtPassword);


        }
        void LoginWindow_Closed(object sender, EventArgs e)
        {
            log.Info("on closed - application.current.shutdown");
            Application.Current.Shutdown();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Cancel login");
            Analytics.TrackEvent("Login - User Cancel");
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Data.CollectionViewSource staffViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("staffViewSource")));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message Box" + ex.ToString(), "No database connection found");
            }
        }
        private void LoginBtn1_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Attempt Login");
            Analytics.TrackEvent("Login - Login Attempt");
            using (var context = new AbleDBEntity())
            {
                // Load all current team members 
                var cudb = context.staffs.Where(a => a.GrantAccess == 1)
                             .ToList();
                var user = cudb.Where(i => i.UserName == this.txtUserName.Text).FirstOrDefault();
                if (user == null)
                {
                    log.Info("Login failure - username error");
                    Analytics.TrackEvent("Login - Incorrect Username Entry");
                    MessageBox.Show("Please check your Username (this is your email address) is correct - Remember usernames are case sensitive", 
                        "USERNAME Incorrect", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else if (this.txtUserName.Text == user.UserName && this.txtPassword.Password == user.Password)
                {
                    log.Info("User settings download");
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
                    log.Info("Login Error - User details fail");
                    Analytics.TrackEvent("Login - Incorrect Password Entry");
                    MessageBox.Show("Please check your password is correct - Remember passwords are case sensitive", "PASSWORD Incorrect");
                }

            }


        }     
    }
}
