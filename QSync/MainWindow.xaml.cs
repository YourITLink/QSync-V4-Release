using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QSync.Controls;
using System.Configuration;
using Microsoft.AppCenter.Analytics;
using MySql.Data.MySqlClient;


namespace QSync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string btn;
        public MainWindow()
        {
            InitializeComponent();
            Footer.Content = new ControlFooter();
            loadUserMenu();
            Title = "QSync V4.0 by Your IT Link | Able Door Services (NSW) Pty Ltd | by Your IT Link";
            lblLocation.Text = "Main Menu";
            title.Text = "Welcome to QSync - Able Door Services";
            Properties.Settings.Default.Status = "Main Menu - QSync - Able Door Services";
            Properties.Settings.Default.Save();
            this.Closed += new EventHandler(MainWindow_Closed);

        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public MainWindow(string currentUser, string currentAcclvl, string currentName, string currentQid)
        {
            InitializeComponent();
            var LoginPage = this.DataContext;
            lblLocation.Text = "Main Menu";
            Footer.Content = new ControlFooter();
            loadUserMenu();
            Title = "QSync V4.0 by Your IT Link | Able Door Services (NSW) Pty Ltd | by Your IT Link";
            lblLocation.Text = "Main Menu";
            title.Text = "Welcome to QSync - Able Door Services";
            //Update status text
            Properties.Settings.Default.Status = "Welcome to QSync - For Support Call - 0410-669-634";
            Properties.Settings.Default.Save();
         //   var iconHandle = QSync.Properties.Resources.QSync_Q.GetHicon();
        //    this.notifyIcon.Icon = System.Drawing.Icon.FromHandle(iconHandle);
        }

        #region ROLE BASED MENU LOADING - loadUserMenu();

        public void loadUserMenu()
        {
            if (Properties.Settings.Default.Accesslevel == "Power")
            {
                fileQuotes.Visibility = Visibility.Visible;
                fileService.Visibility = Visibility.Visible;
                fileInvoices.Visibility = Visibility.Visible;
                fileShutter.Visibility = Visibility.Visible;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Visible;
                fileDev.Visibility = Visibility.Visible;
            }
            else if (Properties.Settings.Default.Accesslevel == "Manager")
            {
                fileQuotes.Visibility = Visibility.Visible;
                fileService.Visibility = Visibility.Visible;
                fileInvoices.Visibility = Visibility.Visible;
                fileShutter.Visibility = Visibility.Visible;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Visible;
                fileDev.Visibility = Visibility.Collapsed;
            }
            else if (Properties.Settings.Default.Accesslevel == "Sales")
            {
                fileQuotes.Visibility = Visibility.Visible;
                fileService.Visibility = Visibility.Collapsed;
                fileInvoices.Visibility = Visibility.Collapsed;
                fileShutter.Visibility = Visibility.Visible;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Collapsed;
                fileDev.Visibility = Visibility.Collapsed;
            }
            else if (Properties.Settings.Default.Accesslevel == "Accounts")
            {
                fileQuotes.Visibility = Visibility.Collapsed;
                fileService.Visibility = Visibility.Collapsed;
                fileInvoices.Visibility = Visibility.Visible;
                fileShutter.Visibility = Visibility.Collapsed;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Collapsed;
                fileDev.Visibility = Visibility.Collapsed;
            }
            else if (Properties.Settings.Default.Accesslevel == "Service")
            {
                fileQuotes.Visibility = Visibility.Visible;
                fileService.Visibility = Visibility.Visible;
                fileInvoices.Visibility = Visibility.Collapsed;
                fileShutter.Visibility = Visibility.Visible;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Collapsed;
                fileDev.Visibility = Visibility.Collapsed;
            }
            else if (Properties.Settings.Default.Accesslevel == "Admin")
            {
                fileQuotes.Visibility = Visibility.Collapsed;
                fileService.Visibility = Visibility.Collapsed;
                fileInvoices.Visibility = Visibility.Collapsed;
                fileShutter.Visibility = Visibility.Collapsed;
                fileCustom.Visibility = Visibility.Visible;
                fileManager.Visibility = Visibility.Collapsed;
                fileDev.Visibility = Visibility.Collapsed;
            }
            else
            {
                fileQuotes.Visibility = Visibility.Collapsed;
                fileService.Visibility = Visibility.Collapsed;  
                fileInvoices.Visibility = Visibility.Collapsed;
                fileShutter.Visibility = Visibility.Collapsed;
                fileCustom.Visibility = Visibility.Collapsed;
                fileManager.Visibility = Visibility.Collapsed;
                fileDev.Visibility = Visibility.Collapsed;
            }

        }

        #endregion ROLE BASED MENU LOADING - END



        #region Application menu commands
        #region File

        public void CloseQSync_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void showSettings_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        public void Save_data()
        {
            //this.Close();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var mw = new MainWindow();
            mw.Show();
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            var abt = new Views.About();
            abt.ShowDialog();
        }
        private void UserSettings_Click(object sender, RoutedEventArgs e)
        {
            var uc = new Controls.UserCustomisation();
            uc.Show();
        }

        #endregion File Menu

        #region Quotes Menu
        private void OpenQuotes_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }

        private void RunQuoteLoad()
        {
           
        }

        private void SearchQuotes_Click(object sender, RoutedEventArgs e)
        {
            
            this.Visibility = Visibility.Hidden;
            var sq = new Views.Quotes(btn);
            sq.Show();
        }
        private void AddQuote_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("File Menu Quotes_Click");
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void QSyncEmailLoad_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }

        #endregion Quotes Menu

        #region Service Menu

        private void OpenServices_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var sv = new Views.Servicing();
            sv.Show();
        }
        private void SearchServices_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var sv = new Views.Servicing();
            sv.Show();
        }
        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var sv = new Views.Servicing();
            sv.Show();
        }

        #endregion Service Menu

        #region Invoices Menu
        private void OpenInvoices_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var inv = new Views.Invoices();
            inv.Show();
        }
        private void SearchInvoices_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var inv = new Views.Invoices();
            inv.Show();
        }
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var inv = new Views.Invoices();
            inv.Show();
        }

        #endregion Invoices Menu

        #region ShutterPro Menu

        private void OpenShutters_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void SearchShutters_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void AddShutters_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }

        #endregion ShutterPro Menu

        #region Mgr Menu

        #endregion Mgr Menu

        #region Dev Menu

        private void dev1_Click(object sender, RoutedEventArgs e) //Load Email Window
        {
            //    this.Visibility = Visibility.Hidden;
            var em = new Controls.ProgEmail();
            em.Show();
        }
        private void dev2_Click(object sender, RoutedEventArgs e) //Load Build Changes Window
        {
            this.Visibility = Visibility.Hidden;
            var bc = new Views.BuildChanges();
            bc.Show();
        }
        private void dev3_Click(object sender, RoutedEventArgs e) //DB Backup
        {
            Analytics.TrackEvent("Dev Menu DB-Backup Click");
            string constring = Properties.Settings.Default.DBBackupConn;
            string file = Properties.Settings.Default.DBBacukup + "QSyncBackup.sql";

            try
            {using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "An error has occurred with the backup of the database. This message box will provide more information for a fix to be compiled.",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void dev4_Click(object sender, RoutedEventArgs e) //DB Restore
        {
            Analytics.TrackEvent("Dev Menu DB-Restore Click");
        }
        private void dev5_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Dev Menu DEV5 Click");
        }

        #endregion Dev Menu

        #region Window Specific Menu
        private void quote_MouseDown(object sender, MouseButtonEventArgs e)
        {
                Analytics.TrackEvent("Main Menu Quotes Click");
                qtbtn.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                var qt = new Views.Quotes();
                qt.Show();
                qtbtn.Visibility = Visibility.Visible;
        }
        private void service_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Analytics.TrackEvent("Main Menu Service Click");
            this.Visibility = Visibility.Hidden;
            var sv = new Views.Servicing();
            sv.Show();
        }
        private void invoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Analytics.TrackEvent("Main Menu Invoice Click");
            this.Visibility = Visibility.Hidden;
            var iv = new Views.Invoices();
            iv.Show();
        }
        private void schedule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Analytics.TrackEvent("Main Schedule Quotes Click");
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void findQuote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Analytics.TrackEvent("Main Find Quote Quotes Click");
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes(btn);
            qt.Show();
        }

        #endregion Window Specific Menu


        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            Save_data();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Save_data();
        }

        #endregion APPLICATION MENU COMMANDS - END
    }


}

