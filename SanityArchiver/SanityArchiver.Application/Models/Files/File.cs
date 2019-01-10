using System.IO;

namespace SanityArchiver.Application.Models.Files
{
    public class File : Entity
    {
        private string extension;

        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                OnPropertyChanged("extension");
            }
        }

        public File(string path) : base(path)
        {
            var info = new FileInfo(path);
            Name = info.Name;
            Extension = info.Extension;
        }

        public override long GetSize()
        {
            return new FileInfo(Path).Length;
        }
    }
}