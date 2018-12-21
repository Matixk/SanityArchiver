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
            throw new System.NotImplementedException();
        }

        
    }
}