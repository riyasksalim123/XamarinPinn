using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Com.Squareup.Picasso;

namespace App10.Adapter
{
    public class HotDogAdapter : BaseAdapter<Result>
    {

        Activity context;
        Result[] items1;
        public HotDogAdapter(Activity context, Result[] items) : base()
        {
            this.context = context;
            this.items1 = items;
        }
        public override Result this[int position]
        {
            get { return items1[position]; }
        }

        public override int Count
        {
            get { return items1.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }




        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var item = items1[position];

            if (convertView == null)
            {
                convertView=context.LayoutInflater.Inflate(Resource.Layout.ColorfulLIst, null);
            }

           convertView.FindViewById<TextView>(Resource.Id.txt1).Text = item.name;
            convertView.FindViewById<TextView>(Resource.Id.txt2).Text = item.vicinity;
            convertView.FindViewById<ImageView>(Resource.Id.imageView1);

         
            //  convertView.FindViewById<TextView>(Resource.Id.txt3).Text = item.opening_hours.open_now.ToString();


            //convertView.FindViewById<ImageView>(Android.Resource.Id.Icon).SetImageBitmap();

            return convertView;
           
        }
    }
}