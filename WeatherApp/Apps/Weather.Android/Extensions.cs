using Android.Content.Res;
using Android.Runtime;
using Java.Lang;

namespace Weather.Android
{
    internal static class Extensions
    {
        public static string GetWeatherIconName(this Services.Portable.DTO.Api.WeatherDetails.Weather weatherStatus)
        {
            var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";
            return $"WeatherStatus{weatherStatus.Id}{dayOrNight}";
        }

        public static int GetWeatherIconResourceId(this Resources resources, Services.Portable.DTO.Api.WeatherDetails.Weather weatherStatus, string packageName)
        {
            var iconName = weatherStatus.GetWeatherIconName();
            var iconId = resources.GetIdentifier(iconName.ToLower(), "drawable", packageName);
            return iconId;
        }

        public static ICharSequence ToCharSequence(this string str)
        {
            return CharSequence.ArrayFromStringArray(new[] { str })[0];
        }
    }
}