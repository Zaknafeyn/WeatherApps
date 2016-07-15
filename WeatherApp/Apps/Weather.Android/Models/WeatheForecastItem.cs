using System;

namespace Weather.AndroidApp.Models
{
    public class WeatheForecastItem
    {
        public DateTime Day { get; set; }
        public string DateImageName { get; set; }
        public int DateImageResourceId { get; set; }
        public decimal TempDay { get; set; }
        public decimal TempNight { get; set; }
    }
}