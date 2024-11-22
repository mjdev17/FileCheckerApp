using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FileCheckerApp.ViewModels;

namespace FileCheckerApp;

public partial class ComparerFileToolView : UserControl
{
    public ComparerFileToolView()
    {
        InitializeComponent();
        DataContext = new FilesAnalyzerViewModel();
    }
}