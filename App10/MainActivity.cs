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
using Com.Squareup.Picasso;

namespace App10
{
    [Activity(Label = "", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btn;
        Button GoogleButton;
        ImageButton FbBtn;
        Button ClarfiaiBtn;
        Position position ;
        Button CustomListbtn;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.imageView);
            Picasso.With(this).Load("http://i.imgur.com/DvpvklR.jpg").Into(imageView);


            GoogleButton = (Button)FindViewById(Resource.Id.Autocomplete);
            btn = (Button)FindViewById(Resource.Id.button1);
            FbBtn = (ImageButton)FindViewById(Resource.Id.FacebookButton);
            ClarfiaiBtn = (Button)FindViewById(Resource.Id.Clarifaibtn);
            CustomListbtn=(Button)FindViewById(Resource.Id.customlist);
            CustomListbtn.Click += CustomListbtn_Click;
            ClarfiaiBtn.Click += ClarfiaiBtn_Click;
            FbBtn.Click += FbBtn_Click;
            btn.Click += delegate
            {
                var activity2 = new Intent(this, typeof(PointOfInterest));
                activity2.PutExtra("latitude", position.Latitude);
                activity2.PutExtra("longitude", position.Longitude);
                StartActivity(activity2);
            };
            GoogleButton.Click += GoogleButton_Click;
            AlertCenter.Default.Init(Application);
            AlertCenter.Default.BackgroundColor = Android.Graphics.Color.Red;

            var progressDialog = ProgressDialog.Show(this, "Please wait...", "lOCATING PLACE...", true);
            new Thread(new ThreadStart(delegate
            {
             
            })).Start();

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

             position = await locator.GetPositionAsync(timeoutMilliseconds: 1000000000);


           AlertCenter.Default.PostMessage("Found Location", "Position Latitude: "+position.Latitude+" and Position Longitude : "+position.Longitude+"", Resource.Drawable.abc_btn_check_material);

            CrossTextToSpeech.Current.Speak("Your Current Latitude is " + position.Latitude);
            progressDialog.Hide();

        }

        private void CustomListbtn_Click(object sender, EventArgs e)
        {
            var activity2 = new Intent(this, typeof(CustomListActivity));
            activity2.PutExtra("latitude", position.Latitude);
            activity2.PutExtra("longitude", position.Longitude);
            //activity2.PutExtra("latitude", 51.5033640);
            //activity2.PutExtra("longitude", -0.1276250);
            StartActivity(activity2);
        }

        private void ClarfiaiBtn_Click(object sender, EventArgs e)
        {
            var activity2 = new Intent(this, typeof(ClarifaiActivity));

            StartActivity(activity2);
        }

        private void GoogleButton_Click(object sender, EventArgs e)
        {
            var activity2 = new Intent(this, typeof(GoogleAutoCompleate));
          
            StartActivity(activity2);
        }

        private void FbBtn_Click(object sender, EventArgs e)
        {
            var FacebookActivity = new Intent(this, typeof(FacebookActivity));
           
            StartActivity(FacebookActivity);

        }
      
    }

}




