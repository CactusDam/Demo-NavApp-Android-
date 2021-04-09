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

namespace App2
{
    public class Room_Coor
    {
        public string name { set; get; }
        public double RealLatitude { set; get; }
        public double RealLongitude { set; get; }
        public int screenLatitude { set; get; }
        public int screenLongitude { set; get; }

        public ImageView picNorth { set; get; }
        public ImageView picSouth { set; get; }
        public ImageView picEast { set; get; }
        public ImageView picWest { set; get; }
    }
}