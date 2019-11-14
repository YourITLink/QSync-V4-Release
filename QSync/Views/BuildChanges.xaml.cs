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
using Microsoft.AppCenter.Analytics;



namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for BuildChanges.xaml
    /// </summary>
    public partial class BuildChanges : Window
    {
        AbleDBEntity context = new AbleDBEntity();
        CollectionViewSource bcViewSource;
        AbleDBEntity _db = new AbleDBEntity();
        public BuildChanges()
        {
            InitializeComponent();
            bcViewSource = ((CollectionViewSource)(FindResource("buildChanxViewSource")));
            Footer.Content = new ControlFooter();
            DataContext = this;
            lblLocation.Text = "QSync Version History";
            Title = "Build Changes | QSync V4.0 by Your IT Link | Able Door Services (NSW) Pty Ltd | by Your IT Link";
            Properties.Settings.Default.Status = "Build Changes - View/Update QSync Builds";
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource buildChanxViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("buildChanxViewSource")));
            context.BuildChanges.Load();
            bcViewSource.Source = context.BuildChanges.Local;
            bcViewSource.View.MoveCurrentToLast();
            
        }

        //LHS btn commands
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bcViewSource.View.MoveCurrentToLast();
            //QID.Text = quoteid.Text;
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bcViewSource.View.MoveCurrentToPrevious();
            //QID.Text = quoteid.Text;
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bcViewSource.View.MoveCurrentToNext();
            //QID.Text = quoteid.Text;
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bcViewSource.View.MoveCurrentToFirst();
        }
        private void RefreshBuildCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bcViewSource.View.MoveCurrentToPrevious();
            bcViewSource.View.MoveCurrentToNext();
            //QID.Text = quoteid.Text;
        }
        private void CommitBuildChangesCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (newBuildChangesDataGrid.IsVisible)
            {
                // Create a new object because the old one is being tracked by EF now.  
                BuildChanx newBuildChanges = new BuildChanx()
                {
                    ChangeDate = addchangeDate.SelectedDate,
                    VersionReleaseDate = addrelDate.SelectedDate,
                    AppliedChanges = addappliedChanges.Text,
                    MMBR = addmmbr.Text,
                    New_MMBR = addnew_Mmbr.Text,
                    Status = addstatus.Text,
                };
                {
                    context.BuildChanges.Local.Add(newBuildChanges);
                    bcViewSource.View.Refresh();
                    bcViewSource.View.MoveCurrentTo(newBuildChanges);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newBuildChangesDataGrid.Visibility = Visibility.Collapsed;
                existingBuildChangeDataGrid.Visibility = Visibility.Visible;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            bcViewSource.View.MoveCurrentToPrevious();
            bcViewSource.View.MoveCurrentToNext();
            //QID.Text = quoteid.Text; */
        }
        // Commit changes from the new customer form, the new order form, or edits made to the existing customer form.  
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            context.SaveChanges();
            //QID.Text = quoteid.Text;
        }
        
        // Sets up the form so that user can enter data. Data is later saved when user clicks Commit.  
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //First set the view to reset the form inform
            existingBuildChangeDataGrid.Visibility = Visibility.Collapsed;
            newBuildChangesDataGrid.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnCommit.Visibility = Visibility.Visible;
            // Clear all the text boxes before adding a new quote.  
            foreach (var child in newBuildChangesDataGrid.Children)
            {
                var tb = child as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            //Clear all the check boxes before adding new quote
            foreach (var child in newBuildChangesDataGrid.Children)
            {
                var cb = child as CheckBox;
                if (cb != null)
                {
                    cb.IsChecked = false;
                }
            }
            //Add some pre defined text before starting the BuildChanges Entry
            addchangeDate.SelectedDate = DateTime.Now;
            addstatus.Text = "Debug"; 
        }
        private void NewOrder_click(object sender, RoutedEventArgs e)
        {
        }

        // Cancels any input into the new customer form  
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
                        //        int acct;
                        //        int.TryParse(add_originv.Text, out acct);
                        if (newBuildChangesDataGrid.IsVisible)
                        {
                            // Create a new object because the old one  
                            // is being tracked by EF now.  
                            BuildChanx newBuildChanges = new BuildChanx()
                            {
                                ChangeDate = addchangeDate.SelectedDate,
                                VersionReleaseDate = addrelDate.SelectedDate,
                                AppliedChanges = addappliedChanges.Text,
                                MMBR = addmmbr.Text,
                                New_MMBR = addnew_Mmbr.Text,
                                Status = addstatus.Text,


                            };
                            {
                                context.BuildChanges.Local.Add(newBuildChanges);
                                bcViewSource.View.Refresh();
                                bcViewSource.View.MoveCurrentTo(newBuildChanges);
                            }
                            //Close the secondary ADD grid and move back to EXISTING grid
                            newBuildChangesDataGrid.Visibility = Visibility.Collapsed;
                            existingBuildChangeDataGrid.Visibility = Visibility.Visible;

                        }    //Save to the database now all has been checked
                        context.SaveChanges();
                        bcViewSource.View.MoveCurrentToPrevious();
                        bcViewSource.View.MoveCurrentToNext();
                        //QID.Text = quoteid.Text; */
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

    }
}
