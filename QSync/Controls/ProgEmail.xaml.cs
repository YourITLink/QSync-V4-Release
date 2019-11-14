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
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace QSync.Controls
{
    /// <summary>
    /// Interaction logic for ProgEmail.xaml
    /// </summary>
    public partial class ProgEmail : Window
    {
        string progEmail;string progPW;string progSMTP;int progSMTPPort;public static string showQuoteID;public static string showStaffName;public static string varLoad;string thisEmail; string recipient;
        string btn;
        public ProgEmail()
        {
            InitializeComponent();
            lblLocation.Text = "Compose Email";
            title.Text = "QSync Email";
            recipient = emailTo.Text;
            

        }
        private void EmailSystemCheck()
        {
            if (Properties.Settings.Default.emailUsed == "Program")
            {
                string progEmail = Properties.Settings.Default.progEmail;
                string progPW = Properties.Settings.Default.progPW;
                string progSMTP = Properties.Settings.Default.progSMTP;
                int progSMTPPort = Properties.Settings.Default.progSMTPPort;
                string thisEmail = Properties.Settings.Default.progEmail;
            }
            else
            {
                string progEmail = Properties.Settings.Default.userEmail;
                string progPW = Properties.Settings.Default.userPW;
                string progSMTP = Properties.Settings.Default.userSMTP;
                int progSMTPPort = Properties.Settings.Default.userSMTPPort;
            }
        }



        #region Application Menu Commands

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

        private void Email_Click(object sender, RoutedEventArgs e)
        {
            //    this.Visibility = Visibility.Hidden;
            var em = new Controls.ProgEmail();
            em.Show();
        }

        #endregion Dev Menu

        #region Window Specific Menu
        private void quote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void service_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var sv = new Views.Servicing();
            sv.Show();
        }
        private void invoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var iv = new Views.Invoices();
            iv.Show();
        }
        private void schedule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
            qt.Show();
        }
        private void findQuote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var qt = new Views.Quotes();
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

        #endregion Application Menu Commands

        //Outgoing email 
        private void EmailBtnSend_Click(object sender, RoutedEventArgs e)
        {
            //*        var server = Properties.Settings.Default.progSMTP;
            //*        var port = Properties.Settings.Default.progSMTPPort; 
            //*   var emailfrom = Properties.Settings.Default.progEmail; 
            //*    var senderPassword = Properties.Settings.Default.progPW;

            var server = "smtp.gmail.com";
            var port = 587;
            var emailfrom = "cmcewen82@gmail.com";
            var senderPassword = "WPT$Matrix_4390";
            var pdfPath = "pathtoexportedPDF";

            /*
            var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                    {
                        Credentials = new NetworkCredential(emailfrom, senderPassword),
                        EnableSsl = true

                       };
            //Attempt to send the email
            try
                    {
                        SmtpClient server = new SmtpClient("smtpClient");
                        server.Credentials = new NetworkCredential("emailFrom", "senderPassword");
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("emailFrom");
                        message.To.Add = (recipient);
                        message.Subject = emailSubject.Text;
                        message.IsBodyHtml = true;
                        message.Body = emailBody.Text;
                        server.Send(message);
                     //   smptClient.Send(senderEmailId,emailTo.Text.Trim(),emailSubject.Text,emailBody.Text);
                        MessageBox.Show("Message Sent Successfully","Email Sent",MessageBoxButton.OK,MessageBoxImage.Information);
                        //Reset form after success
                        emailTo.Text = "";
                        emailSubject.Text = "";
                        emailBody.Text = "";
                        emailTo.Focus();
                    }
            //Display message if email has failed
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Sending Email",MessageBoxButton.YesNoCancel,MessageBoxImage.Error);
            }*/

            using (MailMessage mm = new MailMessage(emailfrom, emailTo.Text.Trim()))

            {
                mm.Subject = emailSubject.Text;
                mm.Body = emailBody.Text;
             //       foreach (string filePath in OpenFileDialog.FileNames)
             //       {
             //           if (File.Exists(filePath))
             //           {
             //               string fileName = Path.GetFileName(filePath);
             //               mm.Attachments.Add(new Attachment(filePath));
             //           }
             //        }
                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(pdfPath));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = server;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(emailfrom, senderPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = port;
                try
                {
                    smtp.Send(mm);
                    MessageBox.Show("Quote has been emailed to customer.", "Email Sent!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Sending Email", MessageBoxButton.YesNoCancel, MessageBoxImage.Error);
                }
            }
        }

        #region Left Hand Side Button Commands
        //LHS btn commands
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    qtViewSource.View.MoveCurrentToLast();
            //QID.Text = quoteid.Text; */
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    qtViewSource.View.MoveCurrentToPrevious();
            //QID.Text = quoteid.Text; */
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    qtViewSource.View.MoveCurrentToNext();
            //QID.Text = quoteid.Text;*/
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    qtViewSource.View.MoveCurrentToFirst();
            //QID.Text = quoteid.Text;*/
        }
        //***REFRESH QUOTE COMMAND HANDLER***
        private void DeleteCustomerCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();
            //QID.Text = quoteid.Text;*/
        }
        private void CommitQuoteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
         /*   //        int acct;
            //        int.TryParse(add_originv.Text, out acct);
            if (newquote.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.  
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
                    Notes = add_notes.Text

                };
                {
                    context.quotes.Local.Add(newQuote);
                    qtViewSource.View.Refresh();
                    qtViewSource.View.MoveCurrentTo(newQuote);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newQuoteDataGrid.Visibility = Visibility.Collapsed;
                newquote.Visibility = Visibility.Collapsed;
                quoteinfo.Visibility = Visibility.Visible;
                quoteitemdetails.Visibility = Visibility.Visible;
                quoteinfo.IsSelected = true;
                QID.Visibility = Visibility.Visible;
                //   QID.Text = quoteid.Text;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();
            lblLocation.Text = "Quotes | All Loaded";*/
        }

        // Commit changes from the new customer form, the new order form,  
        // or edits made to the existing customer form.  
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    context.SaveChanges();
            //QID.Text = quoteid.Text;*/
        }


        // Sets up the form so that user can enter data. Data is later  
        // saved when user clicks Commit.  
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
        /*    //First set the view to reset the form inform
            quoteinfo.Visibility = Visibility.Collapsed;
            quoteitemdetails.Visibility = Visibility.Collapsed;
            quoteBtn.Visibility = Visibility.Collapsed;
            addQuoteBtn.Visibility = Visibility.Visible;
            newquote.Visibility = Visibility.Visible;
            newQuoteDataGrid.Visibility = Visibility.Visible;
            lblLocation.Text = "Quotes | New Quote";
            newquote.IsSelected = true;

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
            //Add some pre defined text before starting the quote
            add_quotedate.SelectedDate = DateTime.Now;
            add_siteaddress.Text = "Same As Address";
            add_salesrep.Text = Properties.Settings.Default.Name;
            add_repID.Text = Properties.Settings.Default.Qid;*/
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
        /*    //        int ind;
            //        int.TryParse(add_induction.IsChecked, out ind);
            if (newquote.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.  
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
                    //    Induction = add_induction.

                };
                {
                    context.quotes.Local.Add(newQuote);
                    qtViewSource.View.Refresh();
                    qtViewSource.View.MoveCurrentTo(newQuote);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newQuoteDataGrid.Visibility = Visibility.Collapsed;
                newquote.Visibility = Visibility.Collapsed;
                btnAddCancel.Visibility = Visibility.Collapsed;
                quoteBtn.Visibility = Visibility.Visible;
                addQuoteBtn.Visibility = Visibility.Collapsed;
                quoteinfo.Visibility = Visibility.Visible;
                quoteitemdetails.Visibility = Visibility.Visible;
                lblLocation.Text = "Quotes | All Loaded";
                quoteinfo.IsSelected = true;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();*/
        }
        private void DuplicateQuote_Click(object sender, RoutedEventArgs e)
        {
        /*    //        int ind;
            //        int.TryParse(add_induction.IsChecked, out ind);
            if (newquote.IsVisible)
            {
                // Create a new object because the old one  
                // is being tracked by EF now.  
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
                    //    Induction = add_induction.

                };
                {
                    context.quotes.Local.Add(newQuote);
                    qtViewSource.View.Refresh();
                    qtViewSource.View.MoveCurrentTo(newQuote);
                }
                //Close the secondary ADD grid and move back to EXISTING grid
                newQuoteDataGrid.Visibility = Visibility.Collapsed;
                newquote.Visibility = Visibility.Collapsed;
                btnAddCancel.Visibility = Visibility.Collapsed;
                quoteBtn.Visibility = Visibility.Visible;
                addQuoteBtn.Visibility = Visibility.Collapsed;
                quoteinfo.Visibility = Visibility.Visible;
                quoteitemdetails.Visibility = Visibility.Visible;
                lblLocation.Text = "Quotes | All Loaded";
                quoteinfo.IsSelected = true;
            }    //Save to the database now all has been checked
            context.SaveChanges();
            qtViewSource.View.MoveCurrentToPrevious();
            qtViewSource.View.MoveCurrentToNext();*/
        }

        #endregion Left Hand Side Button Commands - END

        private void MenuFontFormat_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate the dialog box
            var dlg = new FontDialogBox
            {
                Owner = this,
                FontFamily = emailBody.FontFamily,
                FontSize = emailBody.FontSize,
                FontWeight = emailBody.FontWeight,
                FontStyle = emailBody.FontStyle
            };

            // Configure the dialog box

            // Open the dialog box modally 
            dlg.ShowDialog();

            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                // Update fonts
                emailBody.FontFamily = dlg.FontFamily;
                emailBody.FontSize = dlg.FontSize;
                emailBody.FontWeight = dlg.FontWeight;
                emailBody.FontStyle = dlg.FontStyle;
            }
        }
    }

    
}





