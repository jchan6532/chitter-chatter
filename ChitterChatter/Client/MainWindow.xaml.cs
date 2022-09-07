using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

using Client.Services;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext

            #region comments
            //try
            //{
            //    ViewModels.Client client = new ViewModels.Client();
            //}
            //catch (Exception e)
            //{
            //    LoggingService.Log(e.Message, "EXCEPTION");
            //    MessageBox.Show(e.Message);
            //    this.Close();
            //}
            #endregion
        }

        #endregion

    }
}