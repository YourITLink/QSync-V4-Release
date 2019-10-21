using System;
using System.Collections.Generic;
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

namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }
        // Application menu commands
        #region File

        private void CloseQSync_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        #endregion File Menu

        #region Quotes Menu
        private void OpenQuotes_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void SearchQuotes_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void AddQuote_Click(object sender, RoutedEventArgs e)
        {
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

        #endregion Dev Menu

        #region Page Specific

        private void buildchanges_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var bc = new Views.BuildChanges();
            bc.Show();
        }

        #endregion
    }
}
