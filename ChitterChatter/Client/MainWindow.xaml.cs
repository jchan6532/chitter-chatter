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

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for each logical drive
                var item = new TreeViewItem();

                // Set the header and path
                item.Header = drive;
                item.Tag = drive;

                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main tree view
                this.FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // If the item only contains dummy data/item
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear the dummy item
            item.Items.Clear();

            string folderName = (string)item.Tag;

            
            throw new NotImplementedException();
        }

        #endregion
    }
}
