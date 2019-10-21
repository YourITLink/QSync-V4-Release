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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using QSync.Views;
using CrystalDecisions.Shared;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;
using Microsoft.Win32;

namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for quoteCustomEmail.xaml
    /// </summary>
    public partial class quoteCustomEmail : Window
    {
        string reportLocation = Properties.Settings.Default.ReportsFolder + "Quote.rpt";
        string tempLocation = Properties.Settings.Default.TempReportsFolder + "Quotation.pdf";
        string acceptLocation = Properties.Settings.Default.TempReportsFolder + "QuoteAcceptance.pdf";

        public quoteCustomEmail(string qnpass, string custEmail)
        {
            InitializeComponent();
            qtNbr.Text = qnpass;
            emailAdd.Text = custEmail;
            
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void emailQuote_Click(object sender, RoutedEventArgs e)
        {
            ReportDocument mailReport = new ReportDocument();
            {
                //Report Data settings for export
                DataSet myData = new DataSet();
                MySql.Data.MySqlClient.MySqlConnection conn;
                MySql.Data.MySqlClient.MySqlCommand cmd;
                MySql.Data.MySqlClient.MySqlDataAdapter myAdapter;
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                cmd = new MySql.Data.MySqlClient.MySqlCommand();
                myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
                conn.ConnectionString = Properties.Settings.Default.abab1199_qsyncConnectionString;

                try
                {
                    cmd.CommandText = "SELECT * FROM quotes WHERE QuoteNumber = " + qtNbr.Text;
                    cmd.Connection = conn;
                    myAdapter.SelectCommand = cmd;
                    myAdapter.Fill(myData);
                    mailReport.Load(reportLocation);
                    mailReport.Database.Tables[0].SetDataSource(myData.Tables[0]);
                    mailReport.SetParameterValue("Quote", qtNbr.Text);
                    mailReport.SetParameterValue("Quote_Number", qtNbr.Text);
                    //Report Exporting settings
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = tempLocation;
                    CrExportOptions = mailReport.ExportOptions;
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    mailReport.Export();

                    sendmail();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        private void sendmail()
        {
            var server = Properties.Settings.Default.userSMTP;
            var port = Properties.Settings.Default.userSMTPPort;
            var emailfrom = Properties.Settings.Default.userEmail;
            var senderPassword = Properties.Settings.Default.userPW;
        //    var targetHost = Properties.Settings.Default.progTargetName;
        //    string body = "<html><body>hello <b>world</b>.</body></html>";


            using (MailMessage mm = new MailMessage(emailfrom, emailAdd.Text.Trim()))

            {
                mm.Subject = "Able Doors Quotation " + qtNbr.Text;
                mm.Body = msgBody.Text;
                mm.BodyEncoding = Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(tempLocation));
                mm.Attachments.Add(new Attachment(acceptLocation));
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
                    MessageBox.Show("Your Quote has been emailed to the customer. You can see your sent email history from your Outlook session on your main PC.", "Quote Email Sent!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
