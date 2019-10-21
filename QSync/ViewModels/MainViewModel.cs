using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Linq;
using QSync.Views;
using QSync.Utils;
using QSync.Models;

namespace QSync.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region Parameters
    //    private readonly  DialogService;

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title
        {
            get { return "QSync"; }
        }
        #endregion

        #region Constructors
        
        #endregion

        #region Methods

        #endregion

        #region Commands
  //      public RelayCommand<object> SampleCmdWithArgument { get { return new RelayCommand<object>(OnSampleCmdWithArgument); } }

        public ICommand SaveCmd { get { return new RelayCommand(OnSaveTest, AlwaysFalse); } }
        public ICommand NewCmd { get { return new RelayCommand(OnNewTest, AlwaysFalse); } }
        public ICommand OpenCmd { get { return new RelayCommand(OnOpenTest, AlwaysFalse); } }
        public ICommand ShowAboutDialogCmd { get { return new RelayCommand(OnShowAboutDialog, AlwaysTrue); } }
 //       public ICommand ShowInvoiceCmd { get { return new RelayCommand(OnShowInvoiceDialog, AlwaysTrue); } }
        public ICommand ExitCmd { get { return new RelayCommand(OnExitApp, AlwaysTrue); } }

        private bool AlwaysTrue() { return true; }
        private bool AlwaysFalse() { return false; }

        private void OnSampleCmdWithArgument(object obj)
        {
            // TODO
        }

        
        private void OnSaveTest()
        {
            // TODO
        }
        private void OnNewTest()
        {
            // TODO
        }
        private void OnOpenTest()
        {
    

            
        }
        private void OnShowAboutDialog()
        {
            var ab = new Views.About();
            ab.Show();
        }
        
        private void OnExitApp()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }
        #endregion

        #region Events

        #endregion

        public List <InvoiceTypeSel> TypesList { get; set; }

        public MainViewModel()
        {
            TypesList = new List<InvoiceTypeSel>
            {
                new InvoiceTypeSel
                {
                    Type = "ADJ",
                },
                new InvoiceTypeSel
                {
                    Type = "PP",
                },
                new InvoiceTypeSel
                {
                    Type = "OC"
                }
            };
        }
    }

}
