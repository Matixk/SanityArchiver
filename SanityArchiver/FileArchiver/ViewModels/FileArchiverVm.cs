using System.Diagnostics;
using System.Windows.Input;
using FileArchiver.Views;
using Utils;

namespace FileArchiver.ViewModels
{
    public class FileArchiverVm
    {
        public ICommand Compress => new RelayCommand(OpenCompressDialog);

        public ICommand Decompress => new RelayCommand(OpenDecompressDialog);

        private void OpenCompressDialog(object obj)
        {
            new Compressor().Show();
        }

        private void OpenDecompressDialog(object obj)
        {
            new Decompressor().Show();
        }
    }
}