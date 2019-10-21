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
using System.Data.Entity;
using QSync.Controls;
using System.Windows.Threading;
using System.Threading;
using Microsoft.AppCenter.Analytics;


namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for Quotes.xaml
    /// </summary>/// 
    public partial class Quotes : Window
    {
        string btn;
        AbleDBEntity context = new AbleDBEntity();
        CollectionViewSource qtViewSource;
        CollectionViewSource qtitmViewSource;
        AbleDBEntity _db = new AbleDBEntity();
        public List<staff> SalesRep = new List<staff>();
       
        public Quotes()
        {
            InitializeComponent();
            qtViewSource = ((CollectionViewSource)(FindResource("quoteViewSource")));
            qtitmViewSource = ((CollectionViewSource)(FindResource("quotequoteitemsViewSource")));
            Footer.Content = new ControlFooter();
            DataContext = this;
            int thisFilter = Convert.ToInt32(FilterSet.Text);
            loadUserMenu();
            lblLocation.Text = "Quotes | All";
            title.Text = "Quotations - Able Door Services";
            Properties.Settings.Default.Status = "Quote# " + QID.Text + " | Quotations | QSync | by Your IT Link";
            Properties.Settings.Default.Save();
            Closed += new EventHandler(MainWindow_Closed);
        }

        public Quotes(string btn)
        {
            InitializeComponent();
            qtViewSource = ((CollectionViewSource)(FindResource("quoteViewSource")));
            qtitmViewSource = ((CollectionViewSource)(FindResource("quotequoteitemsViewSource")));
            Footer.Content = new ControlFooter();
            DataContext = this;
            int thisFilter = Convert.ToInt32(FilterSet.Text);
            loadUserMenu();
            lblLocation.Text = "Quotes | All";
            title.Text = "Find a Quote - Able Door Services";
            Properties.Settings.Default.Status = "Search for a Quote | Quotations | QSync | by Your IT Link";
            Properties.Settings.Default.Save();
            newQuoteDataGrid.Visibility = Visibility.Collapsed;
            newquote.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Collapsed;
            quoteSearchBtn.Visibility = Visibility.Visible;
            quoteinfo.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Collapsed;
            quoteSearch.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | Search for a Quote";
            quoteSearch.IsSelected = true;
            TitleUpdate();
            Closed += new EventHandler(MainWindow_Closed);
        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TitleUpdate()
        {
            Title = "Quote# " + QID.Text + " |Quotations | QSync | by Your IT Link";
            Properties.Settings.Default.Status = "Quote# " + QID.Text + " | Quotations | QSync | by Your IT Link";
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource quoteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("quoteViewSource")));
            context.quotes.Load();
            qtViewSource.Source = context.quotes.Local;
            qtViewSource.View.MoveCurrentToLast();
            TitleUpdate();
            //Query between these dates
         //    {
            //Create a query which loads quotes for the past 12 month period only
        //    var limit = DateTime.Now.AddMonths(-12);
         //   AbleDBEntity context = new AbleDBEntity();
         //  var redLoad = from c in context.quotes
         //               where c.Date1 > limit
         //              orderby c.QuoteNumber ascending
         //               select c;
            //Load the query results to view
         //   System.Windows.Data.CollectionViewSource quoteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("quoteViewSource")));
         //   redLoad.Load();
         //   qtViewSource.Source = context.quotes.Local;
         //   qtViewSource.View.MoveCurrentToLast();
         //   TitleUpdate();
        }

        #region Left hand menu button commands

        //LHS btn commands
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            qtViewSource.View.MoveCurrentToLast();
            TitleUpdate();
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            qtViewSource.View.MoveCurrentToPrevious();
            TitleUpdate();
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            qtViewSource.View.MoveCurrentToNext();
            TitleUpdate();
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            qtViewSource.View.MoveCurrentToFirst();
            TitleUpdate();
        }
        //***REFRESH QUOTE COMMAND HANDLER***
        private void DeleteCustomerCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();
        }

        private void DeleteQuoteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Delete attempt");
            // If existing window is visible, delete the customer and all their orders.  
            // In a real application, you should add warnings and allow the user to cancel the operation.  
            var cur = qtViewSource.View.CurrentItem as quote;
            var qt = (from c in context.quotes
                        where c.QuoteNumber == cur.QuoteNumber
                        select c).FirstOrDefault();
            if (qt != null)
            {
                foreach (var qtitm in qt.quoteitems.ToList())
                {
         //           Delete_Order(qtitm);
                }
                context.quotes.Remove(qt);
            }
            context.SaveChanges();
            qtViewSource.View.Refresh();
        }
        

        // Commit changes from the new quote form,  
        // or edits made to the existing quotes form.  
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            context.SaveChanges();
            TitleUpdate();
        }
        

                // Sets up the form so that user can enter data. Data is later  
                // saved when user clicks Commit.  
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Add new Record");
            //First set the view to reset the form information
            quoteinfo.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Collapsed;
            addQuoteBtn.Visibility = Visibility.Visible;
            newquote.Visibility = Visibility.Visible;
            newQuoteDataGrid.Visibility = Visibility.Visible;
            QID.Visibility = Visibility.Collapsed;
            lblLocation.Text = "Quotes | New Quote";
            newquote.IsSelected = true;
            Properties.Settings.Default.Status = "Add a new Quotation - Complete Customer information";
            Properties.Settings.Default.Save();

            // Clear all the text boxes before adding a new quote.  
            foreach (var child in newQuoteDataGrid.Children)
            {
                var tb = child as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
            //Clear all the check boxes before adding new quote
            foreach (var child in newQuoteDataGrid.Children)
            {
                var cb = child as CheckBox;
                if (cb != null)
                {
                    cb.IsChecked = false;
                }
            }
            //Add some pre-defined text before starting the quote
            add_quotedate.SelectedDate = DateTime.Now;
            add_siteaddress.Text = "Same As Address";
            add_salesrep.Text = Properties.Settings.Default.Name;
            add_repID.Text = Properties.Settings.Default.Qid;
            TitleUpdate();
        }


        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Save new Record");
            if (newquote.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.
                //   SalesRepIDMatch();
                quote newQuote = new quote()

                {
                    Date1 = add_quotedate.SelectedDate,
                    Representative = add_salesrep.Text,
                    Company = add_custname.Text,
                    Address = add_custaddress.Text,
                    ContactPerson = add_custcontact.Text,
                    Phone = add_custphone.Text,
                    Mobile = add_custmobile.Text,
                    Email = add_custemail.Text,
                    StrataNo = add_custstrata.Text,
                    RefNo = add_custref.Text,
                    JobLocation = add_siteaddress.Text,
                    SiteContact = add_sitecontact.Text,
                    SCPhone = add_sitephone.Text,
                    SCMobile = add_sitemobile.Text,
                    SCEmail = add_siteemail.Text,
                    Inductiontime = add_indtime.Text,
                    Notes = add_notes.Text,
                    repID = add_repID.Text,
                };
                SalesRepIDMatch();
                {
                    context.quotes.Local.Add(newQuote);
                    qtViewSource.View.Refresh();
                    qtViewSource.View.MoveCurrentTo(newQuote);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newQuoteDataGrid.Visibility = Visibility.Collapsed;
                newquote.Visibility = Visibility.Collapsed;
                //   btnAddCancel.Visibility = Visibility.Collapsed;
                quoteBtn.Visibility = Visibility.Visible;
                addQuoteBtn.Visibility = Visibility.Collapsed;
                quoteinfo.Visibility = Visibility.Visible;
                quoteitemdetails.Visibility = Visibility.Visible;
                lblLocation.Text = "Quotes | All Loaded";
                quoteinfo.IsSelected = true;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();
            QID.Visibility = Visibility.Visible;
            TitleUpdate();
        }

        private void NewOrder_click(object sender, RoutedEventArgs e)
        {
        }

        // Cancels any input into the new quote form  
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void Delete_QItem(invitem invitem)
        {

        }

        

        private void DeleteOrderCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        }


        //On click create a duplicate using the loaded data from view
        private void DuplicateRecord_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("You are about to change settings to the user access, Is this what you wanted to do? Click YES to SAVE or NO to CANCEL", "User Access Changes - ARE YOU SURE?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Analytics.TrackEvent("Quotes - Create DUPLICATE Quote");
            Analytics.TrackEvent("DEBUG - #1 SHOW/HIDE GRIDS");
            //First Setup the form to create a duplicate quote
            quoteinfo.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Collapsed;
            quoteSearchBtn.Visibility = Visibility.Collapsed;
            quoteSearch.Visibility = Visibility.Collapsed;
            addQuoteBtn.Visibility = Visibility.Collapsed;
            duplicateQuoteBtn.Visibility = Visibility.Visible;
            duplicateCreate.Visibility = Visibility.Visible;
            duplicateQuoteDataGrid.Visibility = Visibility.Visible;
            QID.Visibility = Visibility.Collapsed;
            lblLocation.Text = "Quotes | Duplicate Existing Quote";
            duplicateCreate.IsSelected = true;
            Properties.Settings.Default.Status = "*** DUPLICATE QUOTE ENTRY ***";
            Properties.Settings.Default.Save();
            // Normally we would now clear all the data from the view to prep for new - Here we will only clear certain fields
            Analytics.TrackEvent("DEBUG - #2 CLEAR OUT FIELDS");
                    MessageBox.Show("Quote has been duplicated, to complete click the COMMIT button to the left", "Quote Duplicated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case MessageBoxResult.No:
                    ;
                    break;
            }

        }

        private void DuplicateCommit_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("DEBUG - #5 Commit duplicate Quote As Is");

            if (duplicateCreate.IsVisible)
                {
                // Create a new object because the old one  
                // is being tracked by EF now.  
                Analytics.TrackEvent("DEBUG - #6 DEFINE newQuote THEN ASSIGN TEXT FROM INSERTED INTO FIELDS");

                dup_salesrep.Text = Properties.Settings.Default.Name;
                dup_repID.Text = Properties.Settings.Default.Qid;
                dup_quotedate.SelectedDate = DateTime.Now;


                quote dupQuote = new quote()
                {

                    Date1 = DateTime.Now,
                    Representative = dup_salesrep.Text,
                    Company = dup_custname.Text,
                    Address = dup_custaddress.Text,
                    ContactPerson = dup_custcontact.Text,
                    Phone = dup_custphone.Text,
                    Mobile = dup_custmobile.Text,
                    Email = dup_custemail.Text,
                    StrataNo = dup_custstrata.Text,
                    RefNo = dup_custref.Text,
                    JobLocation = dup_siteaddress.Text,
                    SiteContact = dup_sitecontact.Text,
                    SCPhone = dup_sitephone.Text,
                    SCMobile = dup_sitemobile.Text,
                    SCEmail = dup_siteemail.Text,
                    Inductiontime = dup_indtime.Text,
                    Notes = dup_notes.Text,
                    repID = dup_repID.Text,
                  //  repID = Properties.Settings.Default.Qid,
                  //  Induction = Convert.ToInt32(dup_induction),
                  //  COD = Convert.ToInt32(dup_cod),
                  //  SvnDays = Convert.ToInt32(dup_svndays),
                  //  PROGRESSPAYMENT = Convert.ToInt32(dup_pp),
                  //  SWMS = Convert.ToInt32(dup_swms),
                    //    Induction = add_induction.

                };
             //   dup_repID.Text = Properties.Settings.Default.Qid,
                DupRepIDMatch();
                {
                    Analytics.TrackEvent("DEBUG - #7 BRING VIEW INTO CONTEXT");
                    context.quotes.Local.Add(dupQuote);
                    qtViewSource.View.Refresh();
                    qtViewSource.View.MoveCurrentTo(dupQuote);
                }
                Analytics.TrackEvent("DEBUG - #8 SHOW/HIDE GRIDS");
                //Close the secondary ADD grid and move back to EXISTING grid
                duplicateQuoteDataGrid.Visibility = Visibility.Collapsed;
                duplicateCreate.Visibility = Visibility.Collapsed;
            //    btnAddCancel.Visibility = Visibility.Collapsed;
                quoteSearchBtn.Visibility = Visibility.Collapsed;
                quoteBtn.Visibility = Visibility.Visible;
                addQuoteBtn.Visibility = Visibility.Collapsed;
                quoteinfo.Visibility = Visibility.Visible;
                quoteitemdetails.Visibility = Visibility.Visible;
                duplicateQuoteBtn.Visibility = Visibility.Collapsed;
                lblLocation.Text = "Quotes | All Loaded";
                quoteinfo.IsSelected = true;
            }    //Save to the database now all has been checked
            Analytics.TrackEvent("DEBUG - #9 SAVE CONTEXT TO DB");
            context.SaveChanges();
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();
            QID.Visibility = Visibility.Visible;
            TitleUpdate();
            Analytics.TrackEvent("DEBUG #10 END - COMPLETED DUPLICATION OF QUOTE");
        }

        private void quoteDuplicate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will duplicate the details on Quote"  + QID.Text + ". Click YES to DUPLICATE or NO to CANCEL", "Duplicate Quote" + QID.Text + " - ARE YOU SURE?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Analytics.TrackEvent("Quote Duplicate -  Start DUPLICATE Quote");
                    //First Setup the form to create a duplicate quote
                    quoteinfo.Visibility = Visibility.Collapsed;
                    quoteitemdetails.Visibility = Visibility.Collapsed;
                    quoteBtn.Visibility = Visibility.Collapsed;
                    quoteSearchBtn.Visibility = Visibility.Collapsed;
                    quoteSearch.Visibility = Visibility.Collapsed;
                    addQuoteBtn.Visibility = Visibility.Collapsed;
                    duplicateQuoteBtn.Visibility = Visibility.Visible;
                    duplicateCreate.Visibility = Visibility.Visible;
                    duplicateQuoteDataGrid.Visibility = Visibility.Visible;
                    QID.Visibility = Visibility.Collapsed;
                    lblLocation.Text = "Quotes | Duplicate Existing Quote";
                    duplicateCreate.IsSelected = true;
                    Properties.Settings.Default.Status = "*** DUPLICATE QUOTE ENTRY ***";
                    Properties.Settings.Default.Save();
                    // Normally we would now clear all the data from the view to prep for new - Here we will only clear certain fields
                    Analytics.TrackEvent("Quote Duplicate -  Copied info to new view");
                    
                    if (duplicateCreate.IsVisible)
                    {
                        // Create a new object because the old one  
                        // is being tracked by EF now.  
                        Analytics.TrackEvent("Quote Duplicate -  RepID and Rep name updated to current user");

                        dup_salesrep.Text = Properties.Settings.Default.Name;
                        dup_repID.Text = Properties.Settings.Default.Qid;
                        Analytics.TrackEvent("Quote Duplicate -  Today's date set");
                        dup_quotedate.SelectedDate = DateTime.Now;
                        dup_notes.Text = "" + DateTime.Now.ToString("dd/MM/yy") + " - Duplicate of# " + QID.Text +"";
                        quote dupQuote = new quote()
                        {

                            Date1 = DateTime.Now,
                            Representative = dup_salesrep.Text,
                            Company = dup_custname.Text,
                            Address = dup_custaddress.Text,
                            ContactPerson = dup_custcontact.Text,
                            Phone = dup_custphone.Text,
                            Mobile = dup_custmobile.Text,
                            Email = dup_custemail.Text,
                            StrataNo = dup_custstrata.Text,
                            RefNo = dup_custref.Text,
                            JobLocation = dup_siteaddress.Text,
                            SiteContact = dup_sitecontact.Text,
                            SCPhone = dup_sitephone.Text,
                            SCMobile = dup_sitemobile.Text,
                            SCEmail = dup_siteemail.Text,
                            Inductiontime = dup_indtime.Text,
                            Notes = dup_notes.Text,
                            repID = dup_repID.Text,
                            /*  repID = Properties.Settings.Default.Qid,
                              Induction = Convert.ToInt32(dup_induction),
                              COD = Convert.ToInt32(dup_cod),
                              SvnDays = Convert.ToInt32(dup_svndays),
                              PROGRESSPAYMENT = Convert.ToInt32(dup_pp),
                              SWMS = Convert.ToInt32(dup_swms),
                              Induction = add_induction.
                            */

                        };
                        //   dup_repID.Text = Properties.Settings.Default.Qid,
                        DupRepIDMatch();
                        {
                            Analytics.TrackEvent("Quote Duplicate -  BRING VIEW INTO CONTEXT");
                            context.quotes.Local.Add(dupQuote);
                            qtViewSource.View.Refresh();
                            qtViewSource.View.MoveCurrentTo(dupQuote);
                        }
                        Analytics.TrackEvent("Quote Duplicate -  SHOW/HIDE GRIDS");
                        //Close the secondary ADD grid and move back to EXISTING grid
                        duplicateQuoteDataGrid.Visibility = Visibility.Collapsed;
                        duplicateCreate.Visibility = Visibility.Collapsed;
                        quoteinfoLoaded.Visibility = Visibility.Collapsed;
                        //    btnAddCancel.Visibility = Visibility.Collapsed;
                        quoteSearchBtn.Visibility = Visibility.Collapsed;
                        quoteBtn.Visibility = Visibility.Visible;
                        addQuoteBtn.Visibility = Visibility.Collapsed;
                        quoteinfo.Visibility = Visibility.Visible;
                        quoteitemdetails.Visibility = Visibility.Visible;
                        duplicateQuoteBtn.Visibility = Visibility.Collapsed;
                        lblLocation.Text = "Quotes | All Loaded";
                        quoteinfo.IsSelected = true;
                    }    //Save to the database now all has been checked
                    Analytics.TrackEvent("Quote Duplicate -  SAVE CONTEXT TO DB");
                    context.SaveChanges();
                    qtViewSource.View.MoveCurrentToPrevious();
                    qtViewSource.View.MoveCurrentToNext();
                    QID.Visibility = Visibility.Visible;
                    TitleUpdate();
                    Analytics.TrackEvent("Quote Duplicate -  COMPLETED DUPLICATION OF QUOTE");
                    MessageBox.Show("Quote has been duplicated, you can now add quote items to the quote", "Quote Duplicated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case MessageBoxResult.No:
                    Analytics.TrackEvent("Quote Duplicate -  Cancelled DUPLICATE Quote");
                    break;
            }
        }

            private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(e.Parameter as string);
        }

        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Parameter as string))
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void QuoteitemsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            qtitmViewSource.View.Refresh();
            context.SaveChanges();
        }

        private void Quoteinfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
        //    qtitmViewSource.View.Refresh();
            context.SaveChanges();
        }

        //QuoteSearchTab Items

        private void LoadSearchQuotes_click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Load Search Window");
            newQuoteDataGrid.Visibility = Visibility.Collapsed;
            newquote.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Collapsed;
            quoteSearchBtn.Visibility = Visibility.Visible;
            quoteinfo.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Collapsed;
            quoteSearch.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | Search for a Quote";
            quoteSearch.IsSelected = true;
            TitleUpdate();
            //    quoteSearchDataGrid = Visibility.Visible;
        }
        private void CancelSearchQuotes_click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Cancel Quote Search");
            newQuoteDataGrid.Visibility = Visibility.Collapsed;
            newquote.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Visible;
            quoteSearchBtn.Visibility = Visibility.Collapsed;
            quoteinfo.Visibility = Visibility.Visible;
            quoteitemdetails.Visibility = Visibility.Visible;
            quoteSearch.Visibility = Visibility.Collapsed;
            quoteinfoLoaded.Visibility = Visibility.Collapsed;
            loadedQuoteDataGrid.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Visible;
            quoteitemsDataGrid.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | All Loaded (Canceled search for quote)";
            quoteinfo.IsSelected = true;
            TitleUpdate();
            //    quoteSearchDataGrid = Visibility.Visible;
        }
        private void CancelAddQuotes_click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Cancel New Quote");
            newQuoteDataGrid.Visibility = Visibility.Collapsed;
            newquote.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Visible;
            quoteSearchBtn.Visibility = Visibility.Collapsed;
            addQuoteBtn.Visibility = Visibility.Collapsed;
            quoteinfo.Visibility = Visibility.Visible;
            quoteitemdetails.Visibility = Visibility.Visible;
            quoteSearch.Visibility = Visibility.Collapsed;
            quoteinfoLoaded.Visibility = Visibility.Collapsed;
            loadedQuoteDataGrid.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Visible;
            quoteitemsDataGrid.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | All Loaded (Canceled add new quote)";
            quoteinfo.IsSelected = true;
            TitleUpdate();
            //    quoteSearchDataGrid = Visibility.Visible;
        }
 

 
        private void LoadSelectedQuoteView_click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Search Loaded Quote from Search");
            quoteinfoLoaded.Visibility = Visibility.Visible;
            loadedQuoteDataGrid.Visibility = Visibility.Visible;
            quoteitemdetails.Visibility = Visibility.Visible;
            quoteitemsDataGrid.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | Searched | Selected (" + QID.Text + ")";
            quoteinfoLoaded.IsSelected = true;
            TitleUpdate();

        }

        #endregion

        #region Application menu commands
        #region File

        public void CloseQSync_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void SearchQuotes_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var sq = new Views.Quotes(btn);
            sq.Show();
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

        private void dev1_Click(object sender, RoutedEventArgs e)
        {
            //    this.Visibility = Visibility.Hidden;
            var em = new Controls.ProgEmail();
            em.Show();
        }
        private void dev2_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var bc = new Views.BuildChanges();
            bc.Show();
        }
        private void dev3_Click(object sender, RoutedEventArgs e)
        {
        }
        private void dev4_Click(object sender, RoutedEventArgs e)
        {
        }
        private void dev5_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Dev Menu

        #region Window Specific Menu


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


        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Quotes - Preview Quote to send");
            context.SaveChanges();
            test.reportWinQuote rpt = new test.reportWinQuote(Quote_Number.Text, custemail.Text);
            rpt.Show();
        }
        //Removed 23/08/2019
        private void QuoteSearchDataGrid_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
        //    var data = quoteSearchDataGrid.SelectedItem;
            /*
            string MMBR = (quoteSearchDataGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            string NMMBR = (quoteSearchDataGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            string appchg = (quoteSearchDataGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            string CHGDT = (quoteSearchDataGrid.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
            string STAT = (quoteSearchDataGrid.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
            string REL = (quoteSearchDataGrid.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
            string COMP = (quoteSearchDataGrid.SelectedCells[7].Column.GetCellContent(data) as TextBlock).Text;
            Loadedmmbr.Text = MMBR;
            newmmbr.Text = NMMBR;
            changeApplied.Text = appchg;
            chgdate.Text = CHGDT;
            stat.Text = STAT;
            release.Text = REL;*/
        }

        private void salesrepChanged_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void addSalesRep_SelectionChanged(object sender,
                                SelectionChangedEventArgs e)
        {
        //    MessageBox.Show("comboBox1_SelectionChanged event called",
        //                    "Event", MessageBoxButton.OK,
        //                    MessageBoxImage.Information);
        //    add_repID.Visibility = Visibility.Visible;
        }
        public void PopulateAssemblyComboBox()
        {
            List<string> comboBoxList = new List<string>();

            comboBoxList.Add("Brad Hill");
            comboBoxList.Add("Gary Thomson");
            comboBoxList.Add("Greg Reynolds");
            comboBoxList.Add("Marty Lynch");
            comboBoxList.Add("Michael Paraskevas");
            comboBoxList.Add("Steve Rendoth");
            comboBoxList.Add("Internal");

            add_salesrep.ItemsSource = comboBoxList;
         //   dup_salesrep.ItemsSource = comboBoxList;
        //    add_salesrep.SelectionChanged -=
        //       new SelectionChangedEventHandler(addSalesRep_SelectionChanged);
            add_salesrep.Text = Properties.Settings.Default.Name;
         //   dup_salesrep.Text = Properties.Settings.Default.Name;
        //      add_salesrep.SelectionChanged +=
        //       new SelectionChangedEventHandler(addSalesRep_SelectionChanged);
        }

       
        private void Add_salesrep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(add_salesrep.Text == "Brad Hill")
            {
                add_repID.Text = "BH";
            }
            if (add_salesrep.Text == "Gary Thomson")
            {
                add_repID.Text = "GT";
            }
            if (add_salesrep.Text == "Greg Reynolds")
            {
                add_repID.Text = "GR";
            }
            if (add_salesrep.Text == "Marty Lynch")
            {
                add_repID.Text = "ML";
            }
            if (add_salesrep.Text == "Michael Paraskevas")
            {
                add_repID.Text = "MP";
            }
            if (add_salesrep.Text == "Steve Rendoth")
            {
                add_repID.Text = "SR";
            }
            if (add_salesrep.Text == "Internal")
            {
                add_repID.Text = "IT";
            }
        }

        private void Dup_salesrep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (add_salesrep.Text == "Brad Hill")
            {
                add_repID.Text = "BH";
            }
            if (add_salesrep.Text == "Gary Thomson")
            {
                add_repID.Text = "GT";
            }
            if (add_salesrep.Text == "Greg Reynolds")
            {
                add_repID.Text = "GR";
            }
            if (add_salesrep.Text == "Marty Lynch")
            {
                add_repID.Text = "ML";
            }
            if (add_salesrep.Text == "Michael Paraskevas")
            {
                add_repID.Text = "MP";
            }
            if (add_salesrep.Text == "Steve Rendoth")
            {
                add_repID.Text = "SR";
            }
            if (add_salesrep.Text == "Internal")
            {
                add_repID.Text = "IT";
            }
        }

        //Change sales rep ID on quotes creation
        private void SalesRepIDMatch()
        {
            if (add_salesrep.Text == "Brad Hill")
            {
                add_repID.Text = "BH";
            }
            if (add_salesrep.Text == "Gary Thomson")
            {
                add_repID.Text = "GT";
            }
            if (add_salesrep.Text == "Greg Reynolds")
            {
                add_repID.Text = "GR";
            }
            if (add_salesrep.Text == "Marty Lynch")
            {
                add_repID.Text = "ML";
            }
            if (add_salesrep.Text == "Michael Paraskevas")
            {
                add_repID.Text = "MP";
            }
            if (add_salesrep.Text == "Steve Rendoth")
            {
                add_repID.Text = "SR";
            }
            if (add_salesrep.Text == "Internal")
            {
                add_repID.Text = "IT";
            }
        }

        //Change sales rep ID on duplicate quotes creation
        private void DupRepIDMatch()
        {
            if (dup_salesrep.Text == "Brad Hill")
            {
                dup_repID.Text = "BH";
            }
            if (dup_salesrep.Text == "Gary Thomson")
            {
                dup_repID.Text = "GT";
            }
            if (dup_salesrep.Text == "Greg Reynolds")
            {
                dup_repID.Text = "GR";
            }
            if (dup_salesrep.Text == "Marty Lynch")
            {
                dup_repID.Text = "ML";
            }
            if (dup_salesrep.Text == "Michael Paraskevas")
            {
                dup_repID.Text = "MP";
            }
            if (dup_salesrep.Text == "Steve Rendoth")
            {
                dup_repID.Text = "SR";
            }
            if (dup_salesrep.Text == "Internal")
            {
                dup_repID.Text = "IT";
            }
        }
    }
}



