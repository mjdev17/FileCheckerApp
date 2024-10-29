using FileCheckerApp.Models;
using System.IO;
using System.Text;

namespace FileCheckerApp.Services
{
    public class ComparerService
    {
        // Get only the names of the files in a directory
        private HashSet<string> GetFileNamesWithoutPath(string dir)
        {
            var files = new HashSet<string>();
            foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
            {
                files.Add(Path.GetFileName(file));
            }
            return files;
        }

        // Compare files in two directories
        public ComparisonResult CompareDirectories(string dir1, string dir2)
        {
            var filesDir1 = GetFileNamesWithoutPath(dir1);
            var filesDir2 = GetFileNamesWithoutPath(dir2);

            var commonFiles = filesDir1.Intersect(filesDir2).ToList();
            var missingFiles = filesDir1.Except(filesDir2).ToList();
            var additionalFiles = filesDir2.Except(filesDir1).ToList();

            return new ComparisonResult
            {
                TotalFiles = filesDir1.Count + filesDir2.Count,
                CommonFiles = commonFiles.Count,
                AdditionalFiles = additionalFiles.Count,
                ResultsSummary = GenerateResultsSummary(dir1, dir2, commonFiles, missingFiles, additionalFiles)
            };
        }

        // Generate summary of results
        private string GenerateResultsSummary(string dir1, string dir2, List<string> commonFiles, List<string> missingFiles, List<string> additionalFiles)
        {
            StringBuilder result = new();
            result.AppendLine($"{commonFiles.Count + missingFiles.Count + additionalFiles.Count} files were analyzed in the directory: '{dir1}'.\n");

            // Common Files
            if (commonFiles.Count > 0)
            {
                AppendFileResults(result, "common", commonFiles, "✓");
            }
            else
            {
                result.AppendLine("No common files were found in both directories.\n");
            }

            // Missing Files
            if (missingFiles.Count > 0)
            {
                AppendFileResults(result, "missing in directory 2", missingFiles, "✗");
            }
            else
            {
                result.AppendLine("No missing files in directory 2.\n");
            }

            // Additional Files
            if (additionalFiles.Count > 0)
            {
                AppendFileResults(result, "in directory 2", additionalFiles, "-");
            }
            else
            {
                result.AppendLine("There are no additional files in directory 2.\n");
            }

            return result.ToString();
        }

        // Auxiliary method for adding results from files with symbols
        private void AppendFileResults(StringBuilder result, string description, List<string> files, string symbol)
        {
            result.AppendLine($"Files {description}: [{files.Count}]");
            files.ForEach(file => result.AppendLine($"{symbol} {file}"));
            result.AppendLine();
        }
    }

}

