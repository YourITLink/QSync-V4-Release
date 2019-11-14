using Syncfusion.Windows.Reports.Designer;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : RibbonWindow
    {

        public Window1()
        {
            InitializeComponent();
         //   SkinStorage.SetVisualStyle(this, "Office2013");
            this.Loaded += new RoutedEventHandler(Window_Loaded);

        }

        public object ReportDesignerControl { get; private set; }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //   this.ReportDesignerControl.DesignMode = DesignMode.RDLC;
         //   ReportDesignerControl.OpenReport(@"../../../ReportTemplate/RDLC/Sales Dashboard.rdlc");
        }
    }
        
}
