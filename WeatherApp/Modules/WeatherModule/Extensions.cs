using System;

namespace WeatherModule
{
    internal static class Extensions
    {
        internal static decimal NormalizeTemperature(this decimal tempValue)
        {
            return Math.Round(tempValue - 273.15m, 1);
        }
    }
}