using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ILocalStorageService
    {
        RecentCities RecentCities { get; }
        List<string> FavouriteCities { get; }

        void Read();
        void Save();
    }
}