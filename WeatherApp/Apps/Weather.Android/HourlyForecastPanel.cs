using System;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Services.Portable.DTO.Api.WeatherForecast;

namespace Weather.Android
{
	public class HourlyForecastPanel : LinearLayout
	{
		private ImageView _imageViewForecastImage;
	    private Context _ctx;

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
			inflater.Inflate(Resource.Layout.HourlyForecast, this);

			_imageViewForecastImage = FindViewById<ImageView>(Resource.Id.imageViewForecastImage);
		}

		public void SetWeather(WeatherForecastItem weatherForecastItem)
		{
			var weatherStatus = weatherForecastItem.Weather.First();
			var iconId = Resources.GetWeatherIconResourceId(weatherStatus, _ctx.PackageName);

            _imageViewForecastImage.SetImageResource(iconId);
        }
	}
}