using Android.Views;

namespace Weather.AndroidApp.Experimental
{
    public interface IItemClickListener
    {
        void OnClick(View view, int position);
    }
}