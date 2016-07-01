using Android.App;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Weather.Android.Experimental.SwipePage;
using FragmentTransaction = Android.App.FragmentTransaction;

namespace Weather.Android.Activities
{
    public partial class MainActivity : ActionBar.ITabListener
    {
        private AppSectionsPagerAdapter mAppSectionsPagerAdapter;
        private ViewPager mViewPager;

        private void InitSwipePager()
        {
            mAppSectionsPagerAdapter = new AppSectionsPagerAdapter(this.SupportFragmentManager);
            var actionBar = ActionBar;
            actionBar.SetHomeButtonEnabled(false);
            // Specify that we will be displaying tabs in the action bar.
            actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            // Set up the ViewPager, attaching the adapter and setting up a listener for when the
            // user swipes between sections.
            mViewPager = FindViewById<ViewPager>(Resource.Id.pager);
            mViewPager.Adapter = mAppSectionsPagerAdapter;

            mViewPager.AddOnPageChangeListener(new PageChangeListener(actionBar));

            // For each of the sections in the app, add a tab to the action bar.
            for (int i = 0; i < mAppSectionsPagerAdapter.Count; i++)
            {
                // Create a tab with text corresponding to the page title defined by the adapter.
                // Also specify this Activity object, which implements the TabListener interface, as the
                // listener for when this tab is selected.
                actionBar.AddTab(
                        actionBar.NewTab()
                                .SetText(mAppSectionsPagerAdapter.GetPageTitle(i))
                                .SetTabListener(this));
            }
        }

        class PageChangeListener : ViewPager.SimpleOnPageChangeListener
        {
            private readonly ActionBar _actionBar;

            public PageChangeListener(ActionBar actionBar)
            {
                _actionBar = actionBar;
            }

            public override void OnPageSelected(int position)
            {
                _actionBar.SetSelectedNavigationItem(position);
            }
        }

        public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            return;
            throw new System.NotImplementedException();
        }

        public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            mViewPager.SetCurrentItem(tab.Position, true);
        }

        public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            return;
            throw new System.NotImplementedException();
        }
    }
}