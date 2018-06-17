using NUnit.Framework;
using TenorReporting.Models;

namespace TenorReporting.Tests.Models
{
    [TestFixture]
    public class UnparsedLineModelTests
    {
        [Test]
        public void TestValidLines()
        {
            var model = new UnparsedLineModel("9m, 10, 30");
            Assert.IsTrue(model.IsValid, "model.IsValid");
            Assert.AreEqual(model.Tenor, "9m", "model.Tenor != 9m");
            Assert.AreEqual(model.PortfolioID, "10", "model.PortfolioID != 10");
            Assert.AreEqual(model.ParsedValue, (double)30, "model.ParsedValue != 30");

            model = new UnparsedLineModel("1d3m, 10, 20");
            Assert.IsTrue(model.IsValid, "model.IsValid");
            Assert.AreEqual(model.Tenor, "1d3m", "model.Tenor != 1d3m");
            Assert.AreEqual(model.PortfolioID, "10", "model.PortfolioID != 10");
            Assert.AreEqual(model.ParsedValue, (double)20, "model.ParsedValue != 20");

            model = new UnparsedLineModel("1d3m, 11, 120");
            Assert.IsTrue(model.IsValid, "model.IsValid");
            Assert.AreEqual(model.Tenor, "1d3m", "model.Tenor != 1d3m");
            Assert.AreEqual(model.PortfolioID, "11", "model.PortfolioID != 11");
            Assert.AreEqual(model.ParsedValue, (double)120, "model.ParsedValue != 120");
        }

        [Test]
        public void TestInvalidLines()
        {
            var model = new UnparsedLineModel("");
            Assert.IsFalse(model.IsValid, "model.IsValid");

            model = new UnparsedLineModel("xx, 11, 20");
            Assert.IsFalse(model.IsValid, "model.IsValid");

            model = new UnparsedLineModel("9m, , 20");
            Assert.IsFalse(model.IsValid, "model.IsValid");

            model = new UnparsedLineModel("9m, 11, ab");
            Assert.IsFalse(model.IsValid, "model.IsValid");

            model = new UnparsedLineModel("9m, 11, ");
            Assert.IsFalse(model.IsValid, "model.IsValid");

            model = new UnparsedLineModel(", 11, ab");
            Assert.IsFalse(model.IsValid, "model.IsValid");
        }
    }
}
