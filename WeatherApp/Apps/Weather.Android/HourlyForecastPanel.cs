using System;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Services.Portable;
using Services.Portable.DTO.Api.WeatherForecast;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp
{
	public class HourlyForecastPanel : LinearLayout
	{
        private Context _ctx;

        private ImageView _imageViewForecastImage;
	    private TextView _textViewHour;
	    private TextView _textViewTemp;

		public HourlyForecastPanel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public HourlyForecastPanel(Context context) : base(context)
		{
			InitComponent(context);
		}

		public HourlyForecastPanel(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			InitComponent(context);
		}

		public HourlyForecastPanel(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			InitComponent(context);
		}

		private void InitComponent(Context ctx)
		{
		    _ctx = ctx;

            var inflater = (LayoutInflater)ctx.GetSystemService(Context.LayoutInflaterService);
			inflater.Inflate(R.Layout.HourlyForecast, this);

			_imageViewForecastImage = FindViewById<ImageView>(R.Id.imageViewForecastImage);
		    _textViewHour = FindViewById<TextView>(R.Id.textViewHour);
		    _textViewTemp = FindViewById<TextView>(R.Id.textViewTemp);
		}

		public void SetWeather(WeatherForecastItem weatherForecastItem)
		{
			var weatherStatus = weatherForecastItem.Weather.First();
			var iconId = Resources.GetWeatherIconResourceId(weatherStatus, _ctx.PackageName);

            _imageViewForecastImage.SetImageResource(iconId);
		    _textViewTemp.Text = weatherForecastItem.Main.Temp.DisplayTemperature();
		    _textViewHour.Text = $"{weatherForecastItem.Dt.Hour} : 00";
		}
	}
}