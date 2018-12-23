using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Utils;
using Directory = SanityArchiver.Application.Models.Directories.Directory;
using File = SanityArchiver.Application.Models.Files.File;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public sealed class MainWindowVM : INotifyPropertyChanged
    {
        private string clipboard;
        private bool copy;
        private bool cut;
        private bool pasteBtnVisibility;
        public bool PasteBtnVisibility
        {
            get => pasteBtnVisibility;
            set
            {
                pasteBtnVisibility = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Directory> Directories { get; }
        public ObservableCollection<File> Files { get; }
        public File SelectedFile { get; set; }
        private Directory SelectedDirectory { get; set; }
        public ICommand ExpandDirectory => new RelayCommand(LoadDirectories);
        public ICommand ShowDirectory => new RelayCommand(ShowDirectoryFiles);
        public ICommand Cut => new RelayCommand(CutItem);
        public ICommand Copy => new RelayCommand(CopyItem);
        public ICommand Paste => new RelayCommand(PasteItem);
        public ICommand Delete => new RelayCommand(DeleteItem);
        public ICommand Properties => new RelayCommand(ShowProperties);

        /// <summary>
        /// Creates a new ViewModel used in MainWindow and loads drivers for TreeView.
        /// </summary>
        public MainWindowVM()
        {
            PasteBtnVisibility = false;
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
        /// <param name="sender">TreeViewItem directory which calls this method.</param>
        private void LoadDirectories(object sender)
        {
            var selectedDirectory = (Directory)sender;
            selectedDirectory.LoadSubDirectories();
        }

        /// <summary> Update ListView files of selected directory. </summary>
        /// <param name="sender">TreeViewItem directory which calls this method.</param>
        private void ShowDirectoryFiles(object sender = null)
        {
            if (sender != null) SelectedDirectory = (Directory)sender;
            SelectedDirectory.LoadFiles();
            Files.Clear();
            SelectedDirectory.ContainedFiles.ToList().ForEach(file => Files.Add(file));
        }

        /// <summary> Move file to clipboard. </summary>
        /// <param name="deleteFile"> Define to delete file after paste action. </param>
        private void ToClipboard(bool deleteFile)
        {
            try
            {
                clipboard = SelectedFile.Path;
                cut = deleteFile;
                copy = !deleteFile;
                PasteBtnVisibility = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File moving", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CutItem(object sender) => ToClipboard(true);

        private void CopyItem(object sender) => ToClipboard(false);

        /// <summary> Update ListView files of selected directory. </summary>
        /// <param name="sender">ListViewItem directory which calls this method.</param>
        private void PasteItem(object sender)
        {
            try
            {
                if (clipboard == null) return;
                if (cut)
                    System.IO.File.Move(clipboard, $"{SelectedDirectory.Path}\\{Path.GetFileName(clipboard)}");
                if (copy)
                    System.IO.File.Copy(clipboard, $"{SelectedDirectory.Path}\\{Path.GetFileName(clipboard)}", true);
                else
                    throw new DirectoryNotFoundException(SelectedDirectory.Path);
                PasteBtnVisibility = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File moving", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary> Delete selected file and reload list. </summary>
        /// <param name="sender">ListViewItem file which calls this method.</param>
        private void DeleteItem(object sender)
        {
            try
            {
                System.IO.File.Delete(SelectedFile.Path);
                ShowDirectoryFiles();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Delete file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary> Show new window with selected file properties. </summary>
        /// <param name="sender">ListViewItem file which calls this method.</param>
        private void ShowProperties(object sender)
        {
            new Views.Properties(SelectedFile.Path).Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}