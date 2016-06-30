namespace Weather.Android.AppServices
{
    public class Settings : ISettings
    {
        private readonly IStorageService _storageService;
        private bool _settingsLoaded;

        public Settings(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void ReadSettings()
        {
            if (_settingsLoaded)
                return;

            _settingsLoaded = true;

            EnableTestDrawer = _storageService.ReadValue<bool>("EnableTestDrawer");
        }

        public void SaveSettings()
        {
            _storageService.SaveValue("EnableTestDrawer", EnableTestDrawer);
        }

        public bool EnableExperimentalFeatures { get; set; }
        public bool EnableTestDrawer { get; set; }
    }
}