using System;
using System.Globalization;
using System.Linq;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Services.Portable;
using Services.Portable.DTO.Api;
using Weather.AndroidApp.Interfaces;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Fragments
{
    public class CityWeatherCardFragment : Fragment, IUpdateFragment<CityWeatherResult>
    {
        private TextView _city;
        private TextView _cityDescr;
        private TextView _textViewTemperature;
        private ImageView _imageViewCurrentWeather;
		
		private TextView _textViewWindy;
		private TextView _textViewHumidity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(R.Layout.city_weather_fragment, null);

            _city = view.FindViewById<TextView>(R.Id.textViewCityName);
            _cityDescr = view.FindViewById<TextView>(R.Id.textViewDateTimeDetails);
            _textViewTemperature = view.FindViewById<TextView>(R.Id.textViewTemperature);
            _imageViewCurrentWeather = view.FindViewById<ImageView>(R.Id.imageViewCurrentWeather);
            _textViewWindy = view.FindViewById<TextView>(R.Id.textViewWindy);
            _textViewHumidity = view.FindViewById<TextView>(R.Id.textViewHumidity);

            return view;
        }

        private string GetPackageName()
        {
            return Activity.PackageName;
        }

        public void Update(CityWeatherResult data)
        {
            var weatherStatus = data.Weather.First();

            _city.Text = data.Name;
            var date = DateTime.Now.ToString("ddd, t", CultureInfo.InvariantCulture);
            _cityDescr.Text = $"{date}, {weatherStatus.Description}";
            _textViewTemperature.Text = $"{data.Main.Temp.DisplayTemperature()}";

            var drawableId = Resources.GetWeatherIconResourceId(weatherStatus, GetPackageName());
            _imageViewCurrentWeather.SetImageResource(drawableId);

            _textViewHumidity.Text = data.Main.Humidity.DisplayHumidity();
            _textViewWindy.Text = data.Wind.Speed.DisplayWindy();
        }

        public void Update(object data)
        {
            Update(data as CityWeatherResult);
        }
    }
}