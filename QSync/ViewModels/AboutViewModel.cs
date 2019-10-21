using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSync.ViewModels
{
    class AboutViewModel : ViewModelBase
    {
        public bool? DialogResult { get { return false; } }

        public string Content
        {
            get
            {
                return "QSync V4.0 WPF" + Environment.NewLine +
                        "Created by Your IT Link" + Environment.NewLine +
                        "www.youritlink.com.au/QSync" + Environment.NewLine +
                        "2019";
            }
        }

        public string VersionText
        {
            get
            {
                var version1 = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                // For external assemblies
                // var ver2 = typeof(Assembly1.ClassOfAssembly1).Assembly.GetName().Version;
                // var ver3 = typeof(Assembly2.ClassOfAssembly2).Assembly.GetName().Version;

                return "QSync v" + version1.ToString();
            }
        }
    }
}
