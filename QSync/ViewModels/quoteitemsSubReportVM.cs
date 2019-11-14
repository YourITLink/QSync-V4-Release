using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.IO;
using QSync.Views;
using QSync.Models;
using QSync.Reporting;

namespace QSync.ViewModels
{
    class quoteitemsSubReportVM
    {
        private Quotes _quotesView;
        private ReportViewer _reportViewer;
        public quoteitemsSubReportVM(Quotes window)
        {
            _quotesView = window;
         //   _reportViewer = window.reportViewer;
            Initialize();

        }

        private IEnumerable<quote> quotes = new List<quote>()
       {
           
       };
        private void Initialize()
        {
            _reportViewer.LocalReport.DataSources.Clear();
            var quotemodels = new ReportDataSource() { Name = "quote_DS", Value = quotes };
            _reportViewer.LocalReport.DataSources.Add(quotemodels);
            var path = Path.GetDirectoryName(Path.GetDirectoryName
(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var MainPage = path + @"\QSync\Reporting\quoteReport.rdlc";
            _reportViewer.LocalReport.ReportPath = MainPage;
            _reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.Refresh();
            _reportViewer.RefreshReport();

        }
        private List<quoteitem> quoteitems = new List<quoteitem>()
{

};
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
         /*   //var ID = Convert.ToInt32(e.Parameters[0].Values[0]);
            var quoteid = quoteitems.FindAll(x => x.QuoteNumber == quoteitems);
            if (e.ReportPath == "EmployeeDetails")
            {
                var employeeDetails = new ReportDataSource() { Name = "quoteitems_DS", Value = employeegroup };
                e.DataSources.Add(employeeDetails);*/
            }
        }
    }
