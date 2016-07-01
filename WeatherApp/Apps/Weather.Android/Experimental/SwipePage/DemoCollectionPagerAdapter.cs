using System;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;

namespace Weather.Android.Experimental.SwipePage
{
    public class DemoCollectionPagerAdapter : FragmentStatePagerAdapter
    {
        public DemoCollectionPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public DemoCollectionPagerAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => 100;

        public override Fragment GetItem(int position)
        {
            var fragment = new DemoObjectFragment();
            Bundle args = new Bundle();
            // Our object is just an integer :-P
            args.PutString(DemoObjectFragment.ArgObject, $"{position + 1}");
            fragment.Arguments = args;
            return fragment;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return $"Page title - {position}".ToCharSequence();
        }
    }
}