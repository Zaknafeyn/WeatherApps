using System.Threading.Tasks;

namespace Weather.Android.AppServices
{
    public interface ISettings
    {
        void ReadSettings();
        void SaveSettings();

        Task ReadSettingsAsync();
        Task SaveSettingsAsync();

        bool EnableExperimentalFeatures { get; set; }

        bool EnableTestDrawer { get; set; }
    }
}