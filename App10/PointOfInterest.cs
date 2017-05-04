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
using System.Threading;
using Android.Views.Animations;

namespace App10
{
    [Activity(Label = "Point of Interests")]
    public class PointOfInterest : Activity
    {

     
        TextView _addressText;
        TextView poi;
       
        Button get_address_button;
        Button get_POI;
     
        double lat;
        double longi;
      
        TextView _locationText;
        // LocationManager locMgr;
        // string locationProvider;
        private ProgressBar mProgressBar;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.POI);
        
            lat = Intent.GetDoubleExtra("latitude", 0.0);
            longi = Intent.GetDoubleExtra("longitude", 0.0);
            _addressText = FindViewById<TextView>(Resource.Id.address_text);
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
            poi = FindViewById<TextView>(Resource.Id.poi);
            get_address_button = FindViewById<Button>(Resource.Id.get_address_button);

            get_POI = FindViewById<Button>(Resource.Id.get_POI);
            FindViewById<TextView>(Resource.Id.get_address_button).Click += AddressButton_OnClick;
            FindViewById<TextView>(Resource.Id.get_POI).Click += Vvvvvvvvv_Click;
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);


        }

        private async void Vvvvvvvvv_Click(object sender, EventArgs e)
        {
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+lat+","+ longi+"&types=point_of_interest&radius=100000&sensor=false&key=AIzaSyCrMlsuYQlYLpiwHDes4gsxJlu2IcoyJ7w";


            AlphaAnimation AlphaAnimation = new AlphaAnimation(0f,1f);
            AlphaAnimation.Duration = 200;
            mProgressBar.Animation = AlphaAnimation;
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;


            //var progressDialog = ProgressDialog.Show(this, "Please wait...", "Checking POI of the Location", true);
            //new Thread(new ThreadStart(delegate
            //{
               
            //})).Start();
            Rootobject1 Data = await FetchWeatherAsync(url);

           

            var contactsAdapter = new StreamListAdapter(this, Data.results);
            var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
            contactsListView.Adapter = contactsAdapter;
            // progressDialog.Hide();
            AlphaAnimation betaAnimation = new AlphaAnimation(0f, 1f);
            betaAnimation.Duration = 200;
            mProgressBar.Animation = betaAnimation;
            mProgressBar.Visibility = Android.Views.ViewStates.Gone;

            _addressText.Visibility = ViewStates.Gone;
            _locationText.Visibility = ViewStates.Gone;
            get_address_button.Visibility= ViewStates.Gone;
            get_POI.Visibility = ViewStates.Gone;
            poi.Visibility = ViewStates.Gone;

          
        }

        private async Task<Rootobject1> FetchWeatherAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {

                var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Rootobject1 Data;
                // var json = JObject.Parse(rawJson);  //Turns your raw string into a key value lookup
                
                     Data = JsonConvert.DeserializeObject<Rootobject1>(rawJson);

                    return Data;
                   
            
            }
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



public class Rootobject1
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
