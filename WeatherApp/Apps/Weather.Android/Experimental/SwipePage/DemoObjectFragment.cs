using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using R = Weather.AndroidApp.Resource;

namespace Weather.AndroidApp.Experimental.SwipePage
{
    public class DemoObjectFragment : Fragment
    {
        public static string ArgObject = "object";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // The last two arguments ensure LayoutParams are inflated
            // properly.
            var rootView = inflater.Inflate(R.Layout.fragment_collection_object, container, false);
            Bundle args = Arguments;
            ((TextView)rootView.FindViewById(R.Id.text1)).Text = args.GetString(ArgObject);
            return rootView;
        }
    }
}