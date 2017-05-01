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
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App10
{
    [Activity(Label = "ClarifaiActivity")]
    public class ClarifaiActivity : Activity
    {
        Button btngetclarfai;
        // string remoteUri = Console.ReadLine();
        public string Url = "";
        // make a webclient 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Clarifai);
            // btngetclarfai = (Button)FindViewById(Resource.Id.Clarifaibtn);
            //btngetclarfai = (Button)FindViewById(Resource.Id.Clarifaibtn);
            //btngetclarfai.Click += Btngetclarfai_Click;

            FindViewById<Button>(Resource.Id.getclarfai).Click += Vvvvvvvvv_Click;
        }

        private async void Vvvvvvvvv_Click(object sender, EventArgs e)
        {
            string nudeWoman = "https://i.imgur.com/aJXq544.jpg";
            string token = "qp1UNqKgJ4QjYRdOYukNc7cjGquZx9";
            string url = "http://api.clarifai.com/v1/tag/?&url=" + nudeWoman + "&access_token=" + token;
            var Data = await FetchData(url);

        }

       

     
        private async Task<RootObject1> FetchData(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                // request.ContentType = "application/json";
                request.Method = "GET";

                using (WebResponse response = await request.GetResponseAsync())
                {

                    var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    RootObject1 Data;
                    // var json = JObject.Parse(rawJson);  //Turns your raw string into a key value lookup

                  //  Data = JsonConvert.DeserializeObject<Rootobject1>(rawJson);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            

            return null;


            }
        }


    public class Tag
    {
        public double timestamp { get; set; }
        public string model { get; set; }
        public object config { get; set; }
    }

    public class RootObject1
    {
        public Tag tag { get; set; }
    }
}
