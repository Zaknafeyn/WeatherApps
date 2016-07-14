using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Microsoft.Practices.ServiceLocation;
using Weather.AndroidApp.Adapters;
using Weather.AndroidApp.AppServices;
using Weather.AndroidApp.Fragments;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Activities
{
    [Activity(Label = "Settings", Icon = "@drawable/ic_settings_black_24dp", Theme = "@style/MyTheme")]
    public class SettingsActivity : AppCompatActivity
    {
        private ISettings _settings;

        public SettingsActivity()
        {
            var locator = ServiceLocator.Current;

            _settings = locator.GetInstance<ISettings>();
            _settings.ReadSettings();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            Init();
        }

        private void Init()
        {
            SetContentView(R.Layout.settings_layout);

            var toolbar = FindViewById<Toolbar>(R.Id.settings_toolbar);
            SetSupportActionBar(toolbar);
            
            var viewPager = FindViewById<ViewPager>(R.Id.settings_viewpager);
            SetupViewPager(viewPager);
        }

        // Add Fragments to Tabs
        private void SetupViewPager(ViewPager viewPager)
        {
            var adapter = new SettingsAdapter(SupportFragmentManager);
            adapter.AddFragment(new SettingsCardContentFragment(_settings), "Settings");
            viewPager.Adapter = adapter;
        }
    }
}