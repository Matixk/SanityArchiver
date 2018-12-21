using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Utils;

namespace Cryptogram.ViewModels
{
    public class CryptogramVm : INotifyPropertyChanged
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
                OnPropertyChanged("password");
            }
        }

        public ICommand FileBrowser => new RelayCommand(SelectFile);
        public ICommand Action => new RelayCommand(ActionWithFile);

        public CryptogramVm() {}

        public CryptogramVm(string path = null) => Path = path;

        private void SelectFile(object obj)
        {
            var fileBrowser = new OpenFileDialog
            {
                Filter = @"ENC files (*.ENC)|*.ENC|txt files (*.txt)|*.txt|All files (*.*)|*.*", 
                FilterIndex = 3, RestoreDirectory = true
            };

            if (fileBrowser.ShowDialog() == DialogResult.OK)
            {
                Path = fileBrowser.FileName;
            }
        }

        protected void ActionWithFile(object obj)
        {
            if (path == null || !File.Exists(path)) return;
            var extension = new FileInfo(path).Extension;

            switch (extension)
            {
                case ".txt":
                    EncryptFile();
                    break;
                case ".ENC":
                    DecryptFile();
                    break;
                default:
                    MessageBox.Show($"{extension} must be .txt or .ENC", "Action",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void EncryptFile()
        {
            throw new System.NotImplementedException();
        }

        private void DecryptFile()
        {
            throw new System.NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}