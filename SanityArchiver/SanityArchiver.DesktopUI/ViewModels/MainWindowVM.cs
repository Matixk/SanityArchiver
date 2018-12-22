using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SanityArchiver.Application.Models;
using Directory = SanityArchiver.Application.Models.Directories.Directory;
using File = SanityArchiver.Application.Models.Files.File;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class MainWindowVM
    {
        public ObservableCollection<Directory> Directories { get; }
        public ObservableCollection<File> Files { get; }

        public ICommand ExpandDirectory
        {
            get { throw new NotImplementedException(); }
        }
        public object ShowDirectory
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a new ViewModel used in MainWindow and loads drivers for TreeView.
        /// </summary>
        public MainWindowVM()
        {
            Files = new ObservableCollection<File>();
            Directories = new ObservableCollection<Directory>();
            try
            {
                DriveInfo.GetDrives().ToList().ForEach(disk =>
                {
                    if (!disk.IsReady) return;
                    var directory = new Directory(disk.RootDirectory.FullName);
                    directory.LoadSubDirectories();
                    Directories.Add(directory);
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Drivers loading failed",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        /// <summary> Update TreeView directories. </summary>
        /// <param name="sender">TreeViewItem which calling this method.</param>
        private void LoadDirectories(object sender)
        {
            Debug.WriteLine(sender);
            var treeViewItem = (TreeViewItem)sender;
            ((Directory)treeViewItem.DataContext).LoadSubDirectories();
        }
        
    }
}