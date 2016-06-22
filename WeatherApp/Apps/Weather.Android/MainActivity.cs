using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;

namespace Weather.Android
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private Button _buttonShowWeather;
        private EditText _editTextCity;
        private ImageView _imageViewCurrentWeather;
        private TextView _textViewCity;
        private TextView _textViewCurrentTemp;
        private ProgressBar _progressBar;

        private readonly WeatherApi _weatherApi = new WeatherApi();

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _editTextCity = FindViewById<EditText>(Resource.Id.editTextCity);
            _buttonShowWeather = FindViewById<Button>(Resource.Id.MyButton);

            _buttonShowWeather.Click += _buttonShowWeather_Click;

            _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarLoading);
            _imageViewCurrentWeather = FindViewById<ImageView>(Resource.Id.imageViewCurrentWeather);
            _textViewCity = FindViewById<TextView>(Resource.Id.textViewCity);
            _textViewCurrentTemp = FindViewById<TextView>(Resource.Id.textViewCurrentTemp);


            //await DisplayWeatherAsync("Athens");
            await DisplayWeatherAsync(new Coordinates
            {
                Longtitude = 50.4601m,
                Latitude = -30.5148m
            });

        }

        private void SetLoadingState(bool isLoading)
        {
            _buttonShowWeather.Enabled = !isLoading;

            _imageViewCurrentWeather.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;
            _textViewCurrentTemp.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;
            _progressBar.Visibility = isLoading ? ViewStates.Visible : ViewStates.Gone;
        }

        async Task DisplayWeatherAsync(Coordinates coords)
        {
            SetLoadingState(true);

            try
            {
                await ShowWeatherAsync(coords);
            }
            catch (Exception ex)
            {
                _editTextCity.Text = ex.Message;
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        async Task DisplayWeatherAsync(string city)
        {
            SetLoadingState(true);

            try
            {
                await ShowWeatherAsync(city);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        async Task ShowWeatherAsync(string city)
        {
            var cityWeather = await _weatherApi.GetWeatherByCityNameAsync(city);
            ShowWeather(cityWeather);
        }

        async Task ShowWeatherAsync(Coordinates coords)
        {
            var cityWeather = await _weatherApi.GetWeatherByCoordAsync(coords);
            ShowWeather(cityWeather);
        }

        void ShowWeather(CityWeatherResult cityWeather)
        {
            var weatherStatus = cityWeather.Weather.First();

            _textViewCurrentTemp.Text = $"{cityWeather.Main.Temp.NormalizeTemperature()}º";
            _textViewCity.Text = $"{cityWeather.Name}";
            var drawableId = Resources.GetIdentifier(weatherStatus.GetWeatherIconName().ToLower(), "drawable", PackageName);
            _imageViewCurrentWeather.SetImageResource(drawableId);
        }

        private async void _buttonShowWeather_Click(object sender, EventArgs e)
        {
            await ShowWeatherAsync(_editTextCity.Text);
        }
    }
}

