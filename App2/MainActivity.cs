using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Plugin.Geolocator;
using Plugin.Permissions;
using Xamarin.Essentials;

namespace App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public List<Xamarin.Essentials.Location> waypoints = new List<Xamarin.Essentials.Location>();
        public List<Xamarin.Essentials.Location> Rooms = new List<Xamarin.Essentials.Location>();
        public Xamarin.Essentials.Location destination = new Xamarin.Essentials.Location();
        public Xamarin.Essentials.Location currLoc = new Xamarin.Essentials.Location();
        public List<String> RoomNames = new List<string>();

        //TODO: get the code for finding the closest waypoint on the map
        //active location finding

        protected override void OnStart()
        {
            base.OnStart();

            Xamarin.Essentials.Location item = new Xamarin.Essentials.Location();
            item.Latitude = 39.0226;
            item.Longitude = -84.5681;
            RoomNames.Add("Room 3107");
            Rooms.Add(item);
            Xamarin.Essentials.Location item1 = new Xamarin.Essentials.Location();
            item1.Latitude = 39.0227;
            item1.Longitude = -84.5681;
            RoomNames.Add("Room 3147");
            Rooms.Add(item1);
            Xamarin.Essentials.Location item2 = new Xamarin.Essentials.Location();
            item2.Latitude = 39.0226;
            item2.Longitude = -84.568378;
            RoomNames.Add("Dr. Zimmer's Office");
            Rooms.Add(item2);
            Xamarin.Essentials.Location item3 = new Xamarin.Essentials.Location();
            item3.Latitude = 39.0226;
            item3.Longitude = -84.56840;
            RoomNames.Add("Computer Lab");
            Rooms.Add(item3);
            Xamarin.Essentials.Location item4 = new Xamarin.Essentials.Location();
            item4.Latitude = 39.0226;
            item4.Longitude = -84.56840;
            RoomNames.Add("Sculpture/3-D");
            Rooms.Add(item4);

            destination = item1;

            //theta
            Xamarin.Essentials.Location wp1 = new Xamarin.Essentials.Location();
            wp1.Latitude = 39.0224095;
            wp1.Longitude = -84.56834;
            //eta
            Xamarin.Essentials.Location wp2 = new Xamarin.Essentials.Location();
            wp2.Latitude = 39.0226994;
            wp2.Longitude = -84.568407;
            //beta
            Xamarin.Essentials.Location wp3 = new Xamarin.Essentials.Location();
            wp3.Latitude = 39.022888;
            wp3.Longitude = -84.56825;
            //gamma
            Xamarin.Essentials.Location wp4 = new Xamarin.Essentials.Location();
            wp4.Latitude = 39.0227819;
            wp4.Longitude = -84.568301;
            //alpha
            Xamarin.Essentials.Location wp5 = new Xamarin.Essentials.Location();
            wp5.Latitude = 39.02283444;
            wp5.Longitude = -84.568239;
            //delta
            Xamarin.Essentials.Location wp6 = new Xamarin.Essentials.Location();
            wp6.Latitude = 39.0221145;
            wp6.Longitude = -84.56805;
            //epsilon
            Xamarin.Essentials.Location wp7 = new Xamarin.Essentials.Location();
            wp7.Latitude = 39.0223245;
            wp7.Longitude = -84.56829;

            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;

                if (result != null)
                {
                    //if (result.Latitude.ToString() == "")
                    //{
                    //    TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                    //    locationEnable.Text = "Please turn on your location and restart the app - OS";
                    //}
                    //else
                    //{
                    //    //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}");
                    //    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                    //    lat.Text = result.Latitude.ToString();

                    //    TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                    //    longitude.Text = result.Longitude.ToString();

                    //    TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                    //    alt.Text = result.Altitude.ToString();
                    //}
                }
            }
            catch
            {
                //error?
                //TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                //locationEnable.Text = "Please turn on your location and restart the app";
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            //btnshowPopup = FindViewById<Button>(Resource.Id.btnPopup);
            //btnshowPopup.Click += BtnshowPopup_Click;


            //ImageView userHere = FindViewById<ImageView>(Resource.Id.youAreHere);
            //ImageView userGoing = FindViewById<ImageView>(Resource.Id.hereYouGo);
            //userHere.ScaleX = .10f;
            //userHere.ScaleY = .10f;
            //userGoing.ScaleX = .10f;
            //userGoing.ScaleY = .10f;

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            StartListening();
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        //private void BtnshowPopup_Click(object sender, System.EventArgs e)
        //{
        //popupDialog = new Dialog(this);
        //popupDialog.SetContentView(Resource.Layout.activity_main);
        //popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
        //popupDialog.Show();

        // Some Time Layout width not fit with windows size  
        // but Below lines are not necessery  
        //popupDialog.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);
        //popupDialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);

        // Access Popup layout fields like below  
        //btnPopupCancel = popupDialog.FindViewById<Button>(Resource.Id.btnCancel);
        //btnPopOk = popupDialog.FindViewById<Button>(Resource.Id.btnOk);

        // Events for that popup layout  
        //btnPopupCancel.Click += BtnPopupCancel_Click;
        //btnPopOk.Click += BtnPopOk_Click;

        // Some Additional Tips   
        // Set the dialog Title Property - popupDialog.Window.SetTitle("Alert Title");  
        //}

        //private void BtnPopOk_Click(object sender, System.EventArgs e)
        //{
        //    popupDialog.Dismiss();
        //    popupDialog.Hide();
        //}

        //private void BtnPopupCancel_Click(object sender, System.EventArgs e)
        //{
        //    popupDialog.Dismiss();
        //    popupDialog.Hide();
        //}

        private async void FabOnClickAsync(object sender, EventArgs eventArgs)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if (!status.Equals(PermissionStatus.Granted))
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
            }

            if (status.Equals(PermissionStatus.Granted))
            {
            }
            else if (status.Equals(PermissionStatus.Restricted))
            {
                //location denied
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void onResume()
        {
            Log.Info("test", "OR");

            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;
                //if (result != null)
                //{
                //    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                //    if (result.Latitude.ToString() == "")
                //    {
                //        //TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                //        lat.Text = "Please turn on your location and restart the app - OR";
                //    }
                //    else
                //    {
                //        //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}");
                //        //TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                //        lat.Text = result.Latitude.ToString();

                //        TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                //        longitude.Text = result.Longitude.ToString();

                //        TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                //        alt.Text = result.Altitude.ToString();
                //    }
                //}
            }
            catch
            {
                //TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                //locationEnable.Text = "Please turn on your location and restart the app";
            }
        }

        private async Task StartListening()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });

            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            var position = e.Position;
            currLoc.Longitude = position.Longitude;
            currLoc.Latitude = position.Latitude;
            currLoc.Altitude = position.Altitude;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
            Log.Info("loc", output);
            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;
                if (result != null)
                {
                    //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}, Altitude:{result.Altitude}");
                    //TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                    //lat.Text = result.Latitude.ToString();

                    //TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                    //longitude.Text = result.Longitude.ToString();

                    //TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                    //alt.Text = result.Altitude.ToString();


                    //TextView cR = FindViewById<TextView>(Resource.Id.closestRoom);
                    DistanceUnits units = new DistanceUnits();
                    var shortestDist = Xamarin.Essentials.Location.CalculateDistance(result, Rooms.First(), units);
                    string closestRoomName = RoomNames[0];
                    int place = 0;
                    foreach (Xamarin.Essentials.Location room in Rooms)
                    {
                        var dist = Xamarin.Essentials.Location.CalculateDistance(result, room, units);

                        if (dist < shortestDist)
                        {
                            shortestDist = dist;
                            closestRoomName = RoomNames[place];
                        }
                        //else if(dist==shortestDist)
                        //{
                        //    closestRoomName = "SAME";
                        //}
                        place++;

                    }
                    //TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                    //alt.Text = closestRoomName;

                }
            }
            catch
            {

            }

        }

        public override void OnBackPressed()
        {
            Log.Info("test", "OR");

            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;
                //if (result != null)
                //{
                //    Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}, Altitude:{result.Altitude}");
                //    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                //    lat.Text = result.Latitude.ToString();

                //    TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                //    longitude.Text = result.Longitude.ToString();

                //    TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                //    alt.Text = result.Altitude.ToString();
                //}
            }
            catch
            {

            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }


    }
}

