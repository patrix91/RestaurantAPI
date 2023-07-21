using System.Collections.Generic;
using RestaurantAPI.Controllers;
using System.Linq;
using System;

namespace RestaurantAPI
{
    public class WeatherForecastService: IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> Get(int value, int minTemperature, int maxTemperature)
        {
            var rng = new Random();
            return Enumerable.Range(1, value).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(minTemperature, maxTemperature),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}