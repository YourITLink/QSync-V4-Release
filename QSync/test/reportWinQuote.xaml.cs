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

namespace QSync.test
{
    /// <summary>
    /// Interaction logic for reportWinQuote.xaml
    /// </summary>
    public partial class reportWinQuote : Window
    {
        public static int qnLoad;
     //   string pdfFile = "C:/QSync/_AbleQuote.pdf";
        string reportLocation = Properties.Settings.Default.ReportsFolder + "Quote.rpt";
        string tempLocation = Properties.Settings.Default.TempReportsFolder + "Quotation.pdf";
        string acceptLocation = Properties.Settings.Default.TempReportsFolder + "QuoteAcceptance.pdf";

        public reportWinQuote(string Quote_Number, string custemail)
        {
            InitializeComponent();
            qnpass.Text = Quote_Number;
            email_To.Text = custemail;
        }

        public void editEmail_Click(object sender, RoutedEventArgs e)
        {
            // this.Visibility = Visibility.Hidden;
            var ee = new Views.quoteCustomEmail(qnpass.Text, email_To.Text);
            ee.Show();
        }

        private void CrystalReportViewer1_Loaded(object sender, RoutedEventArgs e)
        {
            
            ReportDocument myReport = new ReportDocument();
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
                cmd.CommandText = "SELECT * FROM quotes WHERE QuoteNumber = " + qnpass.Text;
                cmd.Connection = conn;
                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData);
                myReport.Load(reportLocation);
                myReport.Database.Tables[0].SetDataSource(myData.Tables[0]);
                myReport.SetParameterValue("Quote", qnpass.Text);
                myReport.SetParameterValue("Quote_Number", qnpass.Text);
                CrystalReportViewer1.ViewerCore.ReportSource = myReport;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Error!.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void exportQuote_Click(object sender, RoutedEventArgs e)
        {
            ReportDocument expReport = new ReportDocument();
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
                cmd.CommandText = "SELECT * FROM quotes WHERE QuoteNumber = " + qnpass.Text;
                cmd.Connection = conn;
                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData);
                expReport.Load(reportLocation);
                expReport.Database.Tables[0].SetDataSource(myData.Tables[0]);
                expReport.SetParameterValue("Quote", qnpass.Text);
                expReport.SetParameterValue("Quote_Number", qnpass.Text);
                ExportOptions exportOption;
                DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf Files|*.pdf";
                if (sfd.ShowDialog().ToString().Equals("OK"))
                {
                    diskFileDestinationOptions.DiskFileName = sfd.FileName;
                }
                exportOption = expReport.ExportOptions;
                {
                    exportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOption.DestinationOptions = diskFileDestinationOptions;
                    exportOption.FormatOptions = new PdfRtfWordFormatOptions();
                }
                expReport.Export();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Connectivity Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void emailQuote_Click(object sender, RoutedEventArgs e)
        {
         //   var reportLocation = Properties.Settings.Default.ReportsFolder+"Quote.rpt";
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
                    cmd.CommandText = "SELECT * FROM quotes WHERE QuoteNumber = " + qnpass.Text;
                    cmd.Connection = conn;
                    myAdapter.SelectCommand = cmd;
                    myAdapter.Fill(myData);
                    mailReport.Load(reportLocation); 
                    mailReport.Database.Tables[0].SetDataSource(myData.Tables[0]);
                    mailReport.SetParameterValue("Quote", qnpass.Text);
                    mailReport.SetParameterValue("Quote_Number", qnpass.Text);
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
              var server = Properties.Settings.Default.progSMTP;
              var port = Properties.Settings.Default.progSMTPPort; 
              var emailfrom = Properties.Settings.Default.progEmail; 
              var senderPassword = Properties.Settings.Default.progPW;
              var targetHost = Properties.Settings.Default.progTargetName;
              string body = "<html><body>hello <b>world</b>.</body></html>";


            using (MailMessage mm = new MailMessage(emailfrom, email_To.Text.Trim()))

            {
                mm.Subject = "Able Doors Quotation " + qnpass.Text;
                mm.Body = body;
                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(tempLocation));
                mm.Attachments.Add(new Attachment(acceptLocation));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = server;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(emailfrom, senderPassword);
                smtp.UseDefaultCredentials = true;
         //       smtp.TargetName = targetHost;
                smtp.Credentials = NetworkCred;
                smtp.Port = port;
                
                try
                {
                    smtp.Send(mm);
                    MessageBox.Show("Your Quote has been emailed to your customer, Please ensure you keep an eye on the Sales Email inbox for replies or bounces.", "Email Sent!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

    }
}

