using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using Android.Graphics;
using Android;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityProfile")]
    public class ActivityProfile : Activity
    {
        ImageView pfpIV;
        TextView unameTV, fnameTV, lnameTV, bdayTV, winRateTV, lessoncountTV;
        Button logOutBTN, UpdateBTN, PracticeBTN;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ProfileLayout);
            base.OnCreate(savedInstanceState);

            pfpIV=FindViewById<ImageView>(Resource.Id.prof)
        }
    }
}