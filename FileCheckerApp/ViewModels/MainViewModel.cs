using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FileCheckerApp.Commands;
using FileCheckerApp.Views;

namespace FileCheckerApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView = new ToolView();
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand NavigateToToolCommand { get; }
        public ICommand OpenAboutCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            NavigateToToolCommand = new RelayCommand(_ => CurrentView = new ToolView());
            OpenAboutCommand = new RelayCommand(_ => OpenAbout());
            ExitCommand = new RelayCommand(_ => ExitApplication());
        }

        private void OpenAbout()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
