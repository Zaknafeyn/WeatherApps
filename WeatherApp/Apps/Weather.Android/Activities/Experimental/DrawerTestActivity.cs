using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Weather.AndroidApp.Experimental;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Activities.Experimental
{
    [Activity(Label = "Weather - test drawable", Icon = "@drawable/icon")]
    public class DrawerTestActivity : Activity, IItemClickListener
    {
        private MyActionBarDrawerToggle _drawerToggle;
        private DrawerLayout _drawerLayout;
        private RecyclerView _drawerList;
        private List<string> _titles;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(R.Layout.DrawerTest);
            // Create your application here

            _titles = Resources.GetStringArray(R.Array.planets_array).ToList();

            _drawerList = FindViewById<RecyclerView>(R.Id.left_drawer);
            _drawerList.HasFixedSize = true;
            _drawerList.SetLayoutManager(new LinearLayoutManager(this));
            _drawerList.SetAdapter(new MenuAdapter(_titles.ToArray(), this));

            _drawerLayout = FindViewById<DrawerLayout>(R.Id.drawer_layout);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            _drawerToggle = new MyActionBarDrawerToggle(this, _drawerLayout,
                R.Drawable.ic_drawer,
                R.String.ApplicationName,
                R.String.Hello);

            _drawerLayout?.AddDrawerListener(_drawerToggle); 
            //.SetDrawerListener(_drawerToggle);
            if (savedInstanceState == null) //first launch
                SelectContentItem(0);
        }

        public string DrawerTitle { get; private set; }

        public void OnClick(View view, int position)
        {
            Toast.MakeText(ApplicationContext, $"Position {position}", ToastLength.Long);
        }

        private void SelectContentItem(int position)
        {
            // update the main content by replacing fragments
            var fragment = MenuFragment.NewInstance(position);

            var fragmentManager = FragmentManager;
            var ft = fragmentManager.BeginTransaction();
            ft.Replace(R.Id.content_frame, fragment);
            ft.Commit();

            // update selected item title, then close the drawer
            Title = _titles[position];
            //DrawerTitle = Title;
            _drawerLayout.CloseDrawer(_drawerList);
        }
    }
}