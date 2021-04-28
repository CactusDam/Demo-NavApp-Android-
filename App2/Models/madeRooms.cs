using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App2.Models
{
    public class madeRooms
    {
        List<Room_Coor> listORooms = new List<Room_Coor>();
        public List<Xamarin.Essentials.Location> Rooms = new List<Xamarin.Essentials.Location>();
        public List<Xamarin.Essentials.Location> waypoints = new List<Xamarin.Essentials.Location>();
        public void main()
        {
            Room_Coor rc = new Room_Coor();
            Xamarin.Essentials.Location item = new Xamarin.Essentials.Location();
            item.Latitude = 39.0226;
            item.Longitude = -84.5681;
            rc.RealLatitude = item.Latitude;
            rc.RealLongitude = item.Longitude;
            rc.screenLatitude = 150;
            rc.screenLongitude = 1750;
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
            rc1.screenLatitude = 700;
            rc1.screenLongitude = 1750;
            listORooms.Add(rc1);
            Rooms.Add(item1);
            Room_Coor rc2 = new Room_Coor();
            Xamarin.Essentials.Location item2 = new Xamarin.Essentials.Location();
            item2.Latitude = 39.0226;
            item2.Longitude = -84.568378;
            rc2.RealLatitude = item2.Latitude;
            rc2.RealLongitude = item2.Longitude;
            rc2.name = "Dr. Zimmer's Office";
            rc2.screenLatitude = 650;
            rc2.screenLongitude = 850;
            listORooms.Add(rc2);
            Rooms.Add(item2);
            Room_Coor rc3 = new Room_Coor();
            Xamarin.Essentials.Location item3 = new Xamarin.Essentials.Location();
            item3.Latitude = 39.0226;
            item3.Longitude = -84.56840;
            rc3.RealLatitude = item3.Latitude;
            rc3.RealLongitude = item3.Longitude;
            rc3.name = "Computer Lab";
            rc3.screenLatitude = 650;
            rc3.screenLongitude = 400;
            listORooms.Add(rc3);
            Rooms.Add(item3);
            Room_Coor rc4 = new Room_Coor();
            Xamarin.Essentials.Location item4 = new Xamarin.Essentials.Location();
            item4.Latitude = 39.0226;
            item4.Longitude = -84.56840;
            rc4.RealLatitude = item4.Latitude;
            rc4.RealLongitude = item4.Longitude;
            rc4.name = "Sculpture/3-D";
            rc4.screenLatitude = 300;
            rc4.screenLongitude = 1250;
            listORooms.Add(rc4);
            Rooms.Add(item4);
            Room_Coor rc5 = new Room_Coor();
            Xamarin.Essentials.Location item5 = new Xamarin.Essentials.Location();
            item5.Latitude = 39.022429;
            item5.Longitude = -84.5683206;
            rc5.RealLatitude = item5.Latitude;
            rc5.RealLongitude = item5.Longitude;
            rc5.name = "Room 3122";
            rc5.screenLatitude = 650;
            rc5.screenLongitude = 150;
            Rooms.Add(item5);
            listORooms.Add(rc5);
            Room_Coor rc6 = new Room_Coor();
            Xamarin.Essentials.Location item6 = new Xamarin.Essentials.Location();
            item6.Latitude = 39.02329186;
            item6.Longitude = -84.5681324;
            rc6.RealLatitude = item4.Latitude;
            rc6.RealLongitude = item4.Longitude;
            rc6.name = "Professor Price's Office";
            rc6.screenLatitude = 800;
            rc6.screenLongitude = 2100;
            Rooms.Add(item6);
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
            waypoints.Add(wp1); waypoints.Add(wp2);
            waypoints.Add(wp3); waypoints.Add(wp4);
            waypoints.Add(wp5); waypoints.Add(wp6);
            waypoints.Add(wp7); waypoints.Add(wp8);
            waypoints.Add(wp9);
        }

        public List<Xamarin.Essentials.Location> GetRooms()
        {
            return Rooms;
        }

        public List<Room_Coor> Get_Room_Coordinates()
        {
            return listORooms;
        }
    }
}