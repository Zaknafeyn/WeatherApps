using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Microsoft.Practices.ServiceLocation;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;
using Weather.Android.AppServices;

namespace Weather.Android.Activities
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity : FragmentActivity
    {
        private const string MainActivityTag = "Main Activity";
        private ISettings _settings;
        private readonly WeatherApi _weatherApi = new WeatherApi();

        public MainActivity()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Application.Context).As<Context>();

            builder.RegisterType<StorageService>().SingleInstance().As<IStorageService>();

            builder.RegisterType<Settings>().SingleInstance().As<ISettings>();

            var container = builder.Build();
            var cls = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => cls);

            _settings = container.Resolve<ISettings>();
            _settings.ReadSettings();
        }

        public sealed override Context ApplicationContext => base.ApplicationContext;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CrashManager.Register(this);
            MetricsManager.Register(this, Application);

            Log.Debug(MainActivityTag, "Debug message");

            HockeyApp.MetricsManager.TrackEvent("Application is initializing...");

            InitializeLocationManager();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitializeComponents();

            HockeyApp.MetricsManager.TrackEvent("Application is initialized");

            _buttonShowWeather.Click += _buttonShowWeather_Click;

            await DisplayWeatherAsync("Kiev");

            ShowImg();
        }

        protected override void OnResume()
        {
            base.OnResume();

            InvalidateOptionsMenu();

            ShowDiagInfo("Requesting location updates");
            Log.Debug(MainActivityTag, "Requesting location updates");

            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

            _currentLocation = _locationManager.GetLastKnownLocation(_locationProvider);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
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
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var menuItem = menu.FindItem(Resource.Id.menuItemDrawerTest);
            menuItem?.SetVisible(_settings.EnableTestDrawer);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //ShowDiagInfo(item.TitleFormatted.ToString());

            switch (item.ItemId)
            {
                case Resource.Id.menuItemAbout:
                    StartActivity(typeof(AboutActivity));
                    break;
                case Resource.Id.menuItemDrawerTest:
                    StartActivity(typeof(DrawerTestActivity));
                    break;
                case Resource.Id.menuItemSettings:
                    StartActivity(typeof(SettingsActivity));
                    break;
                case Resource.Id.menuItemRefresh:
                    DisplayWeatherAsync(_editTextCity.Text);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowDiagInfo(string message)
        {
#if DEBUG
            Toast.MakeText(this.ApplicationContext, $"Diagnostics: {message}", ToastLength.Short).Show();

            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Diagnostics");
            //dialog.SetMessage(message);
            //RunOnUiThread(() => { dialog.Show(); });
#endif
        }

        private void ToggleLoadingState(bool isLoading)
        {
            _buttonShowWeather.Enabled = !isLoading;
            _main_ButtonWeatherInCurrentLocation.Enabled = !isLoading;

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
            Log.Debug(MainActivityTag, "Show weather by city name");

            var cityWeatherTask = _weatherApi.GetWeatherByCityNameAsync(city);
            var cityForecastWeatherTask = _weatherApi.GetWeatherForecastByCityNameAsync(city);

            await ShowWeatherAsync(cityWeatherTask, cityForecastWeatherTask);
        }

        async Task ShowWeatherAsync(Coordinates coords)
        {
            Log.Debug(MainActivityTag, "Show weather by coord");
            var cityWeatherTask = _weatherApi.GetWeatherByCoordAsync(coords);
            var cityForecastWeatherTask = _weatherApi.GetWeatherForecastByCoordsAsync(coords);

            await Task.WhenAll(cityWeatherTask, cityForecastWeatherTask);

            await ShowWeatherAsync(cityWeatherTask, cityForecastWeatherTask);
        }

        async Task ShowWeatherAsync(Task<CityWeatherResult> cityWeatherTask,  Task<CityWeatherForecastResult> cityForecastWeatherTask)
        {
            var delayTask = Task.Delay(1000); 

            await Task.WhenAll(cityWeatherTask, cityForecastWeatherTask, delayTask);

            var cityWeather = cityWeatherTask.Result;
            var cityForecastWeather = cityForecastWeatherTask.Result;

            var cityWeatherStatus = cityWeather == null ? "null" : "not null";
            Log.Debug(MainActivityTag, $"City weather is {cityWeatherStatus}");
            var cityForecastWeatherStatus = cityForecastWeather == null ? "null" : "not null";
            Log.Debug(MainActivityTag, $"City weather is {cityForecastWeatherStatus}");

            ShowWeather(cityWeather);
            ShowForecast(cityForecastWeather);

            _textViewUpdated.Text = "Updated just now";
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

        private async void _main_ButtonWeatherInCurrentLocation_Click(object sender, EventArgs e)
        {
            Log.Debug(MainActivityTag, "Requesting current coords");




            var coords = await GetCurrentCoords();

            if (coords == null)
            {
                ShowDiagInfo("Cannot detect current address");
                return;
            }

            var logMsg = $"Lon: {coords.Value.Longtitude}, Lat: {coords.Value.Latitude}";
            ShowDiagInfo(logMsg);
            Log.Debug(MainActivityTag, logMsg);

            await DisplayWeatherAsync(coords.Value);
        }
    }
}

