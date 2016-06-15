using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Services.DTO;
using Services.DTO.Api;
using Services.Events;
using Services.Interfaces;
using WeatherModule.DataConverters;
using WeatherModule.Models;

namespace WeatherModule.ViewModels
{
    public class WeatherViewModel : BindableBase
    {
        private readonly IWeatherSevice _weatherSevice;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalStorageService _localStorage;
        private bool _isBusy;
        private CityWeatherStatus _weather;
        private Uri _iconUri;
        private string _city;
        private decimal _currentDegrees;
        private string _weatherDescr;
        private decimal _minTemp;
        private decimal _maxTemp;
        private decimal _windSpeed;
        private WeatherForecastModel _weatherForecastTomorrow;

        public WeatherViewModel(IWeatherSevice weatherSevice, IEventAggregator eventAggregator, ILocalStorageService localStorage)
        {
            _weatherSevice = weatherSevice;
            _eventAggregator = eventAggregator;
            _localStorage = localStorage;

            LoadWeatherCommand = new DelegateCommand(LoadWeatherCommandExecuted, LoadWeatherCommandCanExecute);

            // event subscribe
            var @event = _eventAggregator.GetEvent<RecentCitySelectionChangedEvent>();
            @event.Subscribe(RecentCitySelectionChangedEventHandler);

            City = "Kiev";
            LoadWeatherCommand.Execute();
        }

        public DelegateCommand LoadWeatherCommand { get; }

        public CityWeatherStatus Weather
        {
            get { return _weather; }
            set { SetProperty(ref _weather, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string City
        {
            get { return _city; }
            set
            {
                SetProperty(ref _city, value);
                LoadWeatherCommand.RaiseCanExecuteChanged();
            }
        }

        public Uri IconUri
        {
            get { return _iconUri; }
            set { SetProperty(ref _iconUri, value); }
        }

        public decimal CurrentDegrees
        {
            get { return _currentDegrees; }
            set { SetProperty(ref _currentDegrees, value); }
        }

        public string WeatherDescr
        {
            get { return _weatherDescr; }
            set { SetProperty(ref _weatherDescr, value); }
        }

        public decimal MinTemp
        {
            get { return _minTemp; }
            set { SetProperty(ref _minTemp, value); }
        }

        public decimal MaxTemp
        {
            get { return _maxTemp; }
            set { SetProperty(ref _maxTemp, value); }
        }

        public decimal WindSpeed
        {
            get { return _windSpeed; }
            set { SetProperty(ref _windSpeed, value); }
        }

        public ObservableCollection<WeatherForecastModel> WeatherForecastCollection { get; } = new ObservableCollection<WeatherForecastModel>();

        public WeatherForecastModel WeatherForecastTomorrow
        {
            get { return _weatherForecastTomorrow; }
            set { SetProperty(ref _weatherForecastTomorrow, value); }
        }

        private bool LoadWeatherCommandCanExecute()
        {
            return !string.IsNullOrEmpty(City);
        }

        private async void LoadWeatherCommandExecuted()
        {
            await LoadWeatherByCityNameAsync(City);

            DoCityWeatherRequestSend(new CityItem
            {
                CityId = Weather.Id,
                CityName = Weather.Name
            });
        }

        private async Task LoadWeatherByCityNameAsync(string cityName)
        {
            try
            {
                IsBusy = true;

                var weatherTask = _weatherSevice.GetWeatherByCityNameAsync(cityName);
                var weatherForecastTask = _weatherSevice.GetWeatherForecastByCityNameAsync(cityName);
                await Task.WhenAll(weatherTask, weatherForecastTask);

                var weather = weatherTask.Result;
                var weatherForecast = weatherForecastTask.Result;

                LoadWeather(weather, weatherForecast);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadWeatherByCityIdAsync(int cityId)
        {
            try
            {
                IsBusy = true;

                var weatherTask = _weatherSevice.GetWeatherByCityIdAsync(cityId);
                var weatherForecastTask = _weatherSevice.GetWeatherForecastByCityIdAsync(cityId);
                await Task.WhenAll(weatherTask, weatherForecastTask);

                var weather = weatherTask.Result;
                var weatherForecast = weatherForecastTask.Result;

                LoadWeather(weather, weatherForecast);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadWeather(CityWeatherStatus currentWeather, CityWeatherForecast weatherForecast)
        {
            Weather = currentWeather;

            var weatherForecastHourly =
                    weatherForecast.HourlyForecast.Select(WeatherConverters.Convert).ToList();

            WeatherForecastTomorrow = weatherForecastHourly.Skip(7).Take(1).First();

            WeatherForecastCollection.Clear();
            WeatherForecastCollection.AddRange(weatherForecastHourly);


            var weatherStatus = currentWeather.Weather.First();
            var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";
            IconUri = new Uri($"pack://application:,,,/WeatherModule;component/Resources/Icons/WeatherIcons/{weatherStatus.Id}{dayOrNight}.png");

            CurrentDegrees = Weather.Main.Temp.NormalizeTemperature();
            MinTemp = currentWeather.Main.TempMin.NormalizeTemperature();
            MaxTemp = currentWeather.Main.TempMax.NormalizeTemperature();
            WeatherDescr = weatherStatus.Description;
            WindSpeed = currentWeather.Wind.Speed;
        }

        private async void RecentCitySelectionChangedEventHandler(CityItem recentCityItem)
        {
            City = recentCityItem.CityName;
            await LoadWeatherByCityIdAsync(recentCityItem.CityId);
        }

        private void DoCityWeatherRequestSend(CityItem city)
        {
            _localStorage.RecentCities.AddCity(city);

            var @event =
            _eventAggregator.GetEvent<CityWeatherRequestSentEvent>();
            @event.Publish(city);
        }
    }
}