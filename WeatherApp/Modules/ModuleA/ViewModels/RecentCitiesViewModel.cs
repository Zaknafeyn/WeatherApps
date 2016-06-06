using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Mvvm;
using Services.Events;
using Services.Interfaces;

namespace ModuleA.ViewModels
{
    public class RecentCitiesViewModel : BindableBase
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IEventAggregator _eventAggregator;

        public RecentCitiesViewModel(ILocalStorageService localStorageService, IEventAggregator eventAggregator)
        {
            _localStorageService = localStorageService;
            _eventAggregator = eventAggregator;

            RecentCities.AddRange(localStorageService.RecentCities.Cities);

            var @event = _eventAggregator.GetEvent<CityWeatherRequestSentEvent>();
            @event.Subscribe(CityWeatherRequestSentEventHandler);
        }

        public ObservableCollection<string> RecentCities { get; } = new ObservableCollection<string>();

        private void CityWeatherRequestSentEventHandler(string city)
        {
            RecentCities.Clear();
            RecentCities.AddRange(_localStorageService.RecentCities.Cities);
        }
    }
}