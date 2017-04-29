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
            var scopes= "email,user_hometown,user_religion_politics,publish_actions,user_likes"+
"user_status"+
"user_about_me" +
"user_location" +
"user_tagged_places" +
"user_birthday" +
"user_photos" +
"user_videos" +
"user_education_history" +
"user_posts" +
"user_website" +
"user_friends" +
"user_relationship_details" +
"user_work_history" +
"user_games_activity" +
"user_relationships" +
"Events, Groups&Pages" +
"ads_management" +
"pages_messaging" +
"read_page_mailboxes" +
"ads_read" +
"pages_messaging_payments" +
"rsvp_event" +
"business_management" +
"pages_messaging_phone_number" +
"user_events" +
"manage_pages" +
"pages_messaging_subscriptions" +
"user_managed_groups" +
"pages_manage_cta" +
"pages_show_list" +
"pages_manage_instant_articles" +
"publish_pages" +
"user_actions.books" +
"user_actions.music" +
"user_actions.video" +
"user_actions.fitness" +
"user_actions.news" +
"Other" +
"read_audience_network_insights" +
"read_custom_friendlists" +
"read_insights";
           
            var auth = new OAuth2Authenticator(
                    clientId: "168801230313991",
                    scope: "user_friends,user_likes",
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
                    var request = new OAuth2Request("GET", new System.Uri("https://graph.facebook.com/me?fields=id,name,birthday,about,cover,devices,education,email,first_name,gender,favorite_athletes,favorite_teams,inspirational_people,interested_in,hometown,likes,friends,tagged_places"), null, e.Account);
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