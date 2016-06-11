namespace WeatherModule
{
    internal static class Extensions
    {
        internal static decimal NormalizeTemperature(this decimal tempValue)
        {
            return tempValue - 273.15m;
        }
    }
}