namespace Weather.Android.AppServices
{
    public interface ISettings
    {
        void ReadSettings();
        void SaveSettings();

        bool EnableExperimentalFeatures { get; set; }

        bool EnableTestDrawer { get; set; }
    }
}