#region Usings

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GES.MW.GW.Web.Api.Data.Services;
using GES.MW.GW.Web.Api.Models;

#endregion

namespace GES.MW.GW.Web.Api.Controllers
{
    /// <summary>
    ///     IRL this single controller would be segregated in two CitiesController and WeatherController, but for brevity it
    ///     was merged in this sample
    /// </summary>
    [AllowAnonymous]
    public class WeatherController : ApiController
    {
        private readonly IForecastService _forecastService;

        public WeatherController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [Route("api/GetCityIds")]
        public async Task<IEnumerable<CityModel>> GetCityIds()
        {
            return await _forecastService.GetCityIds();
        }

        [Route("api/GetForecast")]
        public async Task<ForecastGroupModel> GetForecast(int cityId)
        {
            return await _forecastService.GetForecast(cityId);
        }
    }
}