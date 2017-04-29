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
using Xamarin.Auth;
using Newtonsoft.Json;
using App10.Adapter;

namespace App10
{
    [Activity(Label = "FacebookActivity")]
    public class FacebookActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FacebookDetails);
            // Create your application here

            var auth = new OAuth2Authenticator(
                    clientId: "168801230313991",
                    scope: "",
                    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                    redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += Auth_Completed;
            var UI = auth.GetUI(this);
            StartActivity(UI);
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                try
                {
                    var request = new OAuth2Request("GET", new System.Uri("https://graph.facebook.com/me?fields=id,name,birthday,about,cover,devices,education,email,first_name,gender,favorite_athletes,favorite_teams,inspirational_people,interested_in,hometown"), null, e.Account);
                    var FbResponse = await request.GetResponseAsync();
                    var json = FbResponse.GetResponseText();

                    var FbUser = JsonConvert.DeserializeObject<FaceBookUser>(json);
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }
    }
}