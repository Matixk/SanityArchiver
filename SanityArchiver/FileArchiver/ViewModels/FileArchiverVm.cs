using System.Windows.Input;
using FileArchiver.Views;
using Utils;

namespace FileArchiver.ViewModels
{
    public class FileArchiverVm
    {
        public ICommand Compress => new RelayCommand(OpenCompressDialog);

        private void OpenCompressDialog(object obj)
        {
            new Compressor().Show();
        }
    }
}