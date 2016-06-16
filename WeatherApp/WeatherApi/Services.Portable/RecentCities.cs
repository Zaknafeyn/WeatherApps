using System.Collections.Generic;
using Services.Portable.DTO;

namespace Services.Portable
{
    public class RecentCities
    {
        private const int MaxCount = 15;
        public List<CityItem> Cities { get; } = new List<CityItem>();

        public void AddCity(CityItem city)
        {
            if (Cities.Contains(city))
            {
                Cities.Remove(city);
            }

            while (Cities.Count >= MaxCount)
            {
                Cities.RemoveAt(0);
            }

            Cities.Add(city);
        }
    }
}