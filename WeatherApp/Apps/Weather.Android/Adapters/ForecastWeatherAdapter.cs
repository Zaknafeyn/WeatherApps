using System.Collections.Generic;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Services.Portable;
using Weather.AndroidApp.Models;

namespace Weather.AndroidApp.Adapters
{
    public class ForecastWeatherAdapter : RecyclerView.Adapter
    {
        List<WeatheForecastItem> _forecastItems = new List<WeatheForecastItem>();

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public ViewHolder(View v) : base(v)
            {
                ImageForecast = v.FindViewById<ImageView>(Resource.Id.imageViewForecastWeather);
                DayName = v.FindViewById<TextView>(Resource.Id.textViewWeekDay);
                TempDay = v.FindViewById<TextView>(Resource.Id.textViewDayTemp);
                TempNignt = v.FindViewById<TextView>(Resource.Id.textViewNightTemp);
            }

            public ImageView ImageForecast { get; }

            public TextView DayName { get; }

            public TextView TempDay { get; }

            public TextView TempNignt { get; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var myHolder = (ViewHolder)holder;
            var item = _forecastItems[position];
            myHolder.DayName.Text = item.Day.ToString("ddd", CultureInfo.InvariantCulture);
            myHolder.TempDay.Text = item.TempDay.DisplayTemperature();
            myHolder.TempDay.Text = item.TempNight.DisplayTemperature();
            myHolder.ImageForecast.SetImageResource(item.DateImageResourceId);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.city_forecats_list_item, parent, false);
            return new ViewHolder(view);
        }

        public override int ItemCount => _forecastItems.Count;

        public void AddForecastItem(WeatheForecastItem item)
        {
            _forecastItems.Add(item);
        }
    }
}