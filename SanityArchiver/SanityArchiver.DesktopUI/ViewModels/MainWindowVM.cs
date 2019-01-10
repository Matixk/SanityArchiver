using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TextReader;
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
        private bool selectedFileOptionsVisibility;
        private File selectedFile;
        private bool cryptogramOptionVisibility;
        private string fileToSearch = "Search";
        public bool PasteBtnVisibility
        {
            get => pasteBtnVisibility;
            set
            {
                pasteBtnVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool SelectedFileOptionsVisibility
        {
            get => selectedFileOptionsVisibility;
            set
            {
                selectedFileOptionsVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool CryptogramOptionVisibility
        {
            get => cryptogramOptionVisibility;
            set
            {
                cryptogramOptionVisibility = value;
                OnPropertyChanged();
            }
        }
        public File SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                SelectedFileOptionsVisibility = true;
                if (selectedFile.Extension == ".txt" ||
                    selectedFile.Extension == ".ENC")
                {
                    CryptogramOptionVisibility = true;
                }
                else
                {
                    CryptogramOptionVisibility = false;
                }
                OnPropertyChanged();
            }
        }
        private Directory SelectedDirectory { get; set; }
        public string FileToSearch
        {
            get => fileToSearch;
            set
            {
                fileToSearch = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Directory> Directories { get; }
        public ObservableCollection<File> Files { get; }

        public ICommand ExpandDirectory => new RelayCommand(LoadDirectories);
        public ICommand ShowDirectory => new RelayCommand(ShowDirectoryFiles);
        public ICommand Cut => new RelayCommand(CutItem);
        public ICommand Copy => new RelayCommand(CopyItem);
        public ICommand Paste => new RelayCommand(PasteItem);
        public ICommand Delete => new RelayCommand(DeleteItem);
        public ICommand Properties => new RelayCommand(ShowProperties);
        public ICommand OpenFile => new RelayCommand(RunFile);
        public ICommand Cryptographer => new RelayCommand(RunCryptogram);
        public ICommand Search => new RelayCommand(SearchFiles);

        

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
            if (sender == null) return;
            var selectedDirectory = (Directory)sender;
            selectedDirectory.LoadSubDirectories();
        }

        /// <summary> Update ListView files of selected directory. </summary>
        /// <param name="sender">TreeViewItem directory which calls this method.</param>
        private void ShowDirectoryFiles(object sender = null)
        {
            if (sender == null) return;
            SelectedDirectory = (Directory)sender;
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
                PasteBtnVisibility = true;
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

        /// <summary> Open file by default program. </summary>
        /// <param name="sender">ListViewItem file which calls this method.</param>
        private void RunFile(object sender)
        {
            if (sender == null) return;
            var file = ((File) sender);
            var filePath = file.Path;
            var fileExtension = file.Extension;

            if (".txt".Equals(fileExtension))
                new MainWindow(filePath).Show();
        }

        /// <summary> Open Cryptogram dialog for selected file. </summary>
        /// <param name="sender">ListViewItem file which calls this method.</param>
        private void RunCryptogram(object sender)
        {
            if (SelectedFile == null)
            {
                MessageBox.Show("Select file!", "Cryptogram",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var extension = SelectedFile.Extension;

            if (".txt".Equals(extension) || ".ENC".Equals(extension))
                new Cryptogram.Views.Cryptogram(SelectedFile.Path).Show();
            else
                MessageBox.Show(".txt or .ENC extension required", "Cryptogram",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        
        private void SearchFiles(object sender)
        {
            Debug.WriteLine(fileToSearch);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}