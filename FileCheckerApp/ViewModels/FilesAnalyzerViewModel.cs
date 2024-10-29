using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileCheckerApp.Services;
using FileCheckerApp.Commands;

namespace FileCheckerApp.ViewModels
{
    public class FilesAnalyzerViewModel : INotifyPropertyChanged
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
            set { _selectedDirectory1 = value; OnPropertyChanged(); }
        }

        public string? SelectedDirectory2
        {
            get => _selectedDirectory2;
            set { _selectedDirectory2 = value; OnPropertyChanged(); }
        }

        public string Results
        {
            get => _results;
            set { _results = value; OnPropertyChanged(); }
        }

        public int TotalFiles
        {
            get => _totalFiles;
            set { _totalFiles = value; OnPropertyChanged(); }
        }

        public int CommonFiles
        {
            get => _commonFiles;
            set { _commonFiles = value; OnPropertyChanged(); }
        }

        public int AdditionalFiles
        {
            get => _additionalFiles;
            set { _additionalFiles = value; OnPropertyChanged(); }
        }

        public string OperationTime
        {
            get => _operationTime;
            set { _operationTime = value; OnPropertyChanged(); }
        }

        public ICommand SelectDir1Command { get; }
        public ICommand SelectDir2Command { get; }
        public ICommand AnalyzeCommand { get; }

        public FilesAnalyzerViewModel()
        {
            SelectDir1Command = new RelayCommand(SelectDirectory1);
            SelectDir2Command = new RelayCommand(SelectDirectory2);
            AnalyzeCommand = new RelayCommand(AnalyzeFiles, CanAnalyze);
            Results = "Select both directories to analyze...";
        }

        private void SelectDirectory1(object? parameter)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedDirectory1 = dialog.SelectedPath;
            }
        }

        private void SelectDirectory2(object? parameter)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedDirectory2 = dialog.SelectedPath;
            }
        }

        private bool CanAnalyze(object? parameter)
        {
            return !string.IsNullOrEmpty(SelectedDirectory1) && !string.IsNullOrEmpty(SelectedDirectory2);
        }

        private void AnalyzeFiles(object? parameter)
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
