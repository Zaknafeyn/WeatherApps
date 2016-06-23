using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
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
    public partial class MainActivity : Activity, ILocationListener
    {
        int count = 1;

        private Button _buttonShowWeather;
        private EditText _editTextCity;
        private ImageView _imageViewCurrentWeather;
        private TextView _textViewCity;
        private TextView _textViewCurrentTemp;
        private ProgressBar _progressBar;
        private LinearLayout _linearLayoutWeather;
        private TextView _textViewDescription;
        private TextView _textViewTempRange;
        private HorizontalScrollView _horizontalScrollHourlyForecast;
        private LinearLayout _linearLayoutHourlyForecast;


        private readonly WeatherApi _weatherApi = new WeatherApi();

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InitializeLocationManager();

            ShowDiagInfo($"Location providers: {string.Join(", ", _locationProvider)}");

            //_locationManager.RequestLocationUpdates(_locationProvider, 0,0, this);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _editTextCity = FindViewById<EditText>(Resource.Id.editTextCity);
            _buttonShowWeather = FindViewById<Button>(Resource.Id.MyButton);

            _buttonShowWeather.Click += _buttonShowWeather_Click;

            _linearLayoutWeather = FindViewById<LinearLayout>(Resource.Id.linearLayoutWeather);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarLoading);
            _imageViewCurrentWeather = FindViewById<ImageView>(Resource.Id.imageViewCurrentWeather);
            _textViewCity = FindViewById<TextView>(Resource.Id.textViewCity);
            _textViewCurrentTemp = FindViewById<TextView>(Resource.Id.textViewCurrentTemp);
            _textViewDescription = FindViewById<TextView>(Resource.Id.textViewDescription);
	        _textViewTempRange = FindViewById<TextView>(Resource.Id.textViewTempRange);
            _horizontalScrollHourlyForecast =
                FindViewById<HorizontalScrollView>(Resource.Id.horizontalScrollHourlyForecast);
            _linearLayoutHourlyForecast = FindViewById<LinearLayout>(Resource.Id.linearLayoutHourlyForecast);

            //await DisplayWeatherAsync("Athens");

            var coords = new Coordinates
            {
                Longtitude = 50.4601m,
                Latitude = -30.5148m
            };

            //var coords = await GetCurrentCoords();

            await DisplayWeatherAsync(coords);

            //_locationManager.RemoveUpdates(this);
        }

        private void ShowDiagInfo(string message)
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Diagnostics");
            dialog.SetMessage(message);
            RunOnUiThread(() => { dialog.Show(); });
        }

        private void ToggleLoadingState(bool isLoading)
        {
            _buttonShowWeather.Enabled = !isLoading;

            //_imageViewCurrentWeather.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;
            //_textViewCurrentTemp.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;
            _linearLayoutWeather.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;

            _progressBar.Visibility = isLoading ? ViewStates.Visible : ViewStates.Gone;
        }

        async Task DisplayWeatherAsync(Coordinates coords)
        {
            ToggleLoadingState(true);

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
                ToggleLoadingState(false);
            }
        }

        async Task DisplayWeatherAsync(string city)
        {
            ToggleLoadingState(true);

            try
            {
                await ShowWeatherAsync(city);
            }
            finally
            {
                ToggleLoadingState(false);
            }
        }

        async Task ShowWeatherAsync(string city)
        {

            var cityWeatherTask = _weatherApi.GetWeatherByCityNameAsync(city);
            var cityForecastWeatherTask = _weatherApi.GetWeatherForecastByCityNameAsync(city);

            await Task.WhenAll(cityWeatherTask, cityForecastWeatherTask);

            var cityWeather = cityWeatherTask.Result;
            var cityForecastWeather = cityForecastWeatherTask.Result;

            ShowWeather(cityWeather);
            ShowForecast(cityForecastWeather);
        }

        async Task ShowWeatherAsync(Coordinates coords)
        {
            var cityWeatherTask =  _weatherApi.GetWeatherByCoordAsync(coords);
            var cityForecastWeatherTask = _weatherApi.GetWeatherForecastByCoordsAsync(coords);

            await Task.WhenAll(cityWeatherTask, cityForecastWeatherTask);

            var cityWeather = cityWeatherTask.Result;
            var cityForecastWeather = cityForecastWeatherTask.Result;

            ShowWeather(cityWeather);
            ShowForecast(cityForecastWeather);
        }

        void ShowForecast(CityWeatherForecastResult cityForecastWeather)
        {
            _linearLayoutHourlyForecast.RemoveAllViews();
            foreach (var weatherForecastItem in cityForecastWeather.HourlyForecast)
            {
                var panel = new HourlyForecastPanel(this);
                panel.SetWeather(weatherForecastItem);
                _linearLayoutHourlyForecast.AddView(panel);
            }
        }

        void ShowWeather(CityWeatherResult cityWeather)
        {
            var weatherStatus = cityWeather.Weather.First();

            _textViewCurrentTemp.Text = $"{cityWeather.Main.Temp.NormalizeTemperature()}º";
            _textViewCity.Text = $"{cityWeather.Name}";
            var drawableId = Resources.GetWeatherIconResourceId(weatherStatus, PackageName);
            _imageViewCurrentWeather.SetImageResource(drawableId);
            _textViewDescription.Text = weatherStatus.Description;

	        var isMinMaxTempEqual = cityWeather.Main.TempMin == cityWeather.Main.TempMax;

	        _textViewTempRange.Visibility = isMinMaxTempEqual ? ViewStates.Gone : ViewStates.Visible;
			_textViewTempRange.Text =
		        $"{cityWeather.Main.TempMin.NormalizeTemperature()}º .. {cityWeather.Main.TempMax.NormalizeTemperature()}º";
        }

        private async void _buttonShowWeather_Click(object sender, EventArgs e)
        {
            await ShowWeatherAsync(_editTextCity.Text);
        }
    }
}

