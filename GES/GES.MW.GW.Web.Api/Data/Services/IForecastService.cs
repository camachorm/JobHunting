using System.Collections.Generic;
using System.Threading.Tasks;
using GES.MW.GW.Web.Api.Models;

namespace GES.MW.GW.Web.Api.Data.Services
{
    public interface IForecastService
    {
        Task<IEnumerable<CityModel>> GetCityIds();
        Task<ForecastGroupModel> GetForecast(int cityId);
    }
}