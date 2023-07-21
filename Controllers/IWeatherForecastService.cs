using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int value, int minTemperature, int maxTemperature);
    }
}