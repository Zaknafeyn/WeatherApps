using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Services.Portable.DTO.Api;
using R = Weather.Android.Resource;

namespace Weather.Android.Fragments
{
    public class CityWeatherCardFragment : Fragment
    {
        private readonly CityWeatherResult _weather;
        private readonly CityWeatherForecastResult _weatherForecast;

        public CityWeatherCardFragment(CityWeatherResult weather, CityWeatherForecastResult weatherForecast)
        {
            _weather = weather;
            _weatherForecast = weatherForecast;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(R.Layout.city_weather_fragment, null);

            var city = view.FindViewById<TextView>(R.Id.textViewCityName);
            city.Text = "Red Hot Chilli Text";

            return view;
        }
    }
}