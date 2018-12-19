using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Directory = SanityArchiver.Application.Models.Directories.Directory;
using File = SanityArchiver.Application.Models.Files.File;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class MainWindowVM
    {
        public ObservableCollection<Directory> Directories { get; }
        public ObservableCollection<File> Files { get; }
        
        public MainWindowVM()
        {
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
            Files = new ObservableCollection<File>();

        }
    }
}