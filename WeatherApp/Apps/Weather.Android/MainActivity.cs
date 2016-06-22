using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;

namespace Weather.Android
{
    [Activity(Label = "WeatherApp", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity : Activity//, ILocationListener
    {
        private readonly WeatherApi _weatherApi = new WeatherApi();

        private EditText _editTextCity;
        private Button _buttonShowWeather;

        TextView _addressText;
        Location _currentLocation;
        LocationManager _locationManager;

        private string _locationProvider;
        private TextView _locationText;
        private TextView _currentTempIndicator;
        private ImageView _imageViewCurrrentWeather;
        private TextView _textViewCityIndicator;


        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            return;

            //_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

            _editTextCity = FindViewById<EditText>(Resource.Id.editTextCity);
            _editTextCity.TextChanged += _editTextCity_TextChanged;

            _buttonShowWeather = FindViewById<Button>(Resource.Id.MyButton);
            _currentTempIndicator = FindViewById<TextView>(Resource.Id.textViewCurrentTemp);
            _imageViewCurrrentWeather = FindViewById<ImageView>(Resource.Id.imageViewCurrentWeather);
            _textViewCityIndicator = FindViewById<TextView>(Resource.Id.textViewCity);


            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarLoading);
            
            // update controls visiblity state
            _imageViewCurrrentWeather.Visibility = ViewStates.Gone;
            _currentTempIndicator.Visibility = ViewStates.Gone;
            progressBar.Visibility = ViewStates.Visible;

            try
            {
                var coords = await GetCurrentCoords();

                await ShowWeather(coords);

                //var cityWeather = await _weatherApi.GetWeatherByCoordAsync(coords);

                //var weatherStatus = cityWeather.Weather.First();

                //_currentTempIndicator.Text = $"{cityWeather.Main.Temp.NormalizeTemperature()}º";
                //_textViewCityIndicator.Text = $"{cityWeather.Name}";
                //var drawableId = Resources.GetIdentifier(weatherStatus.GetWeatherIconName().ToLower(), "drawable", PackageName);
                //_imageViewCurrrentWeather.SetImageResource(drawableId);
            }
            finally
            {
                // update controls visiblity state
                _imageViewCurrrentWeather.Visibility = ViewStates.Visible;
                _currentTempIndicator.Visibility = ViewStates.Visible;

                progressBar.Visibility = ViewStates.Gone;
            }

            // Get our button from the layout resource,
            // and attach an event to it

            var button = FindViewById<Button>(Resource.Id.MyButton);


            button.Click += Button_Click;

            //_locationManager.RemoveUpdates(this);
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            await ShowWeather(_editTextCity.Text);
        }

        async Task ShowWeather(string city)
        {
            var cityWeather = await _weatherApi.GetWeatherByCityNameAsync(city);
            await ShowWeather(cityWeather);
        }

        async Task ShowWeather(Coordinates coords)
        {
            var cityWeather = await _weatherApi.GetWeatherByCoordAsync(coords);
            await ShowWeather(cityWeather);
        }

        async Task ShowWeather(CityWeatherResult cityWeather)
        {
            var weatherStatus = cityWeather.Weather.First();

            _currentTempIndicator.Text = $"{cityWeather.Main.Temp.NormalizeTemperature()}º";
            _textViewCityIndicator.Text = $"{cityWeather.Name}";
            var drawableId = Resources.GetIdentifier(weatherStatus.GetWeatherIconName().ToLower(), "drawable", PackageName);
            _imageViewCurrrentWeather.SetImageResource(drawableId);
        }

        private void _editTextCity_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            _buttonShowWeather.Enabled = e.AfterCount != 0;
        }
    }
}

