using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using R = Weather.Android.Resource;

namespace Weather.Android.Experimental.SwipePage
{
    public class LaunchpadSectionFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(R.Layout.fragment_section_launchpad, container, false);

            //rootView.FindViewById(Resource.Id.demo_collection_button).SetOnClickListener();
            //todo implement

            return rootView;
        }
    }
}