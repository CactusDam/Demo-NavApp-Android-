using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using App2.Models;
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
        public int screenLat;
        public int screenLon;
        public int level = 0;
        Button levelClicker;
        //TODO: adjust points to where the user is
        //actually navigate

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


            Button userHere = FindViewById<Button>(Resource.Id.youAreHere);
            Button userGoing = FindViewById<Button>(Resource.Id.hereYouGo);
            Button LevelNav = FindViewById<Button>(Resource.Id.levelSelect);
            LevelNav.SetX(1100);
            LevelNav.SetY(0);
            
            
            TextView alt = FindViewById<TextView>(Resource.Id.latPuller);
            TextView alt2 = FindViewById<TextView>(Resource.Id.longPuller);
            alt.SetX(400);
            alt.SetY(1000);
            alt2.SetY(1100);
            alt2.SetX(400);

            madeRooms mr = new madeRooms();
            mr.main();
            waypoints = mr.waypoints;
            Rooms = mr.GetRooms();
            listORooms = mr.Get_Room_Coordinates();

            levelClicker = FindViewById<Button>(Resource.Id.levelSelect);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            TextView tv1 = FindViewById<TextView>(Resource.Id.textView1);
            tv1.SetY(1000);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.RoomsOnMap, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            InitialCalc();
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
                    userGoing.SetX(coordinates.screenLatitude);
                    userGoing.SetY(coordinates.screenLongitude);

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
                    //Button button = FindViewById<Button>(Resource.Id.button1);
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
            //SymbolMover(position.Latitude, position.Longitude);

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
                    TextView lat = FindViewById<TextView>(Resource.Id.latPuller);
                    lat.Text = result.Latitude.ToString();

                    TextView longitude = FindViewById<TextView>(Resource.Id.longPuller);
                    longitude.Text = result.Longitude.ToString();

                    TextView alt = FindViewById<TextView>(Resource.Id.altPuller);
                    alt.Text = result.Altitude.ToString();

                    currLoc.Latitude = result.Latitude;
                    currLoc.Longitude = result.Longitude;
                    SymbolMover(result.Latitude, result.Longitude);
                }
            }
            catch
            {

            }

        }

        public string closestRoom(Location result)
        {
            TextView cR = FindViewById<TextView>(Resource.Id.altimeter);
            DistanceUnits units = new DistanceUnits();
            var shortestDist = Xamarin.Essentials.Location.CalculateDistance(result, Rooms.First(), units);
            int place = 0;
            string winner = "";
            foreach (Room_Coor room in listORooms)
            {
                Location temp = new Location(room.RealLatitude, room.RealLongitude);
                var dist = Xamarin.Essentials.Location.CalculateDistance(result, temp, units);

                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    winner = room.name;
                }
                place++;
            }
            return winner;
        }

        public void SymbolMover(double lat, double longi)
        {
            Location result = new Location(lat, longi);
            string closetoroom = closestRoom(result);
            if (closetoroom != "")
            {
                foreach (Room_Coor room in listORooms)
                {
                    if (room.name == closetoroom)
                    {
                        screenLat = room.screenLatitude;
                        screenLon = room.screenLongitude;
                    }
                }
            }

            if (currLoc.Latitude - lat >= .0001)
            {
                screenLat += 20;
            }
            else if (currLoc.Latitude - lat <= -.0001)
            {
                screenLat -= 20;
            }
            double temp = currLoc.Latitude - lat;
            double temp2 = currLoc.Longitude - longi;
            //do the same thing for longitude
            if (currLoc.Longitude - longi <= .0001)
            {
                screenLon += 20;
            }
            else if (currLoc.Longitude - longi >= .0001)
            {
                screenLon -= 20;
            }



            //foreach (Room_Coor looking4matches in listORooms)
            //{
            //    if (looking4matches.RealLatitude == currLoc.Latitude || currLoc.CalculateDistance(looking4matches.RealLatitude, currLoc.Longitude, units: 0) < .0005)
            //    {
            //        //close to matching longitudes irl
            //        screenLat = looking4matches.screenLatitude;
            //        closetoroom = true;
            //    }
            //    else if (looking4matches.RealLongitude == currLoc.Longitude || currLoc.CalculateDistance(currLoc.Latitude, looking4matches.RealLongitude, units: 0) < .0005)
            //    {
            //        screenLon = looking4matches.screenLongitude;
            //        closetoroom = true;
            //    }
            //    else { closetoroom = false; }
            //}
            Button user = FindViewById<Button>(Resource.Id.youAreHere);
            user.SetX(screenLat);
            user.SetY(screenLon);
        }

        public void InitialCalc()
        {
            //basic is that .001 LAT = 500px
            // --- long = ---px
            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();
                location.Wait();
                var result = location.Result;
                if (result != null)
                { 
                    currLoc.Latitude = result.Latitude;
                    currLoc.Longitude = result.Longitude;
                }
            }
            catch
            {
            }
            bool latMatch = false;
            bool lonMatch = false;

            double storedLat = 0;
            double storedLon = 0;
            foreach (Room_Coor looking4matches in listORooms)
            {
                if(looking4matches.RealLatitude==currLoc.Latitude)
                {
                    //close to matching longitudes irl
                    screenLat = looking4matches.screenLatitude;
                    latMatch = true;
                }
                else if(currLoc.CalculateDistance(looking4matches.RealLatitude, currLoc.Longitude, units: 0) < .0915
                    &&latMatch!=true)
                {
                    screenLat = looking4matches.screenLatitude;
                }
                if(looking4matches.RealLongitude==currLoc.Longitude)
                {
                    screenLon = looking4matches.screenLongitude;
                    lonMatch = true;
                }
                else if(currLoc.CalculateDistance(currLoc.Latitude, looking4matches.RealLongitude, units: 0) < .0005
                    &&lonMatch==false)
                {
                    if(currLoc.CalculateDistance(currLoc.Latitude, looking4matches.RealLongitude, units:0)<
                        currLoc.CalculateDistance(currLoc.Latitude, storedLon, units:0))
                    {
                        screenLon = looking4matches.screenLongitude;
                        storedLon = looking4matches.RealLongitude;
                    }
                }
            }
            Button userLoc = FindViewById<Button>(Resource.Id.youAreHere);
            userLoc.SetX(screenLat);
            userLoc.SetY(screenLon);
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            ImageView iv = FindViewById<ImageView>(Resource.Id.backgroundImage);

            iv.SetImageDrawable(drawable: (Android.Graphics.Drawables.Drawable)Resource.Drawable.midfloor);

        }
        public string LevelSelect()
        {

            ImageView iv = FindViewById<ImageView>(Resource.Id.backgroundImage);
            if(level<3)
            {
                level++;
                if(level==1)
                {
                    //midfloor
                    iv.SetImageDrawable(drawable: (Android.Graphics.Drawables.Drawable)Resource.Drawable.midfloor);
                }
                if(level==2)
                {
                    //topfloor
                    iv.SetImageDrawable(drawable: (Android.Graphics.Drawables.Drawable)Resource.Drawable.topFloor);
                }
            }
            else if(level==3)
            {
                level = 0;
                //limitedmap
                iv.SetImageDrawable(drawable: (Android.Graphics.Drawables.Drawable)Resource.Drawable.limitedMap);
            }
            return "selected";
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

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    //MenuInflater.Inflate(Resource.Menu.menu_main, menu);
        //    return true;
        //}

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    int id = item.ItemId;
        //    if (id == Resource.Id.action_settings)
        //    {
        //        return true;
        //    }

        //    return base.OnOptionsItemSelected(item);
        //}

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View)sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                //.SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            //if (id == Resource.Id.nav_camera)
            //{
            //    // Handle the camera action
            //}
            //else if (id == Resource.Id.nav_gallery)
            //{

            //}
            //else if (id == Resource.Id.nav_slideshow)
            //{

            //}
            //else if (id == Resource.Id.nav_manage)
            //{

            //}
            //else if (id == Resource.Id.nav_share)
            //{

            //}
            //else if (id == Resource.Id.nav_send)
            //{

            //}

            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }


    }
}

