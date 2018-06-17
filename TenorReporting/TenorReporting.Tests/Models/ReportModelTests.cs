using System.IO;
using System.Linq;
using NUnit.Framework;
using TenorReporting.Models;

namespace TenorReporting.Tests.Models
{
    [TestFixture]
    internal class ReportModelTests
    {
        const string TestFilesPath = @"D:\git\JobHunting\TenorReporting\TenorReporting.Tests";
        [Test]
        public void LoadReportModelsFromDataFileFailsTests()
        {
            Assert.Throws<FileNotFoundException>(() =>
                ReportModel.LoadReportModelsFromDataFile(@"C:\fakepath\fakefile.fakeextension"));
            Assert.Throws<FileNotFoundException>(() =>
                ReportModel.LoadReportModelsFromDataFile(null));
        }

        [Test]
        public void LoadReportModelsFromDataFileSucceedsTests()
        {
            var result = ReportModel.LoadReportModelsFromDataFile(Path.Combine(TestFilesPath, "File1.txt")).ToList();
            Assert.IsNotNull(result, "result != null");
            Assert.IsNotEmpty(result, "result is empty");
            Assert.AreEqual(8, result.Count, "result.Count != 8");

            result = ReportModel.LoadReportModelsFromDataFile(Path.Combine(TestFilesPath, "File2.txt")).ToList();
            Assert.IsNotNull(result, "result != null");
            Assert.IsNotEmpty(result, "result is empty");
            Assert.AreEqual(8, result.Count, "result.Count != 8");

            result = ReportModel.LoadReportModelsFromDataFile(Path.Combine(TestFilesPath, "File3.txt")).ToList();
            Assert.IsNotNull(result, "result != null");
            Assert.IsNotEmpty(result, "result is empty");
            Assert.AreEqual(8, result.Count, "result.Count != 8");

            result = ReportModel.LoadReportModelsFromDataFile(Path.Combine(TestFilesPath, "File4.txt")).ToList();
            Assert.IsNotNull(result, "result != null");
            Assert.IsNotEmpty(result, "result is empty");
            Assert.AreEqual(8, result.Count, "result.Count != 8");
        }
    }
}