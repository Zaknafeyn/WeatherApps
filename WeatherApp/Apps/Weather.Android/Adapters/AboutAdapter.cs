using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;
using String = System.String;

namespace Weather.Android.Activities
{
    class AboutAdapter : FragmentPagerAdapter
    {
        private List<Fragment> mFragmentList = new List<Fragment>();
        private List<string> mFragmentTitleList = new List<string>();

        public void AddFragment(Fragment fragment, String title)
        {
            mFragmentList.Add(fragment);
            mFragmentTitleList.Add(title);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return mFragmentTitleList[position].ToCharSequence();
        }

        public AboutAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => mFragmentList.Count;

        public override Fragment GetItem(int position)
        {
            return mFragmentList[position];
        }
    }
}