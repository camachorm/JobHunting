using System;
using System.Diagnostics;
using System.Globalization;

namespace TenorReporting.Extensions
{
    public static class StringExtensions
    {
        public static string Indent(this string source, char indentChar = '\t', int indentLevel = 0)
        {
            return source
                ?.Replace($"{Environment.NewLine}", $"{Environment.NewLine}{new string(indentChar, indentLevel)}")
                .Insert(0, new string(indentChar, indentLevel));
        }

        public static string ParseAndFormatDateTime(this string source, string languageCode = null)
        {
            Trace.TraceInformation("Try to parse date {0}", source);

            return source.ParseDateTime(languageCode).ToString("yyyy-MM-dd");
        }

        public static DateTime ParseDateTime(this string source, string languageCode = null)
        {
            Trace.TraceInformation("Try to parse date {0}", source);

            DateTime result;
            var parseSuccess = languageCode == null
                ? DateTime.TryParse(source, out result)
                : DateTime.TryParse(source, new CultureInfo(languageCode), DateTimeStyles.None, out result);

            if (!parseSuccess)
                parseSuccess = DateTime.TryParseExact(source,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                parseSuccess = DateTime.TryParseExact(source,
                    "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                parseSuccess = DateTime.TryParseExact(source,
                    "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                parseSuccess = DateTime.TryParseExact(source,
                    "MM/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                parseSuccess = DateTime.TryParseExact(source,
                    "M/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                DateTime.TryParseExact(source,
                    "yyyy/mm/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            if (!parseSuccess)
                throw new FormatException($"DateTime format for '{source}' is unknown.");

            return result;
        }
    }
}