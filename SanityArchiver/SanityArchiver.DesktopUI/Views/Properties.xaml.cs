using System.Windows;
using SanityArchiver.DesktopUI.ViewModels;

namespace SanityArchiver.DesktopUI.Views
{
    public partial class Properties : Window
    {
        public Properties()
        {
            InitializeComponent();
        }
        
        public Properties(string path)
        {
            InitializeComponent();
            DataContext = new PropertiesVm(path);
        }
    }
}
