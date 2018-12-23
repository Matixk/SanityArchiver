using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Utils;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public sealed class PropertiesVm : INotifyPropertyChanged
    {
        public object Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public object Path
        {
            get { throw new System.NotImplementedException(); }
        }

        public object Size
        {
            get { throw new System.NotImplementedException(); }
        }

        public ICommand Save => new RelayCommand(SaveChanges);

        public ICommand Cancel
        {
            get { throw new System.NotImplementedException(); }
        }

        private void SaveChanges(object obj)
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