using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Mvvm;
using Services.Events;
using Services.Interfaces;
using Services.Portable.DTO;

namespace WeatherModule.ViewModels
{
    public class RecentCitiesViewModel : BindableBase
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IEventAggregator _eventAggregator;

        private CityItem _recentCitySelected;

        public RecentCitiesViewModel(ILocalStorageService localStorageService, IEventAggregator eventAggregator)
        {
            _localStorageService = localStorageService;
            _eventAggregator = eventAggregator;

            RecentCities.AddRange(localStorageService.RecentCities.Cities);

            // event subscribe
            var @event = _eventAggregator.GetEvent<CityWeatherRequestSentEvent>();
            @event.Subscribe(CityWeatherRequestSentEventHandler);
        }

        public ObservableCollection<CityItem> RecentCities { get; } = new ObservableCollection<CityItem>();

        public CityItem RecentCitySelected
        {
            get { return _recentCitySelected; }
            set
            {
                SetProperty( ref _recentCitySelected, value);
                OnRecentCitySelectionChanged(value);
            }
        }

        private void OnRecentCitySelectionChanged(CityItem selectedCityItem)
        {
            if (selectedCityItem == null)
                return;

            var @event = _eventAggregator.GetEvent<RecentCitySelectionChangedEvent>();
            @event.Publish(selectedCityItem);
        }

        private void CityWeatherRequestSentEventHandler(CityItem city)
        {
            RecentCities.Clear();
            RecentCities.AddRange(_localStorageService.RecentCities.Cities);
        }
    }
}