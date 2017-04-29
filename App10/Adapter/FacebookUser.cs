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

    public class School
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class With
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Education
    {
        public School school { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public List<With> with { get; set; }
    }

    public class FavoriteAthlete
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Hometown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class FaceBookUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
        public Cover cover { get; set; }
        public List<Device> devices { get; set; }
        public List<Education> education { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string gender { get; set; }
        public List<FavoriteAthlete> favorite_athletes { get; set; }
        public List<string> interested_in { get; set; }
        public Hometown hometown { get; set; }
    }
}