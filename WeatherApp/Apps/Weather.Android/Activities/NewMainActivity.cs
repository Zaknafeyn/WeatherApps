using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Microsoft.Practices.ServiceLocation;
using Android.Widget;
using Services.Portable.Service;
using Weather.AndroidApp.Activities.Experimental;
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
        private IAppLogger _appLogger;
        private ISettings _settings;

        private ProgressBar _progressBar;

        public NewMainActivity()
        {
            //AutoFac

            var builder = new ContainerBuilder();
            builder.RegisterInstance(Application.Context).As<Context>();
            //builder.RegisterInstance(Application).As<Application>();

            builder.RegisterType<StorageService>().SingleInstance().As<IStorageService>();

            builder.RegisterType<Settings>().SingleInstance().As<ISettings>();

#if DebugWithMock
            builder.RegisterType<WeatherServiceMock>().As<IWeatherService>();
            //builder.RegisterType<AppLogger>().As<IAppLogger>().SingleInstance();
#else
            builder.RegisterType<WeatherService>().As<IWeatherService>();
            builder.RegisterType<HockeyAppLogger>().As<IAppLogger>().SingleInstance();
#endif
            //builder.RegisterModule<ServiceApiModule>();

            var container = builder.Build();
            var cls = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => cls);

            _weatherService = container.Resolve<IWeatherService>();
            _settings = container.Resolve<ISettings>();
            _appLogger = container.Resolve<IAppLogger>();

            _settings.ReadSettings();

        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

#if !DebugWithMock

            //_appLogger.Init(this, Application);

            //CrashManager.Register(this);
            //MetricsManager.Register(this, Application);

            //HockeyApp.MetricsManager.TrackEvent("Application is initializing...");

#endif
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

            var updatableForecastFragment = (IUpdateFragment)cityForecastFragment;
            updatableForecastFragment.Update(weatherForecast);

            var updatableFragment = (IUpdateFragment)cityWeatherFragment;
            updatableFragment.Update(weather);

            SupportFragmentManager.ShowFragment(cityWeatherFragment, cityForecastFragment);

#if !DebugWithMock
            //HockeyApp.MetricsManager.TrackEvent("Application is initialized");
            _appLogger.Log("Application is initialized");
#endif
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _appLogger.Log("Creating menu");
            MenuInflater.Inflate(R.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            _appLogger.Log("Preparing menu");
            var menuItem = menu.FindItem(Resource.Id.menuItemDrawerTest);
            menuItem?.SetVisible(_settings.EnableTestDrawer);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
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
                //case Resource.Id.menuItemRefresh:
                //    RefreshMenuAction(item);
                //    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        //async void RefreshMenuAction(IMenuItem menuItem)
        //{
        //    var u = AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_image);

        //    using (var imageView = new ImageView(this))
        //    {
        //        imageView.SetImageResource(Resource.Drawable.ic_sync_white_24dp);
        //        imageView.SetMinimumHeight(100);
        //        imageView.StartAnimation(u);

        //        var actionView = menuItem.ActionView;
        //        menuItem.SetActionView(imageView);

        //        await DisplayWeatherAsync(_editTextCity.Text);

        //        menuItem.SetActionView(actionView);
        //    }
        //}

        //private bool IsNetworkConnected()
        //{
        //    var cm = (ConnectivityManager)GetSystemService(ConnectivityService);

        //    return cm?.ActiveNetworkInfo != null;
        //}

    }
}