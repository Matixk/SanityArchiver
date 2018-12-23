using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SanityArchiver.DesktopUI.Converters;
using Utils;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public sealed class PropertiesVm : INotifyPropertyChanged
    {
        private readonly string fileName;
        private string name = "";
        private bool readOnly;
        private bool hidden;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Path { get; set; }
        public string Size { get; set; }
        public bool ReadOnly
        {
            get => readOnly;
            set
            {
                readOnly = value;
                OnPropertyChanged();
            }
        }
        public bool Hidden
        {
            get => hidden;
            set
            {
                hidden = value;
                OnPropertyChanged();
            }
        }

        public ICommand Save => new RelayCommand(SaveChanges);
        public ICommand Cancel => new RelayCommand(Close);

        public PropertiesVm(){}

        public PropertiesVm(string path)
        {
            var info = new FileInfo(path);
            Path = info.DirectoryName;
            Name = info.Name;
            fileName = info.Name;
            Size = FileSizeConverter.ConvertToString(info.Length);
            ReadOnly = info.IsReadOnly;
            Hidden = (info.Attributes & FileAttributes.Hidden) != 0;
        }

        private void SaveChanges(object obj)
        {
            if (!File.Exists($"{Path}\\{fileName}")) return;
            var info = new FileInfo($"{Path}\\{fileName}");

            if (ReadOnly != info.IsReadOnly)
                info.IsReadOnly = ReadOnly;
            if (hidden)
                info.Attributes |= FileAttributes.Hidden;
            else
                info.Attributes &= ~FileAttributes.Hidden;
            if (Name != fileName)
                File.Move($"{Path}\\{fileName}", $"{Path}\\{Name}");
            MessageBox.Show("Saved", "Properties save",
                MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void Close(object obj)
        {
            ((Window)obj).Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}