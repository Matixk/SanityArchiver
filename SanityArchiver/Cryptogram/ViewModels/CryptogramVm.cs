using System.Windows.Input;
using Cryptogram.Views;
using Utils;

namespace Cryptogram.ViewModels
{
    public class CryptogramVm
    {
        public ICommand Encrypt => new RelayCommand(OpenEncryptDialog);
        public ICommand Decrypt => new RelayCommand(OpenDecryptDialog);

        private void OpenEncryptDialog(object obj)
        {
            new Encrypt().Show();
        }

        private void OpenDecryptDialog(object obj)
        {
            new Decrypt().Show();
        }
    }
}