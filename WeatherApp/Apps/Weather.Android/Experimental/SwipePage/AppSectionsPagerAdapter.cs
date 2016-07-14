using System;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;

namespace Weather.AndroidApp.Experimental.SwipePage
{
    public class AppSectionsPagerAdapter : FragmentPagerAdapter
    {
        public AppSectionsPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public AppSectionsPagerAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => 3;

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    // The first section of the app is the most interesting -- it offers
                    // a launchpad into the other demonstrations in this example application.
                    return new LaunchpadSectionFragment();

                default:
                    // The other sections of the app are dummy placeholders.
                    var fragment = new DummySectionFragment();
                    Bundle args = new Bundle();
                    args.PutInt(DummySectionFragment.ARG_SECTION_NUMBER, position + 1);
                    fragment.Arguments = args;
                    return fragment;
            }
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return $"Section {position + 1}".ToCharSequence();
        }
    }
}