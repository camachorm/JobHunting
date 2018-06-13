#region Usings

using System;
using System.Linq;
using GES.MW.GW.Web.Api.Data.Services;
using NUnit.Framework;

#endregion

namespace GES.MW.GW.Web.Api.Tests.Data.Services
{
    [TestFixture]
    public class ForecastServiceTest
    {
        private const int LondonId = 2643743;

        [Test]
        public void GetCityIdsTest()
        {
            var service = new ForecastService();
            var result = service.GetCityIds().Result.ToList();
            Assert.IsNotNull(result, "result != null");
            Assert.IsTrue(result.Any(), "result.Any()");
            Assert.IsTrue(result.Any(i => i.Name.Equals("london", StringComparison.InvariantCultureIgnoreCase) && i.Id == LondonId), "result.Any()");
        }

        [Test]
        public void GetForecastTest()
        {
            // TODO: IRL I would use mocking to bypass the communication to the server on the unit tests, and use integrations tests to validate the end to end, but due to the time limitations I will skip mocking in this code
            var service = new ForecastService();
            var result = service.GetForecast(LondonId).Result;
            Assert.IsNotNull(result, "result != null");
            Assert.IsNotNull(result.ForecastDay1, "result != null");
            Assert.IsNotNull(result.ForecastDay2, "result.ForecastDay2 != null");
            Assert.IsNotNull(result.ForecastDay3, "result.ForecastDay3 != null");
            Assert.IsNotNull(result.ForecastDay4, "result.ForecastDay4 != null");
            Assert.IsNotNull(result.ForecastDay5, "result.ForecastDay5 != null");
            // Avoiding strong typing every single validation, reflection is a nifty tool to use in cirurgical spots
            Assert.DoesNotThrow(() => ReadAllProperties(result, result.ForecastDay1, result.ForecastDay2, result.ForecastDay3, result.ForecastDay4, result.ForecastDay5));
        }

        // Wea have to use polymorphism to make the single line call perfectly readable
        private void ReadAllProperties(params object[] models)
        {
            foreach (var model in models)
            {
                ReadAllProperties(model);
            }
        }

        private void ReadAllProperties(object model)
        {
            foreach (var property in model.GetType().GetProperties())
            {
                property.GetValue(model);
            }
        }
    }
}