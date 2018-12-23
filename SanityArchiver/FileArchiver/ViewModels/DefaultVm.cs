using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Utils;

namespace FileArchiver.ViewModels
{
    public abstract class DefaultVm : INotifyPropertyChanged
    {
        private string path;
        public string Path
        {
            get => path;
            set
            {
                path = (File.Exists(value) || Directory.Exists(value)) ? value : "";
                OnPropertyChanged("path");
            }
        }

        public ICommand DirectoryBrowser => new RelayCommand(SelectFolder);
        public ICommand FileBrowser => new RelayCommand(SelectFile);
        public ICommand Action => new RelayCommand(ActionWithFolder);

        public DefaultVm() {}

        public DefaultVm(string path = null)
        {
            Path = path;
        }

        private void SelectFolder(object obj)
        {
            var directoryBrowser = new FolderBrowserDialog();
            if (directoryBrowser.ShowDialog() == DialogResult.OK)
                Path = directoryBrowser.SelectedPath;
        }

        private void SelectFile(object obj)
        {
            var fileBrowser = new OpenFileDialog();
            if (fileBrowser.ShowDialog() == DialogResult.OK)
                Path = fileBrowser.FileName;
        }

        protected abstract void ActionWithFolder(object obj);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}