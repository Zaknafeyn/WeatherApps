using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Services.DTO;
using Services.DTO.Api;
using Services.Events;
using Services.Interfaces;

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
        private string _currentDegrees;
        private string _weatherDescr;
        private decimal _minTemp;
        private decimal _maxTemp;
        private decimal _windSpeed;

        public WeatherViewModel(IWeatherSevice weatherSevice, IEventAggregator eventAggregator, ILocalStorageService localStorage)
        {
            _weatherSevice = weatherSevice;
            _eventAggregator = eventAggregator;
            _localStorage = localStorage;

            LoadWeatherCommand = new DelegateCommand(LoadWeatherCommandExecuted, LoadWeatherCommandCanExecute);

            City = "London";
            LoadWeatherCommand.Execute();
        }

        private void DoCityWeatherRequestSend(CityItem city)
        {
            _localStorage.RecentCities.AddCity(city);

            var @event =
            _eventAggregator.GetEvent<CityWeatherRequestSentEvent>();
            @event.Publish(city);
        }

        public DelegateCommand LoadWeatherCommand { get; }

        public CityWeatherStatus Weather
        {
            get { return _weather; }
            set { SetProperty( ref _weather, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty( ref _isBusy, value); }
        }

        public string City
        {
            get { return _city; }
            set
            {
                SetProperty( ref _city, value); 
                LoadWeatherCommand.RaiseCanExecuteChanged();
            }
        }

        public Uri IconUri
        {
            get { return _iconUri; }
            set { SetProperty( ref _iconUri, value); }
        }

        public string CurrentDegrees
        {
            get { return _currentDegrees; }
            set { SetProperty( ref _currentDegrees, value); }
        }

        public string WeatherDescr
        {
            get { return _weatherDescr; }
            set { SetProperty( ref _weatherDescr, value); }
        }

        public decimal MinTemp
        {
            get { return _minTemp; }
            set { SetProperty( ref _minTemp, value); }
        }

        public decimal MaxTemp
        {
            get { return _maxTemp; }
            set { SetProperty( ref _maxTemp, value); }
        }

        public decimal WindSpeed
        {
            get { return _windSpeed; }
            set { SetProperty( ref _windSpeed, value); }
        }

        private bool LoadWeatherCommandCanExecute()
        {
            return !string.IsNullOrEmpty(City);
        }

        private async void LoadWeatherCommandExecuted()
        {
            try
            {
                IsBusy = true;

                Weather = await _weatherSevice.GetWeatherByCityNameAsync(City);

                LoadWeather(Weather);
                
            }
            finally
            {
                IsBusy = false;
            }
            
            DoCityWeatherRequestSend(new CityItem
            {
                CityId = Weather.Id,
                CityName = Weather.Name
            });
        }

        private void LoadWeather(CityWeatherStatus weather)
        {
            var weatherStatus = weather.Weather.First();
            var dayOrNight = weatherStatus.Icon.EndsWith("d") ? "d" : "n";
            IconUri = new Uri($"pack://application:,,,/WeatherModule;component/Resources/Icons/WeatherIcons/{weatherStatus.Id}{dayOrNight}.png");

            var temperature = Weather.Main.Temp.NormalizeTemperature();
            CurrentDegrees = $"{temperature}º";
            MinTemp = weather.Main.TempMin.NormalizeTemperature();
            MaxTemp = weather.Main.TempMax.NormalizeTemperature();
            WeatherDescr = weatherStatus.Description;
            WindSpeed = weather.Wind.Speed;
        }
    }
}