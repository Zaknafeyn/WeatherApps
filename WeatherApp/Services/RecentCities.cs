using System.Collections.Generic;
using Services.DTO;

namespace Services
{
    public class RecentCities
    {
        private const int MaxCount = 5;
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