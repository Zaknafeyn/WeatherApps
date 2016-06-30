using Android.Views;

namespace Weather.Android.Experimental
{
    public interface IItemClickListener
    {
        void OnClick(View view, int position);
    }
}