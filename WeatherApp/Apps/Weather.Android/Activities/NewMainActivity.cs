using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Microsoft.Practices.ServiceLocation;
using Android.Widget;
using Services.Portable.Service;
using Weather.AndroidApp.AppServices;
using Weather.AndroidApp.Fragments;
using Weather.AndroidApp.Interfaces;
using R = Weather.AndroidApp.Resource;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Weather.AndroidApp.Activities
{
    [Activity(Label = "Weather", MainLauncher = true, Theme = "@style/MyTheme", Icon = "@drawable/icon")]
    public class NewMainActivity : AppCompatActivity
    {
        private readonly IWeatherService _weatherService;
        private ISettings _settings;

        private ProgressBar _progressBar;

        public NewMainActivity()
        {
            //AutoFac

            var builder = new ContainerBuilder();
            builder.RegisterInstance(Application.Context).As<Context>();

            builder.RegisterType<StorageService>().SingleInstance().As<IStorageService>();

            builder.RegisterType<Settings>().SingleInstance().As<ISettings>();
            builder.RegisterType<WeatherService>().As<IWeatherService>();
            //builder.RegisterModule<ServiceApiModule>();

            var container = builder.Build();
            var cls = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => cls);

            _weatherService = container.Resolve<IWeatherService>();
            //_weatherService = new WeatherService();
            _settings = container.Resolve<ISettings>();
            _settings.ReadSettings();

        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (IsNetworkConnected())
                Toast.MakeText(this, "Internet is available", ToastLength.Long);
            else
                Toast.MakeText(this, "Internet is NOT available", ToastLength.Long);

            CrashManager.Register(this);
            MetricsManager.Register(this, Application);

            HockeyApp.MetricsManager.TrackEvent("Application is initializing...");

            // Create your application here

            SetContentView(R.Layout.main_layout);

            _progressBar = FindViewById<ProgressBar>(R.Id.progressBarLoading);

            var toolbar = FindViewById<Toolbar>(R.Id.toolbar_actionbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Hello from Appcompat Toolbar";

            _progressBar.Visibility = ViewStates.Visible;

            var cityWeatherFragment = SupportFragmentManager.FindFragmentById(R.Id.cityWeatherFragment);
            var cityForecastFragment = SupportFragmentManager.FindFragmentById(R.Id.cityForecastFragment);
            
            SupportFragmentManager.HideFragment(cityWeatherFragment, cityForecastFragment);

            var weatherTask = _weatherService.GetWeatherByCityNameAsync("Kiev");
            var weatherForecastTask = _weatherService.GetWeatherForecastByCityNameAsync("Kiev");
            var delayTask = Task.Delay(2500);
            await Task.WhenAll(weatherTask, weatherForecastTask, delayTask);
            var weather = weatherTask.Result;
            var weatherForecast = weatherForecastTask.Result;

            _progressBar.Visibility = ViewStates.Gone;

            var updatableForecastFragment = (IUpdateFragment) cityForecastFragment;
            updatableForecastFragment.Update(weatherForecast);

            var updatableFragment = (IUpdateFragment)cityWeatherFragment;
            updatableFragment.Update(weather);

            SupportFragmentManager.ShowFragment(cityWeatherFragment, cityForecastFragment);

            HockeyApp.MetricsManager.TrackEvent("Application is initialized");
        }

        private bool IsNetworkConnected()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);

            return cm?.ActiveNetworkInfo != null;
        }

    }
}