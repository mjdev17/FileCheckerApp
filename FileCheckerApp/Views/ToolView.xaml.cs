using System.Windows;
using System.Windows.Controls;
using FileCheckerApp.ViewModels;

namespace FileCheckerApp.Views
{
    /// <summary>
    /// Lógica de interacción para ToolView.xaml
    /// </summary>
    public partial class ToolView : System.Windows.Controls.UserControl
    {
        public ToolView()
        {
            InitializeComponent();
            DataContext = new FilesAnalyzerViewModel();
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            txt_dir1.Text = string.Empty;
            txt_dir2.Text = string.Empty;
            txt_Results.Text = "Select both directories to analyze...";

        }
    }
}
