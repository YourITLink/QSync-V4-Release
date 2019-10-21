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
using QSync.Controls;
using System.Data.Entity;


namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for SearchQuotes.xaml
    /// </summary>
    public partial class SearchQuotes : Window
    {
        public static int qnLoad;
        AbleDBEntity context = new AbleDBEntity();
        CollectionViewSource qtViewSource;
        AbleDBEntity _db = new AbleDBEntity();
        public SearchQuotes()
        {
            InitializeComponent();
            qtViewSource = ((CollectionViewSource)(FindResource("quoteViewSource")));
            Footer.Content = new ControlFooter();
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource quoteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("quoteViewSource")));
            context.quotes.Load();
            qtViewSource.Source = context.quotes.Local;
        }

        //LHS btn commands
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {  
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        { 
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void DeleteCustomerCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void CommitQuoteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }
        
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void NewOrder_click(object sender, RoutedEventArgs e)
        {
        }
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void Delete_QItem(invitem invitem)
        {
        }

        private void DeleteOrderCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
        }

        private void QuoteitemsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

        private void Quoteinfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
            var mw = new QSync.MainWindow();
            mw.Show();
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
    }
}
