using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using FIFTTOW.Models;

namespace FIFTTOW.UI
{
    public class WifiLocationAdapter : BaseAdapter<WifiLocation>
    {
        private List<WifiLocation> _items;
        private Activity _activity;

        public WifiLocationAdapter(Activity activity, List<WifiLocation> items)
        {
            _activity = activity;
            _items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _items[position].DisplayName;
            return view;
        }

        public override int Count => _items.Count;
        public override WifiLocation this[int position] => _items[position];
    }
}