using Newtonsoft.Json.Linq;

namespace GES.MW.GW.Web.Api.Models
{
    public class ForecastModel
    {
        private readonly JObject _forecast;

        public ForecastModel(JObject forecast)
        {
            _forecast = forecast;
        }

        public int Date => _forecast.GetValue("dt").Value<int>();
        public string DateText => _forecast.GetValue("dt_txt").Value<string>();
        public double Temperature => ((JObject)_forecast.GetValue("main")).GetValue("temp").Value<double>();
        public double MinTemperature  => ((JObject)_forecast.GetValue("main")).GetValue("temp_min").Value<double>();
        public double MaxTemperature => ((JObject)_forecast.GetValue("main")).GetValue("temp_max").Value<double>();
        public double Pressure  => ((JObject)_forecast.GetValue("main")).GetValue("pressure").Value<double>();
        public double SeaLevel => ((JObject)_forecast.GetValue("main")).GetValue("sea_level").Value<double>();
        public double GroundLevel => ((JObject)_forecast.GetValue("main")).GetValue("grnd_level").Value<double>();
        public int Humidity => ((JObject)_forecast.GetValue("main")).GetValue("humidity").Value<int>();
        public double TemperatureKf => ((JObject)_forecast.GetValue("main")).GetValue("temp_kf").Value<double>();
        public int WeatherId => ((JObject)((JArray)_forecast.GetValue("weather"))[0]).GetValue("id").Value<int>();
        public string WeatherMain => ((JObject)((JArray)_forecast.GetValue("weather"))[0]).GetValue("main").Value<string>();
        public string WeatherDescription => ((JObject)((JArray)_forecast.GetValue("weather"))[0]).GetValue("description").Value<string>();
        public string WeatherIcon => ((JObject)((JArray)_forecast.GetValue("weather"))[0]).GetValue("icon").Value<string>();
        public int CloudsAll => ((JObject)_forecast.GetValue("clouds")).GetValue("all").Value<int>();
        public double WindSpeed => ((JObject)_forecast.GetValue("wind")).GetValue("speed").Value<double>();
        public double WindDegrees => ((JObject)_forecast.GetValue("wind")).GetValue("deg").Value<double>();
        public string SysPod => ((JObject)_forecast.GetValue("sys")).GetValue("pod").Value<string>();
    }
}