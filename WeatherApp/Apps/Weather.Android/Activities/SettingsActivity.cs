using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Weather.Android.Activities.Experimental;
using Weather.Android.AppServices;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using R = Weather.Android.Resource;
using String = System.String;

namespace Weather.Android.Activities
{
    [Activity(Label = "Settings", Icon = "@drawable/ic_settings_black_24dp", Theme = "@style/MyTheme")]
    public class SettingsActivity : AppCompatActivity
    {
        private ISettings _settings;
        private ToggleButton _toggleButtonEnableTestDrawer;

        public SettingsActivity()
        {
            var locator = ServiceLocator.Current;

            _settings = locator.GetInstance<ISettings>();
            _settings.ReadSettings();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Init();
            return;
            // Create your application here
            SetContentView(Resource.Layout.Settings);

            _toggleButtonEnableTestDrawer = FindViewById<ToggleButton>(Resource.Id.toggleButtonEnableTestDrawer);
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
            adapter.AddFragment(new SettingsCardContentFragment(), "Settings");
            viewPager.Adapter = adapter;
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();

        //    _toggleButtonEnableTestDrawer.Checked = _settings.EnableTestDrawer;
        //    _toggleButtonEnableTestDrawer.CheckedChange += ToggleButtonEnableTestDrawer_CheckedChange;
        //}

        private void ToggleButtonEnableTestDrawer_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            _settings.EnableTestDrawer = e.IsChecked;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _settings.SaveSettings();
            _toggleButtonEnableTestDrawer.CheckedChange -= ToggleButtonEnableTestDrawer_CheckedChange;
        }
    }
}