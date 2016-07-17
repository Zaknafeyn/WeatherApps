using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;
using Services.Portable.DTO.Api.WeatherDetails;
using Services.Portable.DTO.Api.WeatherForecast;

namespace Services.Portable.Service
{
    public class WeatherServiceMock : IWeatherService
    {
        public async Task<CityWeatherResult> GetWeatherByCityNameAsync(string cityName)
        {
            return GetWeather();
        }

        public async Task<CityWeatherResult> GetWeatherByCityIdAsync(int cityId)
        {
            return GetWeather();
        }

        public async Task<CityWeatherResult> GetWeatherByCoordsAsync(Coordinates coords)
        {
            return GetWeather();
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityNameAsync(string cityName)
        {
            return GetForecast();
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityIdAsync(int cityId)
        {
            return GetForecast();
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCoordsAsync(Coordinates coords)
        {
            return GetForecast();
        }

        private CityWeatherResult GetWeather()
        {
            return new CityWeatherResult
            {
                Id = 10,
                Main = new Main
                {
                    Temp = 21,
                    Humidity = 30,
                    Pressure = 1000,
                    TempMax = 22,
                    TempMin = 20
                },
                Weather = new List<Weather>
                {
                    new Weather
                    {
                        Id = 800,
                        Main = "Weather",
                        Description = "Rainy",
                        Icon = "800d"
                    }
                },
                Name = "Weather Name",
                Dt = 12,
                Base = "WeatherBase",
                Clouds = new Clouds
                {
                    All = 30
                },
                Cod = 15,
                Coord = new Coord { Lat = 10m, Lon = 15m },
                Country = "UK",
                Sys = new Sys
                {
                    Id = 12,
                    Message = 12,
                    Country = "UK",
                    Sunrise = 5,
                    Sunset = 7,
                    Type = 2
                },
                Wind = new Wind
                {
                    Deg = 12,
                    Speed = 5
                }
            };
        }

        private CityWeatherForecastResult GetForecast()
        {
            List<WeatherForecastItem> forecast = new List<WeatherForecastItem>();
            var rand = new Random(DateTime.Now.Second);
            for (int i = 0; i < 40; i++)
            {
                forecast.Add(GetForecastItem(rand.Next(10, 35), (i + 1) * 3));
            }

            return new CityWeatherForecastResult
            {
                HourlyForecast = new List<WeatherForecastItem>
                {
                    new WeatherForecastItem
                    {

                    }
                },
                Message = 12,
                City = new City
                {
                    Id = 5,
                    Name = "Kiev",
                    Coord = new Coord
                    {
                        Lon = 12,
                        Lat = 15
                    },
                    Sys = new Sys
                    {
                        Id = 12,
                        Message = 12,
                        Country = "UA",
                        Sunset = 5,
                        Sunrise = 21,
                        Type = 4
                    },
                    Country = "UA",
                    Population = 34000000
                },
                Cod = "Cod",
                Cnt = 7
            };
        }

        private WeatherForecastItem GetForecastItem(decimal temp, int dt)
        {
            var rand = new Random(DateTime.Now.Second);

            var timeShift = DateTime.Now.AddHours(dt);
            var dayOrNight = timeShift.Hour > 5 && timeShift.Hour < 21 ? "d" : "n";

            int weatherId = 0;
            while ((weatherId = rand.Next(2, 9)) != 4){}

            return new WeatherForecastItem
            {
                Main = new Main
                {
                    Temp = temp,
                    TempMin = temp - 1,
                    TempMax = temp + 1,
                    Humidity = 56,
                    Pressure = 12,
                },
                Sys = new Sys
                {
                    Id = 1,
                    Country = "UA",
                    Message = 2,
                    Sunset = 12,
                    Sunrise = 23,
                    Type = 2
                },
                Weather = new List<Weather>
                {
                    new Weather
                    {
                        Id = weatherId,
                        Main = "ForecastMain",
                        Icon = "${weatherId}{dayOrNight }",
                        Description = "Sunny"
                    }
                },
                Wind = new Wind
                {
                    Speed = 12,
                    Deg = 6
                },
                Clouds = new Clouds
                {
                    All = 2
                },
                Dt = timeShift,
                DtTxt = $"{dt}",
                Rain = new Rain
                {
                    LastThreeHoursLevel = 2
                }
            };
        }
    }
}