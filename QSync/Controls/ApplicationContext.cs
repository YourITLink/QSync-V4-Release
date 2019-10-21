using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSync.Controls
{
    public class ApplicationContext
    {
        public static string windowTitleBar { get; set; }
        public static string windowLocation { get; set; }
        public static string windowTitle { get; set; }
        public static string windowStatus { get; set; }
        public static string statusHead { get; set; }
        public static string statusVar { get; set; }


        public ApplicationContext() { }


        public ApplicationContext(string windowTitleBar, string windowLocation, string windowTitle, string windowStatus, string statusHead, string statusVar)
        {
            windowTitleBar = "QSync V4.0 | Able Door Services (NSW) Pty Ltd | by Your IT Link";
            windowLocation = "Main Menu";
            windowTitle = "Welcome to QSync v4.0 - Able Door Services";
            statusHead = "";
            statusVar = "";
            windowStatus = " | QSync | by Your IT Link";
        }
    }
}
