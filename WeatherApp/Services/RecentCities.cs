using System.Collections.Generic;

namespace Services
{
    public class RecentCities 
    {
        public List<string> Cities { get; } = new List<string>();

        public void AddCity(string city)
        {
            if (Cities.Contains(city))
            {
                Cities.Remove(city);
            }

            Cities.Add(city);
        }
    }
}