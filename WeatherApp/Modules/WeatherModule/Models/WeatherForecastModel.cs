using System;

namespace WeatherModule.Models
{
    public class WeatherForecastModel
    {
        public string Day{ get; set; }
        public decimal TempAvg { get; set; }
        public decimal TempMin { get; set; }
        public decimal TempMax { get; set; }
        public decimal Wind { get; set; }
        public string WeatherDescr { get; set; }
        public Uri IconUri { get; set; }
    }
}