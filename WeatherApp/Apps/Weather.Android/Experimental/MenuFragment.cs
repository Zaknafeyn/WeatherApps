using Android.App;
using Android.OS;

namespace Weather.Android.Experimental
{
    public class MenuFragment : Fragment
    {
        public const string ARG_PLANET_NUMBER = "planet_number";

        public MenuFragment()
        {
        }

        public static Fragment NewInstance(int position)
        {
            Fragment fragment = new MenuFragment();
            Bundle args = new Bundle();
            args.PutInt(MenuFragment.ARG_PLANET_NUMBER, position);
            fragment.Arguments = args;
            return fragment;
        }


        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
        //                                       Bundle savedInstanceState)
        //{
        //    //View rootView = inflater.Inflate(Resource.Layout.fragment_planet, container, false);
        //    //var i = this.Arguments.GetInt(ARG_PLANET_NUMBER);
        //    //var planet = this.Resources.GetStringArray(Resource.Array.planets_array)[i];
        //    //var imgId = this.Resources.GetIdentifier(planet.ToLower(),
        //    //                "drawable", this.Activity.PackageName);
        //    //var iv = rootView.FindViewById<ImageView>(Resource.Id.image);
        //    //iv.SetImageResource(imgId);
        //    //this.Activity.Title = planet;
        //    //return rootView;
        //}
    }
}