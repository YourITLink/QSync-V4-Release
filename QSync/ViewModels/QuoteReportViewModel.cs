using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QSync.Reporting;
using System.IO;


namespace QSync.ViewModels
{
    public class ReportingQuotes
    {
        private LoadPreviewQuote _loadPreviewQuote;
        private ReportViewer _reportViewer;
        public ReportingQuotes(LoadPreviewQuote window)
        {
            _loadPreviewQuote = window;
            //    _reportViewer = window.reportViewer;
            Initialize();
        }
        private IEnumerable<quote> quotes = new List<quote>()
        {

        };
        private void Initialize()
        {
            _reportViewer.LocalReport.DataSources.Clear();
            var quoteModels = new ReportDataSource() { Name = "quote_DS", Value = quotes };
            _reportViewer.LocalReport.DataSources.Add(quoteModels);
            var path = Path.GetDirectoryName(Path.GetDirectoryName
            (Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var MainPage = path + @"\QSync\Reporting\ADSQuote.rdlc";
            _reportViewer.LocalReport.ReportPath = MainPage;
            //   _reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.Refresh();
            _reportViewer.RefreshReport();

        }
        /*   public Quoting
           {
           public int QuoteNumber { get; set; }
               public string Representative { get; set; }
               public string repMobile { get; set; }
               public string repID { get; set; }
               public Nullable<System.DateTime> Date1 { get; set; }
               public Nullable<int> MasterID { get; set; }
               public string Company { get; set; }
               public string Address { get; set; }
               public string Suburb { get; set; }
               public string State { get; set; }
               public Nullable<int> PostCode { get; set; }
               public string ContactPerson { get; set; }
               public string Phone { get; set; }
               public string Fax { get; set; }
               public string Mobile { get; set; }
               public string Email { get; set; }
               public string JobLocation { get; set; }
               public string StrataNo { get; set; }
               public string RefNo { get; set; }
               public string SiteContact { get; set; }
               public string SCPhone { get; set; }
               public string SCMobile { get; set; }
               public string SCEmail { get; set; }
               public byte[] Attachments { get; set; }
               public Nullable<int> COD { get; set; }
               public Nullable<int> SvnDays { get; set; }
               public Nullable<int> OrderNo { get; set; }
               public Nullable<int> PROGRESSPAYMENT { get; set; }
               public Nullable<int> Induction { get; set; }
               public string Inductiontime { get; set; }
               public Nullable<int> SWMS { get; set; }
               public string App { get; set; }
               public string Notes { get; set; }
               public string Info { get; set; }
               public System.DateTime ModifyStamp { get; set; }
           }

           private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
           {
               var ID = Convert.ToInt32(e.Parameters[0].Values[0]);
               var quoteitemsgroup = quoteitem.FindAll(x => x.QNL == ID);
               if (e.ReportPath == "EmployeeDetails")
               {
                   var quote = new ReportDataSource() { Name = "quoteitems_DS", Value = quoteitemsgroup };
                   e.DataSources.Add(quoteDetails);
               }*/
    }
}
        
