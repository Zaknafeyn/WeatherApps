using Android.Content;
using Newtonsoft.Json;

namespace Weather.AndroidApp.AppServices
{
    public class StorageService : IStorageService
    {
        private readonly Context _ctx;

        public StorageService(Context ctx)
        {
            _ctx = ctx;
        }

        public void SaveValue<T>(string key, T value)
        {
            var preferences = _ctx.GetSharedPreferences(key, FileCreationMode.Private);
            var prefEditor = preferences.Edit();
            var serializedValue = JsonConvert.SerializeObject(value);
            prefEditor.PutString(key, serializedValue);
            prefEditor.Commit();
        }

        public T ReadValue<T>(string key)
        {
            var preferences = _ctx.GetSharedPreferences(key, FileCreationMode.Private);
            var serializedValue = preferences.GetString(key, string.Empty);
            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }

            var value = JsonConvert.DeserializeObject<T>(serializedValue);
            return value;
        }
    }
}