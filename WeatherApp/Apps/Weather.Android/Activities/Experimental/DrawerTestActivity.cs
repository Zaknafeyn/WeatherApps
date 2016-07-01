using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Weather.Android.Experimental;

namespace Weather.Android.Activities
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

            SetContentView(Resource.Layout.DrawerTest);
            // Create your application here

            _titles = Resources.GetStringArray(Resource.Array.planets_array).ToList();

            _drawerList = FindViewById<RecyclerView>(Resource.Id.left_drawer);
            _drawerList.HasFixedSize = true;
            _drawerList.SetLayoutManager(new LinearLayoutManager(this));
            _drawerList.SetAdapter(new MenuAdapter(_titles.ToArray(), this));

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            _drawerToggle = new MyActionBarDrawerToggle(this, _drawerLayout,
                Resource.Drawable.ic_drawer,
                Resource.String.ApplicationName,
                Resource.String.Hello);

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
            ft.Replace(Resource.Id.content_frame, fragment);
            ft.Commit();

            // update selected item title, then close the drawer
            Title = _titles[position];
            //DrawerTitle = Title;
            _drawerLayout.CloseDrawer(_drawerList);
        }
    }
}