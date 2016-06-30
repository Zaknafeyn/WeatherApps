using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather.Android.AppServices
{
    public class DictionaryStorageService : IStorageService
    {
        private Dictionary<string, string> _dict = new Dictionary<string, string>();

        public void SaveValue<T>(string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            _dict[key] = serializedValue;
        }

        public T ReadValue<T>(string key)
        {
            if (!_dict.ContainsKey(key))
                return default(T);

            var value = JsonConvert.DeserializeObject<T>(_dict[key]);
            return value;
        }
    }
}