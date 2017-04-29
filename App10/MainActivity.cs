using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Controls;
using Java.IO;
using Geolocator.Plugin;
using ExternalMaps.Plugin;
using Refractored.Xam.TTS;
using System.Net;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Json;
using Org.Json;
using Newtonsoft.Json;
using System.Xml;
using System.Linq;
using Android.Views;
using Android.Content;
using System.Net.Http;
using Geolocator.Plugin.Abstractions;
using RestSharp;
using System.Threading;

namespace App10
{
    [Activity(Label = "App10", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btn;
        ImageButton FbBtn;
     
        Position position ;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            btn = (Button)FindViewById(Resource.Id.button1);
            FbBtn = (ImageButton)FindViewById(Resource.Id.FacebookButton);
            FbBtn.Click += FbBtn_Click;
            btn.Click += delegate
            {
                var activity2 = new Intent(this, typeof(PointOfInterest));
                activity2.PutExtra("latitude", position.Latitude);
                activity2.PutExtra("longitude", position.Longitude);
                StartActivity(activity2);
            };
            AlertCenter.Default.Init(Application);
            AlertCenter.Default.BackgroundColor = Android.Graphics.Color.Red;

            var progressDialog = ProgressDialog.Show(this, "Please wait...", "lOCATING PLACE...", true);
            new Thread(new ThreadStart(delegate
            {
             
            })).Start();

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

             position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);


            AlertCenter.Default.PostMessage("Found Location", "Position Latitude: "+position.Latitude+" and Position Longitude : "+position.Longitude+"", Resource.Drawable.Icon);

            CrossTextToSpeech.Current.Speak("Your Current Latitude is " + position.Latitude);
            progressDialog.Hide();

        }

        private void FbBtn_Click(object sender, EventArgs e)
        {
            var FacebookActivity = new Intent(this, typeof(FacebookActivity));
           
            StartActivity(FacebookActivity);

        }
      
    }

}




