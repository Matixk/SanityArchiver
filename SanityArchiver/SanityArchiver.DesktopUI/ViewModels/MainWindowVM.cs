using System.Collections.ObjectModel;
using SanityArchiver.Application.Models.Directories;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class MainWindowVM
    {
        public ObservableCollection<Directory> Directories { get; set; }
        public ObservableCollection<Directory> Files { get; set; }
        
    }
}