using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TextReader.Models;

namespace TextReader.ViewModels
{
    public sealed class TextFileReaderVM : INotifyPropertyChanged
    {
        public string FileName
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public ICommand Open => throw new System.NotImplementedException();

        public ICommand Save => throw new System.NotImplementedException();

        public ICommand SaveAs => throw new System.NotImplementedException();

        public TextFileReader TextFileReader
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public TextFileReaderVM() => TextFileReader = new TextFileReader();
        public TextFileReaderVM(string path)
        {
            TextFileReader = new TextFileReader(path);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}