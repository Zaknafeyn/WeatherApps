using Android.App;
using Android.OS;
using Android.Widget;

namespace Weather.Android.Activities
{
    [Activity(Label = "About")]
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