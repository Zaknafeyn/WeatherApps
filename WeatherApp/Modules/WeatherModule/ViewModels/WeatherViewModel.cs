using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Services.DTO.WeatherInCity;
using Services.Events;
using Services.Interfaces;

namespace WeatherModule.ViewModels
{
    public class WeatherViewModel : BindableBase
    {
        private readonly IWeatherSevice _weatherSevice;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalStorageService _localStorage;
        private CityWeatherStatus _weather;
        private string _city;

        public WeatherViewModel(IWeatherSevice weatherSevice, IEventAggregator eventAggregator, ILocalStorageService localStorage)
        {
            _weatherSevice = weatherSevice;
            _eventAggregator = eventAggregator;
            _localStorage = localStorage;

            LoadWeatherCommand = new DelegateCommand(LoadWeatherCommandExecuted, LoadWeatherCommandCanExecute);
        }

        private void DoCityWeatherRequestSend(string city)
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

        public string City
        {
            get { return _city; }
            set
            {
                SetProperty( ref _city, value); 
                LoadWeatherCommand.RaiseCanExecuteChanged();
            }
        }

        private bool LoadWeatherCommandCanExecute()
        {
            return !string.IsNullOrEmpty(City);
        }

        private async void LoadWeatherCommandExecuted()
        {
            Weather = await _weatherSevice.GetWeatherByCityNameAsync(City);
            DoCityWeatherRequestSend(City);
        }
    }
}