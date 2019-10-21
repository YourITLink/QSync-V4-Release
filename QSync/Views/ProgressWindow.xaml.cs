using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QSync.Views
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ProgressManager pm = new ProgressManager();
            pm.BeginWaiting();
            pm.SetProgressMaxValue(10);

            for (int i = 0; i < 10; i++)
            {
                pm.ChangeStatus("Loading " + i.ToString() + " from 10");
                pm.ChangeProgress(i);
                Thread.Sleep(1000);
            }

            pm.EndWaiting();
        }
        //    }
        public class ProgressManager
        {
            private Thread thread;
            private bool canAbortThread = false;
            private ProgressWindow window;

            public void BeginWaiting()
            {
                this.thread = new Thread(this.RunThread);
                this.thread.IsBackground = true;
                this.thread.SetApartmentState(ApartmentState.STA);
                this.thread.Start();
            }
            public void EndWaiting()
            {
                if (this.window != null)
                {
                    this.window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    { this.window.Close(); }));
                    while (!this.canAbortThread) { };
                }
                this.thread.Abort();
            }

            public void RunThread()
            {
                this.window = new ProgressWindow();
                this.window.Closed += new EventHandler(waitingWindow_Closed);
                this.window.ShowDialog();
            }
            public void ChangeStatus(string text)
            {
                if (this.window != null)
                {
                    this.window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    { this.window.StatusText.Text = text; }));
                }
            }
            public void ChangeProgress(double Value)
            {
                if (this.window != null)
                {
                    this.window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    { this.window.Progress.Value = Value; }));
                }
            }
            public void SetProgressMaxValue(double MaxValue)
            {
                Thread.Sleep(100);
                if (this.window != null)
                {
                    this.window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        this.window.Progress.Minimum = 0;
                        this.window.Progress.Maximum = MaxValue;
                    }));
                }
            }
            void waitingWindow_Closed(object sender, EventArgs e)
            {
                Dispatcher.CurrentDispatcher.InvokeShutdown();
                this.canAbortThread = true;
            }
        }
    }
}
