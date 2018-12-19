using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using File = SanityArchiver.Application.Models.Files.File;

namespace SanityArchiver.Application.Models.Directories
{
    public class Directory : Entity
    {
        public readonly ObservableCollection<File> ContainedFiles = new ObservableCollection<File>();
        public readonly ObservableCollection<Directory> SubDirectories = new ObservableCollection<Directory>();

        public Directory(string path) : base(path)
        {
            var info = new DirectoryInfo(path);
            Name = info.Name;
        }

        public void LoadFiles()
        {
            ContainedFiles.Clear();
            new DirectoryInfo(Path).GetFiles().ToList().ForEach(file =>
            {
                if ((file.Attributes & FileAttributes.Hidden & FileAttributes.System) == 0)
                    ContainedFiles.Add(new File(file.FullName));
            });
        }

        public void LoadSubDirectories()
        {
            SubDirectories.Clear();
            new DirectoryInfo(Path).GetDirectories().ToList().ForEach(directory =>
            {
                if ((directory.Attributes & FileAttributes.Hidden & FileAttributes.System) == 0)
                    SubDirectories.Add(new Directory(directory.FullName));
            });
        }

        public override long GetSize()
        {
            long occupiedSpace = 0;
            LoadFiles();
            LoadSubDirectories();

            ContainedFiles.ToList().ForEach(file => occupiedSpace += file.GetSize());
            SubDirectories.ToList().ForEach(directory => occupiedSpace += directory.GetSize());
            return occupiedSpace;
        }
    }
}