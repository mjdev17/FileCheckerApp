using System.Collections.Generic;

namespace FileCheckerApp.Models
{
    public class ComparisonResult
    {
        public int TotalFiles { get; set; }
        public int CommonFiles { get; set; }  
        public int AdditionalFiles { get; set; }
        public string ResultsSummary { get; set; } = string.Empty;
    }
}
