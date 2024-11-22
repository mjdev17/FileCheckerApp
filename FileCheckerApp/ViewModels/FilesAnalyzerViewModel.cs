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
using System.Collections.Generic;
using FileCheckerApp.Utils.FileFormats;

namespace FileCheckerApp.ViewModels
{
    public class FilesAnalyzerViewModel : ObservableObject
    {
        private string? _selectedFile1; 
        private string? _selectedFile2; 
        private string _results = string.Empty; 

        private int _totalLines;
        private int _commonLines;
        private int _additionalLines;
        private string _operationTime = "00:00:00";

        public string? SelectedFile1
        {
            get => _selectedFile1;
            set => SetProperty(ref _selectedFile1, value);
        }

        public string? SelectedFile2
        {
            get => _selectedFile2;
            set => SetProperty(ref _selectedFile2, value);
        }

        public string Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        public int TotalLines
        {
            get => _totalLines;
            set => SetProperty(ref _totalLines, value);
        }

        public int CommonLines
        {
            get => _commonLines;
            set => SetProperty(ref _commonLines, value);
        }

        public int AdditionalLines
        {
            get => _additionalLines;
            set => SetProperty(ref _additionalLines, value);
        }

        public string OperationTime
        {
            get => _operationTime;
            set => SetProperty(ref _operationTime, value);
        }

        public ICommand SelectFile1Command { get; }
        public ICommand SelectFile2Command { get; }
        public ICommand AnalyzeCommand { get; }

        public FilesAnalyzerViewModel()
        {
            SelectFile1Command = new RelayCommand(SelectFile1);
            SelectFile2Command = new RelayCommand(SelectFile2);
            AnalyzeCommand = new RelayCommand(AnalyzeFiles);
            Results = "Select both files to analyze...";
        }

        // Method to select file 1
        private async void SelectFile1(object? parameter)
        {
            // We access the MainWindow of the application safely
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;

                var fileTypeFilters = new List<FilePickerFileType>();
                foreach (var category in Enum.GetValues<FileCategory>())
                {
                    fileTypeFilters.Add(new FilePickerFileType(category.ToString())
                    {
                        Patterns = FileFormats.Formats[category]
                    });
                }

                // We use StorageProvider to select a file
                var result = await mainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Select a file",
                    FileTypeFilter = fileTypeFilters
                });

                // If a folder was selected, the path is saved
                if (result != null && result.Any())
                {
                    SelectedFile1 = result.First().Path.LocalPath;
                }
            }
        }

        // Method to select file 2
        private async void SelectFile2(object? parameter)
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;

                var fileTypeFilters = new List<FilePickerFileType>();
                foreach (var category in Enum.GetValues<FileCategory>())
                {
                    fileTypeFilters.Add(new FilePickerFileType(category.ToString())
                    {
                        Patterns = FileFormats.Formats[category]
                    });
                }

                var result = await mainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Select a file",
                    FileTypeFilter = fileTypeFilters
                });

                if (result != null && result.Any())
                {
                    SelectedFile2 = result.First().Path.LocalPath;
                }
            }
        }

        // Validation to avoid empty fields
        private bool CanAnalyze(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(SelectedFile1) && !string.IsNullOrWhiteSpace(SelectedFile2);
        }

        // Method for analyzing files
        private void AnalyzeFiles(object? parameter)
        {

            if (!CanAnalyze(parameter))
            {
                Results = "Error: Select both files before continuing.";
                return;
            }

            try
            {
                var comparerService = new ComparerService();

                // Measuring operation time
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                var comparisonResult = comparerService.CompareFilesLineByLine(SelectedFile1!, SelectedFile2!);

                stopwatch.Stop();

                // Update summary properties
                TotalLines = comparisonResult.TotalFiles;
                CommonLines = comparisonResult.CommonFiles;
                AdditionalLines = comparisonResult.AdditionalFiles;
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
