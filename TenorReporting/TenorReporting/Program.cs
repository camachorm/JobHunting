using System;
using System.Configuration;
using System.IO;
using System.Linq;
using TenorReporting.Extensions;
using TenorReporting.Models;

namespace TenorReporting
{
    internal class Program
    {
        static void Main()
        {
            var startTime = DateTime.Now;
            try
            {
                // Assuming that for brevity, the source file can be specified through the config file, rather than taking human input
                var inputFile = ConfigurationManager.AppSettings["InputFilePath"];
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine(
                        $"Specified Input File not found '{inputFile}', please edit App.Config and set InputFilePath to a valid file location and try again.");
                }

                var models = ReportModel.LoadReportModelsFromDataFile(inputFile).ToList();

                WriteReport(models.OrderBy(i => i.TenorWeight).ThenBy(i => i.PortfolioID), "3");

                WriteReport(models.OrderBy(i => i.PortfolioID).ThenBy(i => i.TenorWeight), "4");
            }
            catch (Exception e)
            {
                e.TraceException();
            }
            finally
            {
                Console.WriteLine($"Execution concluded in {DateTime.Now.Subtract(startTime).TotalMilliseconds}ms.");
            }
        }

        private static void WriteReport(IOrderedEnumerable<ReportModel> report4, string reportFileNameWithoutExtension)
        {
            var reportResult = report4.Aggregate("tenor, portfolioid, value\r\n", (current, model) => current + model.ToString());
            
            // Assuming that for brevity, the output file path can be specified through the config file, rather than taking human input
            var outputFile = Path.Combine(ConfigurationManager.AppSettings["OutputFilePath"] ?? ".\\", String.Concat(reportFileNameWithoutExtension, ".txt"));

            File.WriteAllText(outputFile, reportResult);
        }
    }
}
