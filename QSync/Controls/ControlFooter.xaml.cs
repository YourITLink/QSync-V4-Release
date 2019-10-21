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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using QSync;

namespace QSync.Controls
{
    /// <summary>
    /// Interaction logic for ControlFooter.xaml
    /// </summary>
    public partial class ControlFooter : UserControl
    {
        public string statusrec;
        public string statusText;
        public ControlFooter()
        {
            InitializeComponent();
            StatusDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timeStatus_Tick;
            LiveTime.Start();
        //    Closed += new EventHandler(MainWindow_Closed);
            // Window Status Information
            //  statusHead = "For support call";
            //  statusVar = "0410 669 634";
            //  Title = ApplicationContext.windowTitle;
            //  lblLocation.Text = ApplicationContext.windowLocation;
            //  title.Text = ApplicationContext.windowTitleBar;
            //  Properties.Settings.Default.Status = statusHead + " " + statusVar + " " + ApplicationContext.windowStatus; // For Support Call 0410 669 634 | QSync | by Your IT Link
            //  Properties.Settings.Default.Save();


        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        void timeStatus_Tick(object sender, EventArgs e)
        {
            StatusTime.Content = DateTime.Now.ToString("HH:mm:ss");
            capStat();
            numStat();
            insStat();
            versionUpdate();
        }
        private void capStat()
        {
            if (System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
            {
                StatusCaps.Text = "CAPS";
            }
            else
            {
                StatusCaps.Text = "";
            }
        }
        private void numStat()
        {
            if (System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.NumLock))
            {
                StatusNum.Text = "NUM";
            }
            else
            {
                StatusNum.Text = "";
            }
        }
        private void insStat()
        {
            if (System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.Insert))
            {
                StatusIns.Text = "INS";
            }
            else
            {
                StatusIns.Text = "";
            }
        }
        private void versionUpdate()
        {
            StatusText.Text = Properties.Settings.Default.Status;
            StatusVersion.Text = Properties.Settings.Default.Version;
        }
    }
}
