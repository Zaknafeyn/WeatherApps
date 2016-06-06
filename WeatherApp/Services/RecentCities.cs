using System.Collections.Generic;

namespace Services
{
    public class RecentCities
    {
        private const int MaxCount = 5;
        public List<string> Cities { get; } = new List<string>();

        public void AddCity(string city)
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