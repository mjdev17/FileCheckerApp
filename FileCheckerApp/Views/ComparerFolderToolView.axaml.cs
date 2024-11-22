using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FileCheckerApp.ViewModels;

namespace FileCheckerApp
{
    public partial class ComparerFolderToolView : UserControl
    {
        public ComparerFolderToolView()
        {
            InitializeComponent();
            DataContext = new FoldersAnalyzerViewModel();
        }
    }
}