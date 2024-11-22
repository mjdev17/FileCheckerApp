using Avalonia;
using Avalonia.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FileCheckerApp.Commands;
using CommunityToolkit.Mvvm.Input;
using System;

namespace FileCheckerApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public enum ViewType
        {
            FolderComparer,
            FileComparer
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand NavigateToFoldersToolCommand { get; }
        public ICommand NavigateToFilesToolCommand { get; }
        public ICommand OpenAboutCommand { get; }
        public ICommand ExitCommand { get; }

        private readonly Window _mainWindow;

        // Required constructor for window
        public MainViewModel(Window mainWindow)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));

            NavigateToFoldersToolCommand = new Commands.RelayCommand(_ => NavigateToView(ViewType.FolderComparer));
            NavigateToFilesToolCommand = new Commands.RelayCommand(_ => NavigateToView(ViewType.FileComparer));
            OpenAboutCommand = new Commands.RelayCommand(_ => OpenAbout());
            ExitCommand = new Commands.RelayCommand(_ => ExitApplication());

            CurrentView = new ComparerFolderToolView();
        }

        // Empty constructor for the previewer. (Not important)
        public MainViewModel()
        {
            NavigateToFoldersToolCommand = new Commands.RelayCommand(_ => NavigateToView(ViewType.FolderComparer));
            NavigateToFilesToolCommand = new Commands.RelayCommand(_ => NavigateToView(ViewType.FileComparer));
            OpenAboutCommand = new Commands.RelayCommand(_ => OpenAbout());
            ExitCommand = new Commands.RelayCommand(_ => ExitApplication());

            CurrentView = new ComparerFolderToolView();
        }

        private void NavigateToView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.FolderComparer:
                    CurrentView = new ComparerFolderToolView();
                    break;

                case ViewType.FileComparer:
                    CurrentView = new ComparerFileToolView();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void OpenAbout()
        {
            AboutView aboutWindow = new();
            aboutWindow.Show();
        }

        private void ExitApplication()
        {
            _mainWindow.Close();
        }
    }
}
