using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
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
using Xamarin.Forms.PlatformConfiguration;
using static Android.Resource;

namespace App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public List<Xamarin.Essentials.Location> waypoints = new List<Xamarin.Essentials.Location>();
        public List<Xamarin.Essentials.Location> Rooms = new List<Xamarin.Essentials.Location>();
        public Xamarin.Essentials.Location destination = new Xamarin.Essentials.Location();
        public Xamarin.Essentials.Location currLoc = new Xamarin.Essentials.Location();
        List<Room_Coor> listORooms = new List<Room_Coor>();
        //TODO: adjust points to where the user is
        //actually navigate

        protected override void OnStart()
        {
            base.OnStart();

            //try doing math by hand then program
            Room_Coor rc = new Room_Coor();
            Xamarin.Essentials.Location item = new Xamarin.Essentials.Location();
            item.Latitude = 39.0226;
            item.Longitude = -84.5681;
            rc.RealLatitude = item.Latitude;
            rc.RealLongitude = item.Longitude;
            rc.screenLatitude = -150;
            rc.screenLongitude = 1300;
            rc.name = "Room 3107";
            Rooms.Add(item);
            listORooms.Add(rc);
            Room_Coor rc1 = new Room_Coor();
            Xamarin.Essentials.Location item1 = new Xamarin.Essentials.Location();
            item1.Latitude = 39.0227;
            item1.Longitude = -84.5681;
            rc1.RealLatitude = item1.Latitude;
            rc1.RealLongitude = item1.Longitude;
            rc1.name = "Room 3147";
            rc1.screenLatitude=300;
            rc1.screenLongitude = 1350;
            listORooms.Add(rc1);
            Rooms.Add(item1);
            Room_Coor rc2 = new Room_Coor();
            Xamarin.Essentials.Location item2 = new Xamarin.Essentials.Location();
            item2.Latitude = 39.0226;
            item2.Longitude = -84.568378;
            rc2.RealLatitude = item2.Latitude;
            rc2.RealLongitude = item2.Longitude;
            rc2.name = "Dr. Zimmer's Office";
            rc2.screenLatitude = 300;
            rc2.screenLongitude = 400;
            listORooms.Add(rc2);
            Rooms.Add(item2);
            Room_Coor rc3 = new Room_Coor();
            Xamarin.Essentials.Location item3 = new Xamarin.Essentials.Location();
            item3.Latitude = 39.0226;
            item3.Longitude = -84.56840;
            rc3.RealLatitude = item3.Latitude;
            rc3.RealLongitude = item3.Longitude;
            rc3.name = "Computer Lab";
            rc3.screenLatitude = 300;
            rc3.screenLongitude = -50;
            listORooms.Add(rc3);
            Rooms.Add(item3);
            Room_Coor rc4 = new Room_Coor();
            Xamarin.Essentials.Location item4 = new Xamarin.Essentials.Location();
            item4.Latitude = 39.0226;
            item4.Longitude = -84.56840;
            rc4.RealLatitude = item4.Latitude;
            rc4.RealLongitude = item4.Longitude;
            rc4.name = "Sculpture/3-D";
            rc4.screenLatitude = 0;
            rc4.screenLongitude = 750;
            listORooms.Add(rc4);
            Rooms.Add(item4);
            Room_Coor rc5 = new Room_Coor();
            Xamarin.Essentials.Location item5 = new Xamarin.Essentials.Location();
            item5.Latitude = 39.022429;
            item5.Longitude = -84.5683206;
            rc5.RealLatitude = item5.Latitude;
            rc5.RealLongitude = item5.Longitude;
            rc5.name = "Room 3122";
            rc5.screenLatitude = 300;
            rc5.screenLongitude = -250;
            listORooms.Add(rc5);
            Room_Coor rc6 = new Room_Coor();
            Xamarin.Essentials.Location item6 = new Xamarin.Essentials.Location();
            item6.Latitude = 39.02329186;
            item6.Longitude = -84.5681324;
            rc6.RealLatitude = item4.Latitude;
            rc6.RealLongitude = item4.Longitude;
            rc6.name = "Professor Price's Office";
            rc6.screenLatitude = 400;
            rc6.screenLongitude = 1600;
            listORooms.Add(rc6);

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
            //epsilon - location updated with 3-26 mapping
            Xamarin.Essentials.Location wp7 = new Xamarin.Essentials.Location();
            wp7.Latitude = 39.02268447;
            wp7.Longitude = -84.5682627;
            //zeta
            Xamarin.Essentials.Location wp8 = new Xamarin.Essentials.Location();
            wp8.Latitude = 39.022600494;
            wp8.Longitude = -84.5684017;
            //iota
            Xamarin.Essentials.Location wp9 = new Xamarin.Essentials.Location();
            wp9.Latitude = 39.022519510;
            wp9.Longitude = -84.56840749;

            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;

                if (result != null)
                {
                    if (result.Latitude.ToString() == "")
                    {
                        TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                        locationEnable.Text = "Please turn on your location and restart the app - OS";
                    }
                    else
                    {
                        //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}");
                        TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                        lat.Text = result.Latitude.ToString();

                        TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                        longitude.Text = result.Longitude.ToString();

                        TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                        alt.Text = result.Altitude.ToString();
                    }
                }
            }
            catch
            {
                //error?
                TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                locationEnable.Text = "Please turn on your location and restart the app";
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


            Button userHere = FindViewById<Button>(Resource.Id.youAreHere);
            Button userGoing = FindViewById<Button>(Resource.Id.hereYouGo);
            //userHere.ScaleX = .10f;
            //userHere.ScaleY = .10f;
            //userGoing.ScaleX = .10f;
            //userGoing.ScaleY = .10f;
            TextView alt = FindViewById<TextView>(Resource.Id.latPuller);
            TextView alt2 = FindViewById<TextView>(Resource.Id.longPuller);


            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.RoomsOnMap, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            
            StartListening();
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The room is {0}", spinner.GetItemAtPosition(e.Position));
            Button userGoing = FindViewById<Button>(Resource.Id.hereYouGo);
            Room_Coor locationChange = new Room_Coor();
            foreach(Room_Coor coordinates in listORooms)
            {
                
                string watcher = string.Format((string)spinner.GetItemAtPosition(e.Position));
                if(coordinates.name==spinner.GetItemAtPosition(e.Position).ToString())
                {
                    //userGoing.SetX(coordinates.screenLatitude);
                    //userGoing.SetY(coordinates.screenLongitude);

                    foreach(Room_Coor rc in listORooms)
                    {
                        if(rc.name==coordinates.name)
                        {
                            destination.Latitude = rc.RealLatitude;
                            destination.Longitude = rc.RealLongitude;
                        }
                    }
                    List<Xamarin.Essentials.Location> Route = new List<Xamarin.Essentials.Location>();
                    Route = getRoute();
                    Button button = FindViewById<Button>(Resource.Id.button1);
                    foreach(Location wps in Route)
                    {
                        //button.SetX(0);
                        //button.SetY(0);
                    }
                }
            }
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        public List<Xamarin.Essentials.Location> getRoute()
        {
            List<Xamarin.Essentials.Location> a2z = new List<Xamarin.Essentials.Location>();

            Xamarin.Essentials.Location closestWP = new Location();
            closestWP = destination;
            foreach(Xamarin.Essentials.Location loc in waypoints)
            {
                if(currLoc.CalculateDistance(loc, units:0)<currLoc.CalculateDistance(closestWP, units:0))
                {
                    if (destination.CalculateDistance(loc, units:0) < currLoc.CalculateDistance(loc, units: 0))
                    {
                        closestWP = loc;
                    }
                }
            }
            //foreach(Xamarin.Essentials.Location wp2 in waypoints)
            //{
            //    if(closestWP.CalculateDistance(wp2, units:0)<currLoc.CalculateDistance(wp2, units:0))
            //    {

            //    }
            //}
            return a2z;
        }

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
                if (result != null)
                {
                    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                    if (result.Latitude.ToString() == "")
                    {
                        //TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                        lat.Text = "Please turn on your location and restart the app - OR";
                    }
                    else
                    {
                        //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}");
                        //TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                        lat.Text = result.Latitude.ToString();

                        TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                        longitude.Text = result.Longitude.ToString();

                        TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                        alt.Text = result.Altitude.ToString();
                    }
                }
            }
            catch
            {
                TextView locationEnable = FindViewById<TextView>(Resource.Id.latPuller);
                locationEnable.Text = "Please turn on your location and restart the app";
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
                InitialCalc();
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;
                if (result != null)
                {
                    //Log.Warn("location", $"Latitude:{result.Latitude}, Longitude:{result.Longitude}, Altitude:{result.Altitude}");
                    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                    lat.Text = result.Latitude.ToString();

                    TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                    longitude.Text = result.Longitude.ToString();

                    TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                    alt.Text = result.Altitude.ToString();


                    TextView cR = FindViewById<TextView>(Resource.Id.altimeter);
                    DistanceUnits units = new DistanceUnits();
                    var shortestDist = Xamarin.Essentials.Location.CalculateDistance(result, Rooms.First(), units);
                    int place = 0;

                    currLoc.Latitude = result.Latitude ;
                    currLoc.Longitude = result.Longitude;
                    foreach (Xamarin.Essentials.Location room in Rooms)
                    {
                        var dist = Xamarin.Essentials.Location.CalculateDistance(result, room, units);

                        if (dist < shortestDist)
                        {
                            shortestDist = dist;
                        }
                        place++;
                    }
                    SymbolMover(result.Latitude, result.Longitude);
                }
            }
            catch
            {

            }

        }

        public void SymbolMover(double lat, double longe)
        {
            //will change the positions of x and o
            Button userHere = FindViewById<Button>(Resource.Id.youAreHere);
            //know the xy of the rooms, use them to do lat1-lat2 and delta1-x2
            //for every 1 degree, so many pixels
            //lat difference divided by delta x
            //long1-long2=y1-y2
            //deltalong is y/longe
            //use 0,0
            userHere.SetX((int)lat);
            userHere.SetY((int)longe);
        }

        public void InitialCalc()
        {
            Room_Coor rc1 = listORooms.ElementAtOrDefault<Room_Coor>(0);
            Room_Coor rc2 = listORooms.ElementAtOrDefault<Room_Coor>(1);
            double xDelta = rc1.RealLatitude - rc2.RealLatitude;
            double yDelta = rc1.RealLongitude - rc2.RealLongitude;
            int ScreenX = (int)xDelta / 30;
            int ScreenY = (int)yDelta / 30;
            int cartesianx = Math.Abs(ScreenX - 800 / 2);
            int cartesiany = Math.Abs(-ScreenY + 2200 / 2);

            Button userLoc = FindViewById<Button>(Resource.Id.youAreHere);
            userLoc.SetX(5000);
            userLoc.SetY(50000);
        }

        public override void OnBackPressed()
        {
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

