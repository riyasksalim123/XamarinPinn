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

    public class Cover
    {
        public string id { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
    }

    public class Device
    {
        public string os { get; set; }
    }

    public class FavoriteAthlete
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Datum
    {
        public string name { get; set; }
        public string id { get; set; }
        public string created_time { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Likes
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Summary
    {
        public int total_count { get; set; }
    }

    public class Friends
    {
        public List<object> data { get; set; }
        public Summary summary { get; set; }
    }

    public class FaceBookUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public Cover cover { get; set; }
        public List<Device> devices { get; set; }
        public string first_name { get; set; }
        public string gender { get; set; }
        public List<FavoriteAthlete> favorite_athletes { get; set; }
        public Likes likes { get; set; }
        public Friends friends { get; set; }
    }
}