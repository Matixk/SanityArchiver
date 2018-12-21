using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TextReader.Models;
using Utils;
using MessageBox = System.Windows.MessageBox;

namespace TextReader.ViewModels
{
    public sealed class TextFileReaderVM : INotifyPropertyChanged
    {
        private string fileName = "Text Reader";

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged("fileName");
            }
        }

        public ICommand Open => new RelayCommand(OpenFile);

        public ICommand Save => new RelayCommand(SaveFile);

        public ICommand SaveAs => new RelayCommand(SaveFileAs);

        public TextFileReader TextFileReader { get; }

        public TextFileReaderVM() => TextFileReader = new TextFileReader();
        public TextFileReaderVM(string path)
        {
            TextFileReader = new TextFileReader(path);
        }

        private void OpenFile(object obj)
        {
            var fileBrowser = new OpenFileDialog
            {
                Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*", FilterIndex = 2, RestoreDirectory = true
            };

            if (fileBrowser.ShowDialog() == DialogResult.OK &&
                ".txt".Equals(new FileInfo(fileBrowser.FileName).Extension))
            {
                TextFileReader.Path = fileBrowser.FileName;
                FileName = fileBrowser.SafeFileName;
            }
            else
            {
                MessageBox.Show("Couldn't open this file", "Open file",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFile(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void SaveFileAs(object obj)
        {
            throw new System.NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}