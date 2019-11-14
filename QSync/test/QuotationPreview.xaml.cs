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
using CrystalDecisions.Shared;
using QSync.Views;

namespace QSync.test
{
    /// <summary>
    /// Interaction logic for QuotationPreview.xaml
    /// </summary>
    public partial class QuotationPreview : Window
    {
        public QuotationPreview(string Quote_Number)
        {
            InitializeComponent();
            quoteLoad.Text = Quote_Number;
        }
        private void QuoteReport_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void RefreshReport(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportDocument qtRpt = new ReportDocument();
                qtRpt.Load("E:/OneDrive - Your IT Link/Your IT Link/Development/QMS 2018/QSync V4/VS 2019/QSync/QSync/QSync/test/AbleQuotation.rpt");
                using (AbleDBEntity context = new AbleDBEntity())
                {
                    var q = (from a in context.quotes
                             join b in context.quoteitems on a.QuoteNumber equals b.QNL
                             where a.QuoteNumber == b.QNL
                             select new
                             {
                                 a.Company,
                                 a.Representative,
                                 a.Address,
                                 a.Email,
                                 a.JobLocation,
                                 a.Phone,
                                 a.ContactPerson,
                                 a.Mobile,
                                 a.QuoteNumber,
                                 b.QNL,
                                 b.ItemDescription,
                //                 b.Price
                             }).ToList();
                    if (q != null)
                    {
                        ParameterFieldDefinitions crParameterFieldDefinitions;
                        ParameterFieldDefinition crParameterFieldDefinition;
                        ParameterValues crParameterValues = new ParameterValues();
                        ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                        crParameterDiscreteValue.Value = quoteLoad.Text;
                        crParameterFieldDefinitions = qtRpt.DataDefinition.ParameterFields;
                        crParameterFieldDefinition = crParameterFieldDefinitions["QuoteLoad"];
                        crParameterValues = crParameterFieldDefinition.CurrentValues;

                        crParameterValues.Clear();
                        crParameterValues.Add(crParameterDiscreteValue);
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                        qtRpt.SetDataSource(q);
                        QuotePreviewReport.ViewerCore.ReportSource = qtRpt;
                        //         crystalReportViewer1.ReportSource = qtRpt;
                        //         crystalReportViewer1.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            
        }
    }
}
