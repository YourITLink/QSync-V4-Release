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

namespace QSync.Reporting
{
    /// <summary>
    /// Interaction logic for QuotePreview.xaml
    /// </summary>
    public partial class QuotePreview : Window
    {
        public QuotePreview()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewer.DataSources.Clear();
            this.viewer.DataSources.Add(new Syncfusion.Windows.Reports.ReportDataSource()
            {
                Name = "quote, quote_item", 
                Value = new AbleDBEntity().quotes.Take(100)
                

            });
            this.viewer.RefreshReport();
        }
    }
}
