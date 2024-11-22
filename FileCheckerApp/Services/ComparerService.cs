using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using FileCheckerApp.Models;

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

        // #####################################################
        // #####################################################
        // ######### COMPARE FILES IN TWO DIRECTORIES ##########
        // #####################################################
        // #####################################################
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

        // #####################################################
        // #####################################################
        // ### COMPARE THE CONTENT OF TWO FILES LINE BY LINE ###
        // #####################################################
        // #####################################################
        public ComparisonResult CompareFilesLineByLine(string file1Path, string file2Path)
        {
            // Read the files line by line
            var file1Lines = File.ReadLines(file1Path).ToList();
            var file2Lines = File.ReadLines(file2Path).ToList();

            // Compare lines
            var commonLines = file1Lines.Intersect(file2Lines).ToList();
            var missingLines = file1Lines.Except(file2Lines).ToList();
            var additionalLines = file2Lines.Except(file1Lines).ToList();

            return new ComparisonResult
            {
                TotalFiles = file1Lines.Count + file2Lines.Count,
                CommonFiles = commonLines.Count,
                AdditionalFiles = additionalLines.Count,
                ResultsSummary = GenerateResultsSummaryForFiles(file1Path, file2Path, commonLines, missingLines, additionalLines)
            };
        }

        // Generate summary of results for files
        private string GenerateResultsSummaryForFiles(string file1Path, string file2Path, List<string> commonLines, List<string> missingLines, List<string> additionalLines)
        {
            StringBuilder result = new();
            result.AppendLine($"Total lines compared between files: '{file1Path}' and '{file2Path}'.\n");

            // Common Lines
            if (commonLines.Count > 0)
            {
                AppendLineResults(result, "common", commonLines, "✓");
            }
            else
            {
                result.AppendLine("No common lines were found in both files.\n");
            }

            // Missing Lines
            if (missingLines.Count > 0)
            {
                AppendLineResults(result, "missing in file 2", missingLines, "✗");
            }
            else
            {
                result.AppendLine("No missing lines in file 2.\n");
            }

            // Additional Lines
            if (additionalLines.Count > 0)
            {
                AppendLineResults(result, "in file 2", additionalLines, "-");
            }
            else
            {
                result.AppendLine("There are no additional lines in file 2.\n");
            }

            return result.ToString();
        }

        // Auxiliary method for adding results from lines with symbols
        private void AppendLineResults(StringBuilder result, string description, List<string> lines, string symbol)
        {
            result.AppendLine($"Lines {description}: [{lines.Count}]");
            lines.ForEach(line => result.AppendLine($"{symbol} {line}"));
            result.AppendLine();
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

