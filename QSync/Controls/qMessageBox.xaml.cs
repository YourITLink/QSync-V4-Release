﻿using System;
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
using System.Windows.Forms;


namespace QSync.Controls
{
    /// <summary>
    /// Interaction logic for qMessageBox.xaml
    /// </summary>
    public partial class qMessageBox : Window
    {
        public qMessageBox()
        {
            InitializeComponent();
            txtbMessage.Text = "QSync system message box";
            btnOK.Visibility = Visibility.Visible;
            btnYes.Visibility = Visibility.Collapsed;
            btnNo.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
        }

        

    }
        
}
