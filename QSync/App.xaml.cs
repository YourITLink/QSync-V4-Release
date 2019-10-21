using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Windows.Threading;
using log4net;


namespace QSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Log4net config
        private static readonly ILog log = LogManager.GetLogger(typeof(App));


        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            //log4net logging start
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("        =============  QSync Log Session Begin  =============        ");
            AppCenter.Start("5a1cc2b3-6765-4680-ae41-2c6f9c50bd66",
            typeof(Analytics), typeof(Crashes), typeof(LogLevel));
            AppCenter.LogLevel = LogLevel.Verbose; //Verbose logging of events whilst in BETA
            System.Guid? installId = await AppCenter.GetInstallIdAsync(); //Get the UID of the app which is reporting
            
         //   base.OnStartup(e);
            new Controls.LoginPage().Show();
         //   new Views.Quotes().Show();
         //     new MainWindow().Show();
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            MessageBox.Show("A System error has occurred, this error is an unknown error, a log has been captured for diagnosing this issue. You may be able to continue with your current session.", "*** Unhandled Exception forcing shutdown ***",
                MessageBoxButton.OK, MessageBoxImage.Error);
            // Prevent default unhandled exception processing
            e.Handled = true;
        }
    }
}
