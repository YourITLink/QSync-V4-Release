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
using Syncfusion.Linq;
using QSync.Views;
using CrystalDecisions.Shared;
using System.Data;

namespace QSync.test
{
    /// <summary>
    /// Interaction logic for crystal.xaml
    /// </summary>
    public partial class crystal : Window
    {
        public static int qnLoad;

        public crystal(string Quote_Number)
        {

            InitializeComponent();
            qnpass.Text = Quote_Number;
            //    qnLoad = Convert.ToInt32(qnpass);

        }
        private void CrystalReportViewer1_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            DataSet myData = new DataSet();
            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;
            MySql.Data.MySqlClient.MySqlDataAdapter myAdapter;

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();

            conn.ConnectionString = "server=abledoors.net.au;uid = abab1199_qadacc;" +
                "pwd=@mjK%U5eyNOK;database=abab1199_qsync;convert zero datetime=True;persistsecurityinfo=True";

            try
            {
                cmd.CommandText = "SELECT quotes.QuoteNumber, quotes.Company, quotes.Address, quotes.ContactPerson, " +
                    "quotes.Phone, quotes.Mobile, quotes.Email, quotes.StrataNo, quotes.RefNo, quotes.JobLocation, " +
                    "quotes.SiteContact, quotes.SCPhone, quotes.SCMobile, quotes.SCEmail, quotes.Representative FROM quotes;" +
                    "SELECT quoteitems.QNL, quoteitems.ItemDescription FROM quoteitems";
                cmd.Connection = conn;

                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData);
                rd.Load("E:/OneDrive - Your IT Link/Your IT Link/Development/QMS 2018/QSync V4/VS 2019/QSync/QSync/QSync/test/Quote.rpt");
                //    myReport.Load("E:/OneDrive - Your IT Link/Your IT Link/Development/QMS 2018/QSync V4/VS 2019/QSync/QSync/QSync/test/Quote.rpt");
                rd.Database.Tables[0].SetDataSource(myData.Tables[0]);
                rd.Database.Tables[1].SetDataSource(myData.Tables[1]);
                //    myViewer.ReportSource = myReport;
                rd.SetParameterValue("Quote", qnpass.Text);
                rd.SetParameterValue("Quote_Number", qnpass.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
