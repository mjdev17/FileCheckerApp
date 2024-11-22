using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using System.Windows.Input;
using FileCheckerApp.Services;
using FileCheckerApp.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace FileCheckerApp.ViewModels
{
    public class FoldersAnalyzerViewModel : ObservableObject
    {
        private string? _selectedDirectory1;
        private string? _selectedDirectory2;
        private string _results = string.Empty;

        private int _totalFiles;
        private int _commonFiles;
        private int _additionalFiles;
        private string _operationTime = "00:00:00";

        public string? SelectedDirectory1
        {
            get => _selectedDirectory1;
            set => SetProperty(ref _selectedDirectory1, value);
        }

        public string? SelectedDirectory2
        {
            get => _selectedDirectory2;
            set => SetProperty(ref _selectedDirectory2, value);
        }

        public string Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        public int TotalFiles
        {
            get => _totalFiles;
            set => SetProperty(ref _totalFiles, value);
        }

        public int CommonFiles
        {
            get => _commonFiles;
            set => SetProperty(ref _commonFiles, value);
        }

        public int AdditionalFiles
        {
            get => _additionalFiles;
            set => SetProperty(ref _additionalFiles, value);
        }

        public string OperationTime
        {
            get => _operationTime;
            set => SetProperty(ref _operationTime, value);
        }

        public ICommand SelectDir1Command { get; }
        public ICommand SelectDir2Command { get; }
        public ICommand AnalyzeCommand { get; }

        public FoldersAnalyzerViewModel()
        {
            SelectDir1Command = new RelayCommand(SelectDirectory1);
            SelectDir2Command = new RelayCommand(SelectDirectory2);
            AnalyzeCommand = new RelayCommand(AnalyzeFiles);
            Results = "Select both directories to analyze...";
        }

        // Método para seleccionar el directorio 1
        private async void SelectDirectory1(object? parameter)
        {
            // Accedemos al MainWindow de la aplicación de forma segura
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;

                // Usamos StorageProvider para seleccionar un directorio
                var result = await mainWindow.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = "Select a folder"
                });

                // Si se seleccionó una carpeta, se guarda la ruta.
                if (result != null && result.Any())
                {
                    SelectedDirectory1 = result.First().Path.LocalPath;
                }
            }
        }

        // Método para seleccionar el directorio 2
        private async void SelectDirectory2(object? parameter)
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;

                var result = await mainWindow.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = "Select a folder"
                });

                if (result != null && result.Any())
                {
                    SelectedDirectory2 = result.First().Path.LocalPath;
                }
            }
        }

        private bool CanAnalyze(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(SelectedDirectory1) && !string.IsNullOrWhiteSpace(SelectedDirectory2);
        }

        private void AnalyzeFiles(object? parameter)
        {

            if (!CanAnalyze(parameter))
            {
                Results = "Error: Select both routes before continuing.";
                return;
            }

            try
            {
                var comparerService = new ComparerService();

                // Measuring operation time
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                var comparisonResult = comparerService.CompareDirectories(SelectedDirectory1!, SelectedDirectory2!);

                stopwatch.Stop();

                // Update summary properties
                TotalFiles = comparisonResult.TotalFiles;
                CommonFiles = comparisonResult.CommonFiles;
                AdditionalFiles = comparisonResult.AdditionalFiles;
                OperationTime = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");

                // Update the summary in the UI
                Results = comparisonResult.ResultsSummary;
            }
            catch (Exception ex)
            {
                Results = $"An error occurred during analysis: {ex.Message}";
            }
        }
    }
}
