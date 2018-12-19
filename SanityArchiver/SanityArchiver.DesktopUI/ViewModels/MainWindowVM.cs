using System.Collections.ObjectModel;
using SanityArchiver.Application.Models.Directories;
using SanityArchiver.Application.Models.Files;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class MainWindowVM
    {
        public ObservableCollection<Directory> Directories { get; set; }
        public ObservableCollection<File> Files { get; set; }
        
    }
}