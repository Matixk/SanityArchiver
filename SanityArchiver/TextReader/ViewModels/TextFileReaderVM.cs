using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TextReader.Models;
using Utils;

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
            throw new System.NotImplementedException();
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