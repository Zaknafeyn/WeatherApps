using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Java.Net;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;

namespace Weather.Android
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity : Activity, ILocationListener
    {
        private readonly WeatherApi _weatherApi = new WeatherApi();
        private const string HockeyAppId = "d852457aea42476bb9a9377774b314e5";

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CrashManager.Register(this);
            MetricsManager.Register(this, Application);

            HockeyApp.MetricsManager.TrackEvent("Application is initializing...");

            InitializeLocationManager();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            //ShowDiagInfo($"Location providers: {string.Join(", ", _locationProvider)}");

            //_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

            InitializeComponents();

            HockeyApp.MetricsManager.TrackEvent("Application is initialized");

            _buttonShowWeather.Click += _buttonShowWeather_Click;

            await DisplayWeatherAsync("Kiev");

            ShowImg();


            //var coords = new Coordinates
            //{
            //    Longtitude = 50.4601m,
            //    Latitude = -30.5148m
            //};

            ////var coords = await GetCurrentCoords();

            //await DisplayWeatherAsync(coords);

            ////_locationManager.RemoveUpdates(this);
        }

        private void ShowImg()
        {
            //var restClient = new RestClient();
            //try
            //{
            //    var result = await restClient.GetAsync(
            //    new Uri(
            //        "http://thecatapi.com/api/images/get?api_key=OTcwMTU&size=med&type=png&format=src&results_per_page=1"));
            //    ShowDiagInfo(result);
            //}
            //catch (Exception ex)
            //{
            //    ShowDiagInfo(ex.Message);
            //}


            //var src = await restClient.GetAsync(new Uri(result));


            //var kittyUrl = new URL("http://24.media.tumblr.com/tumblr_m2wxvgkow61r73wdao1_500.png");
            //var bmp = BitmapFactory.DecodeStream(kittyUrl.OpenConnection().InputStream);

            var imageViewKitty = FindViewById<ImageView>(Resource.Id.imageViewKitty);
            imageViewKitty.Visibility = ViewStates.Gone;

            //imageViewKitty.SetImageBitmap(bmp);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //ShowDiagInfo(item.TitleFormatted.ToString());

            switch (item.ItemId)
            {
                case Resource.Id.menuItemAbout:
                    StartActivity(typeof(AboutActivity));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowDiagInfo(string message)
        {
#if DEBUG
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Diagnostics");
            dialog.SetMessage(message);
            RunOnUiThread(() => { dialog.Show(); });
#endif
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
            foreach (var weatherForecastItem in cityForecastWeather.HourlyForecast.Take(8))
            {
                var panel = new HourlyForecastPanel(this);
                panel.SetWeather(weatherForecastItem);
                _linearLayoutHourlyForecast.AddView(panel);
            }
        }

        void ShowWeather(CityWeatherResult cityWeather)
        {
            var weatherStatus = cityWeather.Weather.First();

            _textViewCurrentTemp.Text = $"{cityWeather.Main.Temp.DisplayTemperature()}";
            _textViewCity.Text = $"{cityWeather.Name}";
            var drawableId = Resources.GetWeatherIconResourceId(weatherStatus, PackageName);
            _imageViewCurrentWeather.SetImageResource(drawableId);
            _textViewDescription.Text = weatherStatus.Description;

	        var isMinMaxTempEqual = cityWeather.Main.TempMin == cityWeather.Main.TempMax;

	        _textViewTempRange.Visibility = isMinMaxTempEqual ? ViewStates.Gone : ViewStates.Visible;
			_textViewTempRange.Text =
		        $"{cityWeather.Main.TempMin.DisplayTemperature()} .. {cityWeather.Main.TempMax.DisplayTemperature()}";
        }

        private async void _buttonShowWeather_Click(object sender, EventArgs e)
        {
            await ShowWeatherAsync(_editTextCity.Text);
        }
    }
}

