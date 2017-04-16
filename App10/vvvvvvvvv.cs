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
using Android.Locations;
using Android.Util;
using System.Threading.Tasks;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;
using RestSharp;
using RestSharp.Extensions;
using System.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using App10.Adapter;

namespace App10
{
    [Activity(Label = "vvvvvvvvv")]
    public class vvvvvvvvv : Activity
    {

        static readonly string TAG = "X:" + typeof(vvvvvvvvv).Name;
        TextView _addressText;
        Location _currentLocation;
        LocationManager _locationManager;
        Position p;
        double lat;
        double longi;
        string _locationProvider;
        TextView _locationText;
        LocationManager locMgr;
        string locationProvider;



      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            AppDomain.CurrentDomain.UnhandledException += ErrorHandler.CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += ErrorHandler.TaskSchedulerOnUnobservedTaskException;
            lat = Intent.GetDoubleExtra("latitude", 0.0);
            longi = Intent.GetDoubleExtra("longitude", 0.0);
            _addressText = FindViewById<TextView>(Resource.Id.address_text);
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
            FindViewById<TextView>(Resource.Id.get_address_button).Click += AddressButton_OnClick;
            FindViewById<TextView>(Resource.Id.get_POI).Click += Vvvvvvvvv_Click;

        }

        private async void Vvvvvvvvv_Click(object sender, EventArgs e)
        {
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+lat+","+ longi+"&types=point_of_interest&radius=50000&sensor=false&key=AIzaSyCrMlsuYQlYLpiwHDes4gsxJlu2IcoyJ7w";


            JsonValue json = await FetchWeatherAsync(url);
            ParseAndDisplay(json);
        }

        private async Task<string> FetchWeatherAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {

                var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Rootobject Data;
                // var json = JObject.Parse(rawJson);  //Turns your raw string into a key value lookup
                try {

                     Data = JsonConvert.DeserializeObject<Rootobject>(rawJson);

                    var contactsAdapter = new StreamListAdapter(this, Data.results);
                    var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
                    contactsListView.Adapter = contactsAdapter;


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                
                
                // string licsene_value = json["results"].ToObject<string>();

              
                //List<Result> Res = new List<Result>();


                //Res = Data.results.ToList();


             

                return "";
            
            }
        }

        private void ParseAndDisplay(JsonValue json)
        {

            TextView poi = FindViewById<TextView>(Resource.Id.poi);

            JsonValue weatherResults = json["weatherObservation"];

            poi.Text = weatherResults["stationName"];


        }

        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList = await geocoder.GetFromLocationAsync(lat, longi, 10);
            Address address = addressList.FirstOrDefault();

            DisplayAddress(address);
        }



        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                _addressText.Text = deviceAddress.ToString();

            }
            else
            {
                _addressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }


    }


}



public class Rootobject
{
    public object[] html_attributions { get; set; }
    public string next_page_token { get; set; }
    public Result[] results { get; set; }
    public string status { get; set; }
}

public class Result
{
    public Geometry geometry { get; set; }
    public string icon { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public Photo[] photos { get; set; }
    public string place_id { get; set; }
    public float rating { get; set; }
    public string reference { get; set; }
    public string scope { get; set; }
    public string[] types { get; set; }
    public string vicinity { get; set; }
    public Opening_Hours opening_hours { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public Viewport viewport { get; set; }
}

public class Location
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Northeast
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Southwest
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Opening_Hours
{
    public bool open_now { get; set; }
    public object[] weekday_text { get; set; }
}

public class Photo
{
    public int height { get; set; }
    public string[] html_attributions { get; set; }
    public string photo_reference { get; set; }
    public int width { get; set; }
}
