using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TenorReporting.Models
{
    public class ReportModel
    {
        private ReportModel(UnparsedLineModel model)
        {
            Tenor = model.ParsedTenor;
            PortfolioID = model.PortfolioID;
            Value = model.ParsedValue;
        }

        public TenorModel Tenor { get; set; }
        public int TenorWeight => Tenor.DurationInDays;
        public string PortfolioID { get; set; }
        public double Value { get; set; }

        public static IEnumerable<ReportModel> LoadReportModelsFromDataFile(string fileFullPathAndName)
        {
            var result = new List<ReportModel>();

            if (!File.Exists(fileFullPathAndName)) throw new FileNotFoundException($"Requested file could not be found. {fileFullPathAndName}");

            result.AddRange(File.ReadLines(fileFullPathAndName).Select(line => new UnparsedLineModel(line)).Where(model => model.IsValid).Select(model => new ReportModel(model)));

            return result;
        }

        public override string ToString()
        {
            return Tenor.Tenor + ',' + PortfolioID + ',' + Value + Environment.NewLine;
        }
    }
}
