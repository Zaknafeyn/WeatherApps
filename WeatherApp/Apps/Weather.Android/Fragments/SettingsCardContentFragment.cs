using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Weather.AndroidApp.AppServices;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Fragments
{
    public class SettingsCardContentFragment : Fragment
    {
        private SwitchCompat _switchCompat;

        private readonly ISettings _settingsService;

        public SettingsCardContentFragment(ISettings settingsService)
        {
            _settingsService = settingsService;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(R.Layout.settings_option_item, null);
            _switchCompat = view.FindViewById<SwitchCompat>(R.Id.switchCompatEnableTestDrawer);
            _switchCompat.Checked = _settingsService.EnableTestDrawer;
            return view;
        }

        public override void OnResume()
        {
            _settingsService.ReadSettings();
            _switchCompat.Checked = _settingsService.EnableTestDrawer;

            base.OnResume();
        }

        public override void OnPause()
        {
            _settingsService.EnableTestDrawer = _switchCompat.Checked;
            _settingsService.SaveSettingsAsync();
            base.OnPause();
        }
    }
}