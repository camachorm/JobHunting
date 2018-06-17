using NUnit.Framework;
using TenorReporting.Models;

namespace TenorReporting.Tests.Models
{
    [TestFixture]
    class TenorModelTests
    {
        [Test]
        public void TestValidTenor()
        {
            var model = new TenorModel("1y1d1m");
            Assert.IsTrue(model.IsValid);
            Assert.AreEqual(365 + 1 + 30, model.DurationInDays);

            model = new TenorModel("10y10d10m");
            Assert.IsTrue(model.IsValid);
            Assert.AreEqual(3650 + 10 + 300, model.DurationInDays);
        }

        [Test]
        public void TestInvalidTenor()
        {
            var model = new TenorModel("y1d1m");
            Assert.IsFalse(model.IsValid);

            model = new TenorModel("10yd10m");
            Assert.IsFalse(model.IsValid);

            model = new TenorModel("10y10dm");
            Assert.IsFalse(model.IsValid);

            model = new TenorModel("10a10d");
            Assert.IsFalse(model.IsValid);

        }
    }
}