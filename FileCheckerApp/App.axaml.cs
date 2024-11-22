using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using FileCheckerApp.ViewModels;
using FileCheckerApp.Views;

namespace FileCheckerApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                var mainWindow = new MainWindow();  // Create a new MainWindow instance here
                var mainViewModel = new MainViewModel(mainWindow); // Bind MainWindow to ViewModel

                // We assign the DataContext and show the window
                mainWindow.DataContext = mainViewModel;
                desktop.MainWindow = mainWindow;
                mainWindow.Show();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}