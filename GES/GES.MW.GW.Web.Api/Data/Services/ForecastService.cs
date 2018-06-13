using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GES.MW.GW.Web.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GES.MW.GW.Web.Api.Data.Services
{
    public class ForecastService : IForecastService
    {
        static readonly List<CityModel> Result = new List<CityModel>();

        private const string ApiEndpoint = "http://api.openweathermap.org/data/2.5/forecast?id={0}&appid={1}";


        public async Task<IEnumerable<CityModel>> GetCityIds()
        {
            if (Result.Any()) return Result;


            using (var file = File.OpenText(ConfigurationManager.AppSettings["JsonFilePath"]))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var cities = (JArray)await JToken.ReadFromAsync(reader);
                    foreach (var jToken in cities)
                    {
                        var city = (JObject)jToken;
                        Result.Add(new CityModel
                        {
                            Country = city.GetValue("country").Value<string>(),
                            Id = city.GetValue("id").Value<int>(),
                            Name = city.GetValue("name").Value<string>(),
                            Latitude = ((JObject)city.GetValue("coord")).GetValue("lat").Value<float>(),
                            Longitude = ((JObject)city.GetValue("coord")).GetValue("lon").Value<float>()
                        });
                    }
                }
            }

            return Result;
        }

        public async Task<ForecastGroupModel> GetForecast(int cityId)
        {
            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(string.Format(ApiEndpoint, cityId, ConfigurationManager.AppSettings["ApiId"]));

                httpResponse.EnsureSuccessStatusCode();

                var jsonStringResponse = await httpResponse.Content.ReadAsStringAsync();

                var jsonObject = (JObject) await JToken.ReadFromAsync(new JsonTextReader(new StringReader(jsonStringResponse)));

                var city = (await GetCityIds()).Single(i => i.Id == cityId);

                return new ForecastGroupModel(city, (JArray) jsonObject.GetValue("list"));
            }
        }
    }
}