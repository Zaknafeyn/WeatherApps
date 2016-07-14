using Android.Support.V7.Widget;
using Android.Widget;

namespace Weather.AndroidApp.Experimental
{
    public class ViewHolder : RecyclerView.ViewHolder
    {
        public readonly TextView _textView;
        public ViewHolder(TextView v) : base(v)
        {
            _textView = v;
        }

        public TextView TextView => _textView;
    }
}