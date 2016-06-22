using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Services.Portable.API;

namespace Weather.Android
{
    [Activity(Label = "Weather.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarLoading);
            var imgView = FindViewById<ImageView>(Resource.Id.imageViewCurrentWeather);
            var cityIndicator = FindViewById<TextView>(Resource.Id.textViewCity);
            var currentTempIndicator = FindViewById<TextView>(Resource.Id.textViewCurrentTemp);

            try
            {
                imgView.Visibility = ViewStates.Gone;
                currentTempIndicator.Visibility = ViewStates.Gone;

                progressBar.Visibility = ViewStates.Visible;

                var api = new WeatherApi();
                var cityWeather = await api.GetWeatherByCityNameAsync("New York");
                var weatherStatus = cityWeather.Weather.First();

                var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";

                currentTempIndicator.Text = $"{cityWeather.Main.Temp-273.15m}º";
                cityIndicator.Text = $"{cityWeather.Name}";
                var drawableId = Resources.GetIdentifier($"WeatherStatus{weatherStatus.Id}{dayOrNight}".ToLower(), "drawable", PackageName);
                imgView.SetImageResource(drawableId);
            }
            finally
            {
                imgView.Visibility = ViewStates.Visible;
                currentTempIndicator.Visibility = ViewStates.Visible;

                progressBar.Visibility = ViewStates.Gone;
            }

            // Get our button from the layout resource,
            // and attach an event to it

            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += (sender, args) => button.Text = $"{count++} clicks!";
        }
    }
}

