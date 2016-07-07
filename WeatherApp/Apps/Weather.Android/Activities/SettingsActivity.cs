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
using Android.Support.V7.Widget;
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