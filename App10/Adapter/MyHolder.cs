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

namespace App10.Adapter
{
    class MyHolder
    {
        public TextView Nametxt;
        public ImageView Img;


        public MyHolder(View itemview)
        {
            Nametxt = itemview.FindViewById<TextView>(Resource.Id.ContactName);
            Img = itemview.FindViewById<ImageView>(Resource.Id.ContactImage);
        }
    }
}