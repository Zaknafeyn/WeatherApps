using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Weather.AndroidApp.Adapters;
using Weather.AndroidApp.Fragments;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Activities
{
    [Activity(Label = "About", Theme = "@style/MyTheme")]
    public class AboutActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            Init();
            return;

            SetContentView(R.Layout.about_material);

            //var version = FindViewById<TextView>(Resource.Id.about_textViewVersion);

            //var version = FindViewById<TextView>(Resource.Id.tile_text);

            //version.Text = $"Version: {GetAppVersionName()}";
        }

        private void Init()
        {
            SetContentView(R.Layout.about_material);

            var toolbar = FindViewById<Toolbar>(R.Id.toolbar);
            SetSupportActionBar(toolbar);
            // Setting ViewPager for each Tabs
            var viewPager = FindViewById<ViewPager>(R.Id.viewpager);
            SetupViewPager(viewPager);
            // Set Tabs inside Toolbar
            var tabs = FindViewById<TabLayout>(R.Id.tabs);
            tabs.SetupWithViewPager(viewPager);
        }

        // Add Fragments to Tabs
        private void SetupViewPager(ViewPager viewPager)
        {
            var adapter = new AboutAdapter(SupportFragmentManager);
            adapter.AddFragment(new ListContentFragment(), "List");
            adapter.AddFragment(new TitleContentFragment(), "Tile");
            adapter.AddFragment(new CardContentFragment(), "Card");
            viewPager.Adapter = adapter;
        }

        private string GetAppVersionName()
        {
            var pInfo = PackageManager.GetPackageInfo(PackageName, 0);
            var versionName = pInfo.VersionName;
            return versionName;
        }
    }
}