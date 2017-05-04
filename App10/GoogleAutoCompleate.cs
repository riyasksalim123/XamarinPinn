using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Android.Views.InputMethods;
using System.Text;
using App10.Adapter;


namespace App10
{
    [Activity(Label = "GoogleMapPlaceAPI", MainLauncher = false )]

    public class GoogleAutoCompleate : Activity,IOnMapReadyCallback
    {
        Button showlocationme;
        const string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        const string strGoogleApiKey = "AIzaSyAq1k0JIsGu8iEUdcjOjMt11c42x_t2WOo";
        const string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        AutoCompleteTextView txtSearch;
        GoogleMap map;
        ArrayAdapter adapter = null;
        GoogleMapClass objMapClass;
        string autoCompleteOptions;
        string[] strPredictiveText;
        int index = 0;
        double lat;
        double longi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AutoCompleateGoogle);
            lat = Intent.GetDoubleExtra("latitude", 0.0);
            longi = Intent.GetDoubleExtra("longitude", 0.0);
            txtSearch = (AutoCompleteTextView)FindViewById(Resource.Id.txtTextSearch);
            showlocationme= (Button)FindViewById(Resource.Id.showlocationme);
            showlocationme.Click += Showlocationme_Click;
            txtSearch.ItemClick += AutoCompleteOption_Click;
            txtSearch.Hint = "Enter source  ";
            MapsInitializer.Initialize(this);
            setUpMap();

           


            txtSearch.TextChanged += async delegate (object sender, Android.Text.TextChangedEventArgs e)
            {
                try
                {
                    autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + txtSearch.Text + "&key=" + strGoogleApiKey);

                    if (autoCompleteOptions == "Exception")
                    {
                        Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
                        return;
                    }
                    objMapClass = JsonConvert.DeserializeObject<GoogleMapClass>(autoCompleteOptions);
                    strPredictiveText = new string[objMapClass.predictions.Count];
                    index = 0;
                    foreach (Prediction objPred in objMapClass.predictions)
                    {
                        strPredictiveText[index] = objPred.description;
                        index++;
                    }
                    adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, strPredictiveText);
                    txtSearch.Adapter = adapter;
                }
                catch
                {
                    Toast.MakeText(this, "Unable to process at this moment!!!", ToastLength.Short).Show();
                }

            };

        }

        private void Showlocationme_Click(object sender, EventArgs e)
        {
            LatLng Position = new LatLng(lat,longi);
            updateCameraPosition(Position);
            MarkOnMap("I am Here !!", Position);
        }

        private void setUpMap()
        {
            if (map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        async void AutoCompleteOption_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(txtSearch.WindowToken, HideSoftInputFlags.NotAlways);
                if (txtSearch.Text != string.Empty)
                {
                    var sb = new StringBuilder();
                    sb.Append(strGeoCodingUrl);
                    sb.Append("?address=").Append(txtSearch.Text);
                    string strResult = await fnDownloadString(sb.ToString());
                    if (strResult == "Exception")
                    {
                        Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();

                    }
                    GoogleMapPlaceAPI.GeoCodeJSONClass objGeoCodeJSONClass = JsonConvert.DeserializeObject<GoogleMapPlaceAPI.GeoCodeJSONClass>(strResult);
                    LatLng Position = new LatLng(objGeoCodeJSONClass.results[0].geometry.location.lat, objGeoCodeJSONClass.results[0].geometry.location.lng);
                    updateCameraPosition(Position);
                    MarkOnMap(txtSearch.Text, Position);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        void MarkOnMap(string title, LatLng pos)
        {
            RunOnUiThread(()  =>
            {
                var marker = new MarkerOptions();
                marker.SetTitle(title);
                marker.SetPosition(pos);
                map.AddMarker(marker);
            } );

        }
        void updateCameraPosition(LatLng pos)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(pos);
            builder.Zoom(14);
            builder.Bearing(45);
            builder.Tilt(90);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.AnimateCamera(cameraUpdate);
        }

        async Task<string> fnDownloadString(string strUri)
        {
            WebClient webclient = new WebClient();
            string strResultData = "" ;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
               
            }
            catch(Exception ex)
            {
                strResultData = "Exception";
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
                } );
            }
            finally
            {
                webclient.Dispose();
                webclient = null;
                
            }
            return strResultData;

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.map = googleMap;
        }
    }
}
