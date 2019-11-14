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


namespace QSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {   
            
            AppCenter.Start("5a1cc2b3-6765-4680-ae41-2c6f9c50bd66",
            typeof(Analytics), typeof(Crashes), typeof(LogLevel));
            AppCenter.LogLevel = LogLevel.Verbose; //Verbose logging of events whilst in BETA
            System.Guid? installId = await AppCenter.GetInstallIdAsync(); //Get the UID of the app which is reporting
         //   new Controls.LoginPage().Show();
         //   new Views.Quotes().Show();
              new MainWindow().Show();
        }
    }
}
