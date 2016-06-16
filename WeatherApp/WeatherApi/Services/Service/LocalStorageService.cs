using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Portable;

namespace Services.Service
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly ILogger _logger;
        private const string StorageFileName = "IsolatedFile";

        SettingStorage _settingsStorage = new SettingStorage();

        private IsolatedStorageFile _isolatedStorage =
            IsolatedStorageFile.GetStore(
                IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

        public LocalStorageService(ILogger logger)
        {
            _logger = logger;
        }

        public RecentCities RecentCities => _settingsStorage.RecentCities;
        public List<string> FavouriteCities => _settingsStorage.FavouriteCities;

        public void Read()
        {
            string result;

            using (var isolatedFileStream = new IsolatedStorageFileStream(StorageFileName, FileMode.OpenOrCreate, _isolatedStorage))
            using (var reader = new StreamReader(isolatedFileStream))
            {
                result = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
                return;

            _logger.Log(result);

            try
            {
                var deserialized = JsonConvert.DeserializeObject<SettingStorage>(result);

                 deserialized.RecentCities?.Cities?.ForEach(x => _settingsStorage.RecentCities.AddCity(x)); 
                 deserialized.FavouriteCities?.ForEach(x => _settingsStorage.FavouriteCities.Add(x)); 
            }
            catch (JsonSerializationException ex)
            {
                _isolatedStorage.Remove();
            }
        }

        public void Save()
        {
            using (var isolatedFileStream = new IsolatedStorageFileStream(StorageFileName, FileMode.Truncate, _isolatedStorage))
            using (var writer = new StreamWriter(isolatedFileStream))
            {
                var serializedObj = JsonConvert.SerializeObject(_settingsStorage);
                writer.Write(serializedObj);

                _logger.Log("Serialized object");
                _logger.Log(serializedObj);
            }
        }


        internal class SettingStorage
        {
            public RecentCities RecentCities { get; set; } = new RecentCities();
            public List<string> FavouriteCities { get; set; } = new List<string>();
        }
    }
}