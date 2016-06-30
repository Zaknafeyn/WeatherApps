using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Weather.Android.Experimental
{
    public class MenuAdapter : RecyclerView.Adapter
    {
        private string[] _dataset;
        private IItemClickListener _listener;

        public MenuAdapter(string[] myDataSet, IItemClickListener listener)
        {
            _dataset = myDataSet;
            _listener = listener;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var vi = LayoutInflater.From(parent.Context);
            var v = vi.Inflate(Resource.Layout.drawer_list_item, parent, false);
            var tv = v.FindViewById<TextView>(Resource.Id.text1);
            return new ViewHolder(tv);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holderRaw, int position)
        {
            var holder = (ViewHolder)holderRaw;
            holder.TextView.Text = _dataset[position];
            holder.TextView.Click += (sender, args) => {
                _listener.OnClick((View)sender, position);
            };
        }

        public override int ItemCount => _dataset.Length;
    }
}