namespace Weather.Android.AppServices
{
    public interface IStorageService
    {
        void SaveValue<T>(string key, T value);
        T ReadValue<T>(string key);
    }
}