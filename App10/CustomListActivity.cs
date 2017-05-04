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
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using App10.Adapter;

namespace App10
{
    [Activity(Label = "CustomListActivity")]
    public class CustomListActivity : Activity
    {
        ListView lwhotdog;
        PointOfInterest poi = new PointOfInterest();
        double lat;
        double longi;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogListView);
            lat = Intent.GetDoubleExtra("latitude", 0.0);
            longi = Intent.GetDoubleExtra("longitude", 0.0);
            lwhotdog = FindViewById<ListView>(Resource.Id.hotDogListView);
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lat + "," + longi + "&types=point_of_interest&radius=100000&sensor=false&key=AIzaSyCrMlsuYQlYLpiwHDes4gsxJlu2IcoyJ7w";
            Rootobject1 Data =  FetchWeatherAsync(url);
            lwhotdog.Adapter = new HotDogAdapter(this,Data.results);
            lwhotdog.FastScrollEnabled = true; 


        }


        private  Rootobject1 FetchWeatherAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response =  request.GetResponse())
            {

                var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Rootobject1 Data;
               

                Data = JsonConvert.DeserializeObject<Rootobject1>(rawJson);

                return Data;


            }
        }
    }
}