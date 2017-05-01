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
using System.Threading;

namespace App10
{
    [Activity(Label = "SplashScreen", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.SplashActivity")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Wait for 2 seconds
            Thread.Sleep(2000);

            //Moving to next activity
            StartActivity(typeof(MainActivity));
        }
    }
}