using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSync.Utils
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action methodToExecute;
        private Func<bool> canExecuteEvaluator;
        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke();
        }

        public void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //        qtViewSource.View.MoveCurrentToLast();
        }
        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //        qtViewSource.View.MoveCurrentToPrevious();
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //       qtViewSource.View.MoveCurrentToNext();
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //       qtViewSource.View.MoveCurrentToFirst();
        }

        private void DeleteCustomerCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //       context.SaveChanges();


            //           qtViewSource.View.Refresh();
            //           qtitemViewSource.View.Refresh();
            //           qtViewSource.View.MoveCurrentTo(qtViewSource);
            //Move to the newest quote created
            //        qtViewSource.View.MoveCurrentToLast();
        }

        // Sets up the form so that user can enter data. Data is later  
        // saved when user clicks Commit.  
        //   private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        //       existingCustomerGrid.Visibility = Visibility.Collapsed;
        //       newOrderGrid.Visibility = Visibility.Collapsed;
        //       newCustomerGrid.Visibility = Visibility.Visible;

        // Clear all the text boxes before adding a new customer.  
        //        using (var context = new QSyncEntities())
        //       {

        //           foreach (var child in existingQuoteDataGrid.Children)
        //           {
        //               var tb = child as TextBox;
        //               if (tb != null)
        //               {
        //                   tb.Text = "";
        //               }
        //           }
        //           foreach (var child in existingQuoteDataGrid.Children)
        //           {
        //               var cb = child as CheckBox;
        //               if (cb != null)
        //               {
        //                   cb.IsChecked = false;
        //               }
        //           }

        //            var quotes = new quotes()
        //            {
        //                Info = "New",
        //           };


        //           quotedate.SelectedDate = DateTime.Now.Date;
        //           salesrep.Text = "Craig McEwen";
        //           siteaddress.Text = "Same As Address";
        //           notes.Text = DateTime.Now.Date + " - Quote created";
        //           context.SaveChanges();
        //           context.quotes.Add(quotes);
        //           QSyncEntities e2 = new QSyncEntities();
        //           e2 = new QSyncEntities();

        //Save the new Quote to the DB
        //            context.SaveChanges();
        //           context.quotes.Add(quotes);

        //Refresh our views
        //           qtViewSource.View.Refresh();
        //           int quoteid = quotes.QuoteNumber;
        //Show new ID
        //        }
        //        using (var context = new QSyncEntities())
        //        {
        //           context.quotes.AddObject(myNewObject);


        //            int id = quoteid.Text; // Your Identity column ID
        //       }

        //   }


        //   private void NewOrder_click(object sender, RoutedEventArgs e)
        //   {
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
        //       }

        // Cancels any input into the new customer form  
        //      private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        //       {
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
        //      }

        //       private void Delete_QItem(quoteitems quoteitems)
        //       {
        //
        //        }

        //       private void DeleteOrderCommandHandler(object sender, ExecutedRoutedEventArgs e)
        //       {
        // Get the Order in the row in which the Delete button was clicked.  
        //       Order obj = e.Parameter as Order;
        //       Delete_Order(obj);
        //       }
        //   }

     //   public class RelayCommand<T> : ICommand
     //   {
            #region Fields

         //   private readonly Action<T> _execute = null;
       //     private readonly Predicate<T> _canExecute = null;

            #endregion

            #region Constructors

            /// <summary>
            /// Creates a new command that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
    //        public RelayCommand(Action<T> execute)
      //          : this(execute, null)
      //      {
            }

            /// <summary>
            /// Creates a new command with conditional execution.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
     //       public RelayCommand(Action<T> execute, Predicate<T> canExecute)
     //       {
       //         if (execute == null)
         //           throw new ArgumentNullException("execute");

     //           _execute = execute;
     //           _canExecute = canExecute;
            }

            #endregion

            #region ICommand Members

     //       public bool CanExecute(object parameter)
       //     {
   //             return _canExecute == null ? true : _canExecute((T)parameter);
       //     }

    //        public event EventHandler CanExecuteChanged
     //       {
     //           add
     //           {
     //               if (_canExecute != null)
     //                   CommandManager.RequerySuggested += value;
     //           }
     //           remove
     //           {
     //               if (_canExecute != null)
     //                   CommandManager.RequerySuggested -= value;
     //           }
      //      }

    //        public void Execute(object parameter)
      //      {
    //            _execute((T)parameter);
     //       }

            #endregion
     //   }
    //}

