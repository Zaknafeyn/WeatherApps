using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Converters;
using Services.DTO.Api.WeatherDetails;

namespace Services.DTO.Api.WeatherForecast
{
    public class WeatherForecastItem
    {
        [JsonProperty("dt")]
        [JsonConverter(typeof(UnixDateJsonConverter))]
        public DateTime Dt { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public IList<Weather> Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }
    }
}