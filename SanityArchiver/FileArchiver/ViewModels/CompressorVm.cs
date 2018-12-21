using System.IO;
using System.Windows;
using FileArchiver.Models.Compressor;

namespace FileArchiver.ViewModels
{
    public class CompressorVm : DefaultVm
    {
        private CompressionSpeed speed;
        
        public CompressionSpeed Speed
        {
            get => speed;
            set
            {
                speed = value;
                OnPropertyChanged("speed");
            }
        }
        
        public CompressorVm() {}

        public CompressorVm(string path) : base(path) {}
        
        protected override void ActionWithFolder(object obj)
        {
            if (!Directory.Exists(Path)) return;
            Compressor.CreateZipFromDirectory(Path);
            MessageBox.Show("Compression done!", "Compression",
                MessageBoxButton.OK, MessageBoxImage.None);
        }

        
    }
}