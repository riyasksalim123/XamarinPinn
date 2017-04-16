using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace App10.Adapter
{
   public class StreamListAdapter : BaseAdapter<Result>
    {
        Result[] items;
        Activity context;
        public StreamListAdapter(Activity context, Result[] items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Result this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Length; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                View view = convertView; // re-use an existing view, if one is available
                if (view == null) // otherwise create a new one
                    view = context.LayoutInflater.Inflate(Resource.Layout.ContactListItem, null);

                view.FindViewById<TextView>(Resource.Id.ContactName).Text = items[position].name;

              //  view.FindViewById<TextView>(Resource.Id.location_text).Text = items[position].vicinity;

                var imageView = view.FindViewById<ImageView>(Resource.Id.ContactImage);

                //int resourceId = (int)typeof(Resource.Drawable).GetField("icon").GetVa‌​lue(null);
                //imageView.SetImageResource(resourceId);
                Android.Net.Uri url = Android.Net.Uri.Parse(items[position].icon);
                imageView.SetImageURI(url);


                return view;
            }
            catch (Exception ex)
            {

                throw ex;
               // return "";
            }
           
        }
    }


}