using System.IO;
using System.Windows;
using FileArchiver.Models.Decompressor;

namespace FileArchiver.ViewModels
{
    public class DecompressorVm : DefaultVm
    {
        public DecompressorVm() {}

        public DecompressorVm(string path) : base(path) {}
        
        protected override void ActionWithFolder(object obj)
        {
            if (!File.Exists(Path)) return;
            Decompressor.ExtractZip(Path);
            MessageBox.Show("Decompression done!", "Decompression",
                MessageBoxButton.OK, MessageBoxImage.None);
        }
    }
}