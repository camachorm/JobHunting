#region Usings

using System;
using System.Linq;

#endregion

namespace TenorReporting.Models
{
    public class UnparsedLineModel
    {
        public UnparsedLineModel(string line)
        {
            Line = line;

            if (string.IsNullOrEmpty(line)) return;

            LineEntries = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            double parsedValue = -1;

            IsValid = line.IndexOf(',') >= 0 &&
                      LineEntries.Length == 3 &&
                      !string.IsNullOrEmpty(PortfolioID) &&
                      (Tenor?.Any(char.IsDigit)).GetValueOrDefault() &&
                      (Tenor?.Any(char.IsLetter)).GetValueOrDefault() &&
                      double.TryParse(Value, out parsedValue) &&
                      ParsedTenor.IsValid;

            ParsedValue = parsedValue;
        }

        public string[] LineEntries { get; }

        public string Line { get; }

        public string Tenor => LineEntries?.Length > 0 ? LineEntries[0] : null;
        public TenorModel ParsedTenor => LineEntries?.Length > 0 ? new TenorModel(LineEntries[0]) : null;
        public string PortfolioID => LineEntries?.Length > 1 ? LineEntries[1] : null;
        public string Value => LineEntries?.Length > 2 ? LineEntries[2] : null;
        public double ParsedValue { get; set; }

        public bool IsValid { get; }
    }
}