using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Experimental.SwipePage
{
    public class DummySectionFragment : Fragment
    {
        public static readonly string ARG_SECTION_NUMBER = "section_number";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(R.Layout.fragment_section_dummy, container, false);
            Bundle args = Arguments;
            rootView.FindViewById<TextView>(R.Id.text1).Text =  GetString(R.String.dummy_section_text, args.GetInt(ARG_SECTION_NUMBER));
            return rootView;
        }
    }
}