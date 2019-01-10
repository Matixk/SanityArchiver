using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Directory = SanityArchiver.Application.Models.Directories.Directory;

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

        public static List<File> SearchFile(string name)
        {
            var files = new List<File>();

            DriveInfo.GetDrives().ToList().ForEach(info =>
            {
                if (!info.IsReady) return;
                files.AddRange(new Directory(info.Name).SearchFile(new Regex(name)));
            });
            return files;
        }
    }
}