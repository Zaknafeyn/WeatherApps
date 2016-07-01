using Android.Widget;

namespace Weather.Android.Activities
{
    public partial class MainActivity
    {
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
        private Button _main_ButtonWeatherInCurrentLocation;
        private TextView _textViewUpdated;

        private void InitializeComponents()
        {
            _editTextCity = FindViewById<EditText>(Resource.Id.editTextCity);
            _buttonShowWeather = FindViewById<Button>(Resource.Id.MyButton);
            _buttonShowWeather.RequestFocus();

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
            _main_ButtonWeatherInCurrentLocation = FindViewById<Button>(Resource.Id.Main_ButtonWeatherInCurrentLocation);
            _main_ButtonWeatherInCurrentLocation.Click += _main_ButtonWeatherInCurrentLocation_Click;

            _textViewUpdated = FindViewById<TextView>(Resource.Id.textViewUpdated);
        }
    }
}