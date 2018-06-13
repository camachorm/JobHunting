using System.Linq;
using Newtonsoft.Json.Linq;

namespace GES.MW.GW.Web.Api.Models
{
    public class ForecastGroupModel
    {
        private readonly CityModel _city;
        private readonly JArray _forecasts;

        public ForecastGroupModel(CityModel city, JArray forecasts)
        {
            _city = city;
            _forecasts = forecasts;
        }

        public int CityId => _city.Id;

        public string CityName => _city.Name;

        public string CityCountry => _city.Country;

        public float CityLatitude => _city.Latitude;

        public float CityLongitude => _city.Longitude;

        public ForecastModel ForecastDay1 => new ForecastModel((JObject) _forecasts.ElementAt(1));

        public ForecastModel ForecastDay2 => new ForecastModel((JObject)_forecasts.ElementAt(2));

        public ForecastModel ForecastDay3 => new ForecastModel((JObject)_forecasts.ElementAt(3));

        public ForecastModel ForecastDay4 => new ForecastModel((JObject)_forecasts.ElementAt(4));

        public ForecastModel ForecastDay5 => new ForecastModel((JObject)_forecasts.ElementAt(5));
    }
}