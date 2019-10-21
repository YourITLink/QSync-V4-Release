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
    /// Interaction logic for Invoices.xaml
    /// </summary>
    /// 
    public partial class Invoices : Window
    {

        AbleDBEntity context = new AbleDBEntity();
        CollectionViewSource invViewSource;
        CollectionViewSource invitmViewSource;
        AbleDBEntity _db = new AbleDBEntity();

        public Invoices()
        {
            InitializeComponent();
            invViewSource = ((CollectionViewSource)(FindResource("invoiceViewSource")));
            invitmViewSource = ((CollectionViewSource)(FindResource("invoiceinvitemsViewSource")));
            Footer.Content = new ControlFooter();
            DataContext = this;
            this.Closed += new EventHandler(Window_Closed);
            

        }
        void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource invoiceViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("invoiceViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // invoiceViewSource.Source = [generic data source]
            context.invoices.Load();

            invViewSource.Source = context.invoices.Local;
            invViewSource.View.MoveCurrentToLast();

        }

        //Commands run from this point
        //The commands will need to be arranged and re-used for the project

        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            invViewSource.View.MoveCurrentToLast();
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            invViewSource.View.MoveCurrentToPrevious();
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            invViewSource.View.MoveCurrentToNext();
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            invViewSource.View.MoveCurrentToFirst();
        }

        private void DeleteCustomerCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            invViewSource.View.MoveCurrentToPrevious();
            invViewSource.View.MoveCurrentToNext();
        }
        private void CommitInvoiceCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            int acct;
            int.TryParse(add_originv.Text, out acct);
            if (newInvoiceDataGrid.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.  
                invoice newInvoice = new invoice()
                {
                    OrigInvNumber = acct,
                    AccountNo = add_accountno.Text,
                    CustEmail = add_custemail.Text,
                    CompCust = add_company.Text,
                    Address = add_address.Text,
                    SAddress = add_saddress.Text,
                    InvoiceDate = add_invdate.SelectedDate,
                    CustomerOrderNo = add_custorder.Text,
                    Contact = add_contact.Text,
                    Phone = add_phone.Text,
                    InvCode = add_invCodeComboBox.Text,
                    User = add_user.Text
                };
                {
                    context.invoices.Local.Add(newInvoice);
                    invViewSource.View.Refresh();
                    invViewSource.View.MoveCurrentTo(newInvoice);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newInvoiceDataGrid.Visibility = Visibility.Collapsed;
                //Collapse Commit Btn/Show Save Btn
                btnUpdate.Visibility = Visibility.Visible;
                btnCommit.Visibility = Visibility.Collapsed;
                existingInvoiceDataGrid.Visibility = Visibility.Visible;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            invViewSource.View.MoveCurrentToPrevious();
            invViewSource.View.MoveCurrentToNext();
        }
        private void CommitInvoice_Click(object sender, ExceptionRoutedEventArgs e)
        {
            
        }
        // Commit changes from the new customer form, the new order form,  
        // or edits made to the existing customer form.  
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            context.SaveChanges();
        }

        // Sets up the form so that user can enter data. Data is later  
        // saved when user clicks Commit.  
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //First set the view to reset the form inform
            existingInvoiceDataGrid.Visibility = Visibility.Collapsed;
            invitemsDataGrid.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnCommit.Visibility = Visibility.Visible;
            newInvoiceDataGrid.Visibility = Visibility.Visible;
     //       newCustomerGrid.Visibility = Visibility.Visible;

            // Clear all the text boxes before adding a new customer.  
            foreach (var child in newInvoiceDataGrid.Children)
            {
                var tb = child as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }


        private void NewOrder_click(object sender, RoutedEventArgs e)
        {
            //       var cust = custViewSource.View.CurrentItem as Customer;
            //       if (cust == null)
            //       {
            //           MessageBox.Show("No customer selected.");
            //           return;
            //       }

            //       Order newOrder = new Order();
            //       newOrder.CustomerID = cust.CustomerID;

            // Get address and other mostly constant fields from   
            // an existing order, if one exists  
            //       var coll = custViewSource.Source as IEnumerable<Customer>;
            //       var lastOrder = (from c in coll
            //                        from ord in c.Orders
            //                        select ord).LastOrDefault();
            //       if (lastOrder != null)
            //       {
            //           newOrder.ShipAddress = lastOrder.ShipAddress;
            //           newOrder.ShipCity = lastOrder.ShipCity;
            //           newOrder.ShipCountry = lastOrder.ShipCountry;
            //           newOrder.ShipName = lastOrder.ShipName;
            //           newOrder.ShipPostalCode = lastOrder.ShipPostalCode;
            //           newOrder.ShipRegion = lastOrder.ShipRegion;
            //       }

            //       existingCustomerGrid.Visibility = Visibility.Collapsed;
            //       newCustomerGrid.Visibility = Visibility.Collapsed;
            //       newOrderGrid.UpdateLayout();
            //       newOrderGrid.Visibility = Visibility.Visible;
        }

        // Cancels any input into the new customer form  
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //           add_addressTextBox.Text = "";
            //           add_cityTextBox.Text = "";
            //           add_companyNameTextBox.Text = "";
            //           add_contactNameTextBox.Text = "";
            //          add_contactTitleTextBox.Text = "";
            //         add_countryTextBox.Text = "";
            //           add_customerIDTextBox.Text = "";
            //           add_faxTextBox.Text = "";
            //           add_phoneTextBox.Text = "";
            //           add_postalCodeTextBox.Text = "";
            //           add_regionTextBox.Text = "";
            //
            //           existingCustomerGrid.Visibility = Visibility.Visible;
            //           newCustomerGrid.Visibility = Visibility.Collapsed;
            //           newOrderGrid.Visibility = Visibility.Collapsed;
        }

        private void Delete_QItem(invitem invitem)
        {

        }

        private void DeleteOrderCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            // Get the Order in the row in which the Delete button was clicked.  
            //       Order obj = e.Parameter as Order;
            //       Delete_Order(obj);
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            int acct;
            int.TryParse(add_originv.Text, out acct);
            if (newInvoiceDataGrid.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.  
                invoice newInvoice = new invoice()
                {
                    OrigInvNumber = acct,
                    AccountNo = add_accountno.Text,
                    CustEmail = add_custemail.Text,
                    CompCust = add_company.Text,
                    Address = add_address.Text,
                    SAddress = add_saddress.Text,
                    InvoiceDate = add_invdate.SelectedDate,
                    CustomerOrderNo = add_custorder.Text,
                    Contact = add_contact.Text,
                    Phone = add_phone.Text,
                    InvCode = add_invCodeComboBox.Text,
                    User = add_user.Text
                };
                {
                    context.invoices.Local.Add(newInvoice);
                    invViewSource.View.Refresh();
                    invViewSource.View.MoveCurrentTo(newInvoice);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newInvoiceDataGrid.Visibility = Visibility.Collapsed;
                invitemsDataGrid.Visibility = Visibility.Visible;
                existingInvoiceDataGrid.Visibility = Visibility.Visible;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            invViewSource.View.MoveCurrentToPrevious();
            invViewSource.View.MoveCurrentToNext();
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

        private void Add_invCodeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("ADJ");
            data.Add("OC");
            data.Add("PP");
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void Add_invCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
