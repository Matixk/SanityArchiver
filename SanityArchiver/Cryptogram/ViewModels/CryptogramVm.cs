using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Cryptogram.ViewModels
{
    public class CryptogramVm : INotifyPropertyChanged
    {
        public string Path
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
        public string Password
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
        public ICommand FileBrowser
        {
            get { throw new System.NotImplementedException(); }
        }
        public ICommand Action
        {
            get { throw new System.NotImplementedException(); }
        }

        public CryptogramVm() {}

        public CryptogramVm(string path)
        {
            Path = path;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}