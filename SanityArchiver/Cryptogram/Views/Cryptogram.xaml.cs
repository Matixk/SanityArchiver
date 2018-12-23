using System.Windows;
using Cryptogram.ViewModels;

namespace Cryptogram.Views
{
    public partial class Cryptogram : Window
    {
        public Cryptogram()
        {
            InitializeComponent();
        }

        public Cryptogram(string path)
        {
            InitializeComponent();
            DataContext = new CryptogramVm(path);
        }
    }
}
