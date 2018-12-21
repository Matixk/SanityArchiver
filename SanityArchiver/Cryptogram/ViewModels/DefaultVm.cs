using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Utils;

namespace Cryptogram.ViewModels
{
    public abstract class DefaultVm : INotifyPropertyChanged
    {
        private string path = "";
        private string password = "";

        public string Path
        {
            get => path;
            set
            {
                path = File.Exists(value) ? value : "";
                OnPropertyChanged("path");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value ?? "";
                OnPropertyChanged("path");
            }
        }

        public ICommand FileBrowser => new RelayCommand(SelectFile);
        public ICommand Action => new RelayCommand(ActionWithFile);

        public DefaultVm() {}

        public DefaultVm(string path = null)
        {
            Path = path;
        }

        private void SelectFile(object obj)
        {
            var fileBrowser = new OpenFileDialog();
            if (fileBrowser.ShowDialog() == DialogResult.OK)
                Path = fileBrowser.FileName;
        }

        protected abstract void ActionWithFile(object obj);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}