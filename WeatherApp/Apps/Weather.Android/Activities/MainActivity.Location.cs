using System.Linq;
using System.Threading.Tasks;
using Android.Locations;
using Android.OS;
using Android.Util;
using Services.Portable.DTO;

namespace Weather.Android.Activities
{
    public partial class MainActivity : ILocationListener
    {
        Location _currentLocation;
        LocationManager _locationManager;
        private string _locationProvider;

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            var criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Coarse,
                PowerRequirement = Power.Low,
            };

            var acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            _locationProvider = acceptableLocationProviders.Any() ? acceptableLocationProviders.First() : string.Empty;
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            var geocoder = new Geocoder(this);
            Log.Debug(MainActivityTag, $"ReverseGeocodeCurrentLocation ... ");
            var addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Log.Debug(MainActivityTag, $"Count of addresses: {addressList.Count}");
            var address = addressList.FirstOrDefault();

            return address;
        }

        async Task<Coordinates?> GetCurrentCoords()
        {
            if (_currentLocation != null)
                return new Coordinates
                {
                    Longtitude = (decimal)_currentLocation.Longitude,
                    Latitude = (decimal)_currentLocation.Latitude
                };

            return null;

            var address = await ReverseGeocodeCurrentLocation();

            if (address == null)
                return null;

            return new Coordinates
            {
                Latitude = (decimal)address.Latitude,
                Longtitude = (decimal)address.Longitude
            };
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            Log.Debug(MainActivityTag, "Location changed");
            Log.Debug(MainActivityTag, $"{_currentLocation.Latitude:f6},{_currentLocation.Longitude:f6}");


            if (_currentLocation == null)
            {
                //_locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                //var coords = await GetCurrentCoords();
                //await DisplayWeatherAsync(coords);

                //_locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                //var address = await ReverseGeocodeCurrentLocation();
                //DisplayAddress(address);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            ShowDiagInfo($"Provider {provider} - disabled");
        }

        public void OnProviderEnabled(string provider)
        {
            ShowDiagInfo($"Provider {provider} - enabled");
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            ShowDiagInfo($"Provider {provider} - status changed. Status - {status}");
        }
    }
}