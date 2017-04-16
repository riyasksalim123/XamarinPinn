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
using System.Collections;

namespace App10.Adapter
{
    class CustomAdapter : ArrayAdapter
    {
        private Context c;
        private List<Player> players;
        private int Resource;
        LayoutInflater inflator;
        public CustomAdapter(Context context,int resource,List<Player> objects) : base(context, resource, objects)
        {
            this.c = context;
            this.Resource = resource;
            this.players = objects;

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflator == null)
            {
                inflator = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            }

            if (convertView == null)
            {
                convertView = inflator.Inflate(Resource, parent, false);

            }

            MyHolder Myholder = new MyHolder(convertView);
            Myholder.Nametxt.Text = players[position].Name;
            Myholder.Img.SetImageResource(players[position].Image);


            convertView.SetBackgroundColor(Android.Graphics.Color.Azure);

            return base.GetView(position, convertView, parent);
        }
    }
}