using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Services.Portable.DTO.Api;
using Weather.AndroidApp.Adapters;
using Weather.AndroidApp.Interfaces;
using Weather.AndroidApp.Models;

namespace Weather.AndroidApp.Fragments
{
    public class CityForecatsCardFragment : Fragment, IUpdateFragment<CityWeatherForecastResult >
    {
        private ForecastWeatherAdapter _adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.city_forecast_fragment, null);

            var linearManager = new LinearLayoutManager(Activity)
            {
                Orientation = 0
            };

            _adapter = new ForecastWeatherAdapter();

            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewForecast);

            recyclerView.SetLayoutManager(linearManager);
            recyclerView.ScrollToPosition(0);
            recyclerView.SetAdapter(_adapter);

            return view;
        }

        public void Update(CityWeatherForecastResult data)
        {
            var dailyForecast = GetDailyForecats(data);
            foreach (var weatheForecastItem in dailyForecast)
            {
                _adapter.AddForecastItem(weatheForecastItem);
            }

            _adapter.NotifyDataSetChanged();
        }

        public void Update(object data)
        {
            Update(data as CityWeatherForecastResult);
        }

        private IEnumerable<WeatheForecastItem> GetDailyForecats(CityWeatherForecastResult forecast)
        {
            //int skipItemsCount = 0;
            //var currentHour = DateTime.Now.Hour;
            //if (currentHour > 0 && currentHour < 6)
            //    skipItemsCount = 8 + 3;
            //else if (currentHour < 18)
            //    skipItemsCount = 8 + 0;
            //else
            //    skipItemsCount = 8-3;

            for (int i = 0; i < 5; i++)
            {
                var dayItems = forecast.HourlyForecast.Skip(i*8 - 1).Take(8);
                
                var day = dayItems.First();
                var night = dayItems.Skip(4).First();

                var weather = day.Weather.First();

                yield return new WeatheForecastItem
                {
                    TempNight = night.Main.Temp,
                    TempDay = day.Main.Temp,
                    Day = DateTime.Now.AddDays(i + 1),
                    DateImageName = weather.GetWeatherIconName(),
                    DateImageResourceId = Activity.Resources.GetWeatherIconResourceId(weather, Activity.PackageName)
                };
            }
        }
    }
}