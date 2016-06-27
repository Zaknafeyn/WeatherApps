using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Weather.Android
{
    [Activity(Label = "AboutActivity")]
    public class AboutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.About);

            var version = FindViewById<TextView>(Resource.Id.about_textViewVersion);
            version.Text = $"Version: {GetAppVersionName()}";
        }

        private string GetAppVersionName()
        {
            var pInfo = PackageManager.GetPackageInfo(PackageName, 0);
            var versionName = pInfo.VersionName;
            return versionName;
        }
    }
}