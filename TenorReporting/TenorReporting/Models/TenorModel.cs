using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TenorReporting.Models
{
    public class TenorModel
    {
        private readonly double _durationInDays;

        public TenorModel(string tenor)
        {
            Tenor = tenor;

            var entries = Regex.Split(tenor, "([0-9]*[dwmy]{1})").Where(entry => !string.IsNullOrEmpty(entry)).Select(ParseString).ToList();

            if (entries.Any(model => !model.Item1)) return;

            _durationInDays += entries.Sum(entry => entry.Item2);

            IsValid = true;
        }

        public string Tenor { get; }

        /// <summary>
        /// Using a sum of the Tenor time representation for sorting purposes.
        /// </summary>
        public int DurationInDays => (int)_durationInDays;

        public bool IsValid { get; set; }

        private Tuple<bool, int> ParseString(string tenor)
        {
            var tenorScale = tenor.ToCharArray().Last();

            if (!int.TryParse(tenor.Substring(0, tenor.Length - 1), out int multiplier))
                return new Tuple<bool, int>(false, 0);

            var result = true;
            var resultSum = 0;

            switch (tenorScale)
            {
                case 'd':
                    resultSum += multiplier * 1;
                    break;
                case 'w':
                    resultSum += multiplier * 7;
                    break;
                case 'm':
                    // Assuming month as 30 days
                    resultSum += multiplier * 30;
                    break;

                case 'y':
                    // Assuming month as 365 days
                    resultSum += multiplier * 365;
                    break;

                default:
                    result = false;
                    break;
            }
            return new Tuple<bool, int>(result, resultSum);
        }
    }
}
