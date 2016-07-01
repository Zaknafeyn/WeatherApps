using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Weather.Android.Experimental.SwipePage;

namespace Weather.Android.Activities.Experimental
{
    public class CollectionDemoActivity : FragmentActivity
    {
        DemoCollectionPagerAdapter mDemoCollectionPagerAdapter;
        ViewPager mViewPager;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.activity_collection_demo);

            // ViewPager and its adapters use support library
            // fragments, so use getSupportFragmentManager.
            mDemoCollectionPagerAdapter = new DemoCollectionPagerAdapter(SupportFragmentManager);
            //mViewPager = FindViewById< ViewPager>(Resource.Id.pager);
            mViewPager.Adapter = mDemoCollectionPagerAdapter;
        }

        
    }
}