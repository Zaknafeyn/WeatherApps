using Prism.Commands;
using Prism.Mvvm;
using Services.DTO.WeatherInCity;
using Services.Interfaces;

namespace WpfApplication1.ViewModels
{
    public class WeatherViewModel : BindableBase
    {
        private readonly IWeatherSevice _weatherSevice;
        private CityWeatherStatus _weather;
        private string _city;

        public WeatherViewModel(IWeatherSevice weatherSevice)
        {
            _weatherSevice = weatherSevice;

            LoadWeatherCommand = new DelegateCommand(LoadWeatherCommandExecuted, LoadWeatherCommandCanExecute);
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
        }
    }
}