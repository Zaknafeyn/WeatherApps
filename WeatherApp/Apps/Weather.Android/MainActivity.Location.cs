using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Locations;
using Android.OS;
using Services.Portable.DTO;

namespace Weather.Android
{
    public partial class MainActivity
    {
        Location _currentLocation;
        LocationManager _locationManager;
        private string _locationProvider;

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            var address = addressList.FirstOrDefault();

            return address;
        }


        async Task<Coordinates> GetCurrentCoords()
        {
            InitializeLocationManager();

            var address = await ReverseGeocodeCurrentLocation();

            return new Coordinates
            {
                Latitude = (decimal)address.Latitude,
                Longtitude = (decimal)address.Longitude
            };
        }

        public void OnLocationChanged(Location location)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}