using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Weather.Android.AppServices;

namespace Weather.Android.Activities
{
    [Activity(Label = "Settings", Icon = "@drawable/settingsIcon")]
    public class SettingsActivity : Activity
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

            // Create your application here
            SetContentView(Resource.Layout.Settings);

            _toggleButtonEnableTestDrawer = FindViewById<ToggleButton>(Resource.Id.toggleButtonEnableTestDrawer);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _toggleButtonEnableTestDrawer.Checked = _settings.EnableTestDrawer;
            _toggleButtonEnableTestDrawer.CheckedChange += ToggleButtonEnableTestDrawer_CheckedChange;
        }

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