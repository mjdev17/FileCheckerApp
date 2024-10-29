using System.Windows;
using System.Windows.Media.Imaging;
using FileCheckerApp.ViewModels;

namespace FileCheckerApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
