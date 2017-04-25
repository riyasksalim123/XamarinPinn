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
      //  public string ff;
        Position position ;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


             btn = (Button)FindViewById(Resource.Id.button1);
            btn.Click += delegate
            {
                var activity2 = new Intent(this, typeof(PointOfInterest));
                activity2.PutExtra("latitude", position.Latitude);
                activity2.PutExtra("longitude", position.Longitude);
                StartActivity(activity2);
                // StartActivity(typeof(vvvvvvvvv));
            };
            AlertCenter.Default.Init(Application);
            AlertCenter.Default.BackgroundColor = Android.Graphics.Color.Red;

            var progressDialog = ProgressDialog.Show(this, "Please wait...", "lOCATING PLACE...", true);
            new Thread(new ThreadStart(delegate
            {
                //LOAD METHOD TO GET ACCOUNT INFO
              //  RunOnUiThread(() => Toast.MakeText(this, "Toast within progress dialog.", ToastLength.Long).Show());
                //HIDE PROGRESS DIALOG
                //  RunOnUiThread(() => progressDialog.Hide());
            })).Start();

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

             position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);


            //AlertCenter.Default.PostMessage("Knock knock!", "Position Status: { 0} " + position.Timestamp + "", Resource.Drawable.Icon);
            AlertCenter.Default.PostMessage("Found Location", "Position Latitude: "+position.Latitude+" and Position Longitude : "+position.Longitude+"", Resource.Drawable.Icon);
            //AlertCenter.Default.PostMessage("Knock knock!", "Position Longitude: { 0} " + position.Longitude + "", Resource.Drawable.Icon);

            CrossTextToSpeech.Current.Speak("Your Current Latitude is " + position.Latitude);
            //var a = CrossTextToSpeech.Current.GetInstalledLanguages();
            progressDialog.Hide();

            // GeocodeData gdata = new GeocodeData();



            //  RetrieveFormatedAddress(position.Latitude.ToString(), position.Longitude.ToString(), gdata);

            // btn.Text = gdata.Address;


        }
       
        //public async void RetrieveFormatedAddress(string lat, string lng,GeocodeData g)
        //{

        //    string baseUri = "http://maps.googleapis.com/maps/api/" + "geocode/xml?latlng={0},{1}&sensor=false";

        //    string requestUri = string.Format(baseUri, lat, lng);

        //    using (WebClient wc = new WebClient())
        //    {
            
        //      wc.DownloadStringCompleted += ((s, e) => wc_DownloadStringCompleted(s, e,g));
        //        wc.DownloadStringAsync(new Uri(requestUri));

            
        //    }

        //}

        //public static string wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e,GeocodeData g)
        //{
        //    var xmlElm = System.Xml.Linq.XElement.Parse(e.Result);

        //    var status = (from elm in xmlElm.Descendants() where elm.Name == "status" select elm).FirstOrDefault();

        //    if (status.Value.ToLower() == "ok")
        //    {
        //      return  (from elm in xmlElm.Descendants() where elm.Name == "formatted_address" select elm.Value).FirstOrDefault();
            
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}



      


















        // https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=AIzaSyD-PXZ1TzmPOXTfZ-UaXT6lgi_kpMJmswg

        //   CrossExternalMaps.Current.NavigateTo("Xamarin", "394 pacific ave.", "San Francisco", "CA", "94111", "USA", "USA");
        // CrossExternalMaps.Current.NavigateTo("Space Needle", position.Latitude, position.Longitude);

        // Get the latitude and longitude entered by the user and create a query.
        //string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" + position.Latitude + "&lng=" + position.Longitude + "&username=demo";
      //  string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=AIzaSyD-PXZ1TzmPOXTfZ-UaXT6lgi_kpMJmswg";


        //XmlDocument xDoc = new XmlDocument();
        //xDoc.Load(url);

        //XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
        //XmlNode xNode = xNodelst.Item(0);
        //string adress = xNode.SelectSingleNode("formatted_address").InnerText;
        //string mahalle = xNode.SelectSingleNode("address_component[3]/long_name").InnerText;
        //string ilce = xNode.SelectSingleNode("address_component[4]/long_name").InnerText;
        //string il = xNode.SelectSingleNode("address_component[5]/long_name").InnerText;

        //// Fetch the weather information asynchronously, 
        //// parse the results, then update the screen:
        //JsonValue json = await FetchWeatherAsync(url);
        // ParseAndDisplay (json);
        //JSONObject jsona = new JSONObject(json);

        //Rootobject r = new Rootobject();



        //foreach (var dataItem in data)
        //{
        //    string myValue = dataItem["myKey"];
        //    //...
        //}

    }


    //public class GeocodeData
    //{
    //    public string Address { get; set; }
    //}
    // Gets weather data from the passed URL.
    //private async Task<JsonValue> FetchWeatherAsync(string url)
    //{
    //    // Create an HTTP web request using the URL:
    //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
    //    request.ContentType = "application/json";
    //    request.Method = "GET";

    //    // Send the request to the server and wait for the response:
    //    using (WebResponse response = await request.GetResponseAsync())
    //    {
    //        // Get a stream representation of the HTTP web response:
    //        using (Stream stream = response.GetResponseStream())
    //        {
    //            // Use this stream to build a JSON document object:
    //            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
    //            try
    //            {


    //                Rootobject deserializedProduct = JsonConvert.DeserializeObject<Rootobject>(jsonDoc);
    //            }
    //            catch (Exception e)
    //            {

    //                throw;
    //            }


    //            // Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

    //            // Return the JSON document:
    //            return jsonDoc;
    //        }
    //    }
    //}


    //private void ParseAndDisplay(JsonValue json)
    //{

    //    var weatherResults1 = json["formatted_address"];
    //    // Extract the array of name/value results for the field name "weatherObservation". 
    //    JsonValue weatherResults = json["formatted_address"];

    //    // Extract the "stationName" (location string) and write it to the location TextBox:
    //   var station = weatherResults["stationName"];


    //    double humidPercent = weatherResults["humidity"];


    //    // Get the "clouds" and "weatherConditions" strings and 
    //    // combine them. Ignore strings that are reported as "n/a":
    //    string cloudy = weatherResults["clouds"];
    //    if (cloudy.Equals("n/a"))
    //        cloudy = "";
    //    string cond = weatherResults["weatherCondition"];
    //    if (cond.Equals("n/a"))
    //        cond = "";

    //    // Write the result to the conditions TextBox:

    //}

}




