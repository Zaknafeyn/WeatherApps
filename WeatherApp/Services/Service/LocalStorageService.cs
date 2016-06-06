using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Services.Interfaces;

namespace Services.Service
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string StorageFileName = "IsolatedFile";

        private IsolatedStorageFile _isolatedStorage =
            IsolatedStorageFile.GetStore(
                IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

        public RecentCities RecentCities { get; private set; } = new RecentCities();
        public List<string> FavouriteCities { get; private set; } = new List<string>();

        public void Read()
        {
            //_isolatedStorage.Remove();
            using (var isolatedFileStream = new IsolatedStorageFileStream(StorageFileName, FileMode.OpenOrCreate, _isolatedStorage))
            using (var reader = new StreamReader(isolatedFileStream))
            {
                var result = reader.ReadToEnd();
                if (string.IsNullOrEmpty(result))
                    return;

                var deserialized = JsonConvert.DeserializeObject<LocalStorageService>(result);

                FavouriteCities = deserialized.FavouriteCities;
                RecentCities = deserialized.RecentCities;
            }
        }

        public void Save()
        {
            using (var isolatedFileStream = new IsolatedStorageFileStream(StorageFileName, FileMode.OpenOrCreate, _isolatedStorage))
            using (var writer = new StreamWriter(isolatedFileStream))
            {
                var serializedObj = JsonConvert.SerializeObject(this);
                writer.Write(serializedObj);
            }
        }

    }
}