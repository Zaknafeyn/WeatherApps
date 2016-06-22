namespace Weather.Android
{
    internal static class Extensions
    {
        public static string GetWeatherIconName(this Services.Portable.DTO.Api.WeatherDetails.Weather weatherStatus)
        {
            var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";
            return $"WeatherStatus{weatherStatus.Id}{dayOrNight}";

        }
    }
}