﻿using System;
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
                var item = new TreeViewItem()
                {
                    // Set the header and the full path
                    Header = drive,
                    Tag = drive
                };

                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // Add it to the main tree view
                this.FolderView.Items.Add(item);
            }
        }

        #endregion

        #region Folder Expanded

        /// <summary>
        /// When a folder is expanded, find the sub folders/files
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            TreeViewItem item = (TreeViewItem)sender;

            // If the item only contains dummy data/item
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear the dummy item
            item.Items.Clear();

            // Get full path
            string fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // Create a blank list for directories
            List<string> directories = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // For each directory...
            directories.ForEach(directoryPath =>
            {
                // Create directory item
                TreeViewItem subItem = new TreeViewItem()
                {
                    // Set header as folder name and tag as full path
                    Header = MainWindow.GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                // Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            // Create a blank list for files
            List<string> files = new List<string>();

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // For each file...
            files.ForEach(filePath =>
            {
                // Create file item
                TreeViewItem subItem = new TreeViewItem()
                {
                    // Set header as file name and tag as full path
                    Header = MainWindow.GetFileFolderName(filePath),
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Find the file/folder name
        /// </summary>
        /// <param name="path">Thr full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // If we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            string normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            int lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex < 0)
                return path;

            return path.Substring(lastIndex+1);
        }

        #endregion
    }
}
