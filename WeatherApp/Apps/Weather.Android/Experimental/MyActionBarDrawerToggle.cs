using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Weather.Android.Activities;
using Weather.Android.Activities.Experimental;

namespace Weather.Android.Experimental
{
    internal class MyActionBarDrawerToggle : ActionBarDrawerToggle
    {
        DrawerTestActivity owner;

        public MyActionBarDrawerToggle(DrawerTestActivity activity, DrawerLayout layout, int imgRes, int openRes, int closeRes)
            : base(activity, layout, imgRes, openRes, closeRes)
        {
            owner = activity;
        }

        public override void OnDrawerClosed(View drawerView)
        {
            owner.ActionBar.Title = owner.Title;
            owner.InvalidateOptionsMenu();
        }

        public override void OnDrawerOpened(View drawerView)
        {
            owner.ActionBar.Title = owner.Title;
            owner.InvalidateOptionsMenu();
        }
    }
}