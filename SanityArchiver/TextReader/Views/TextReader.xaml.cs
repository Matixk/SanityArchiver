using TextReader.ViewModels;

namespace TextReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string path)
        {
            InitializeComponent();
            DataContext = new TextFileReaderVM(path);
        }
    }
}