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
        Button DeleteBTN, UpdateBTN, PracticeBTN;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ProfileLayout);
            base.OnCreate(savedInstanceState);

            pfpIV = FindViewById<ImageView>(Resource.Id.ProfilePFPIV);
            fnameTV = FindViewById<TextView>(Resource.Id.ProfileFnameTV);
            unameTV = FindViewById<TextView>(Resource.Id.ProfileUnameTV);
            lnameTV = FindViewById<TextView>(Resource.Id.ProfileLnameTV);
            fnameTV = FindViewById<TextView>(Resource.Id.ProfileFnameTV);
            bdayTV = FindViewById<TextView>(Resource.Id.ProfileBirthdayTV);
            winRateTV = FindViewById<TextView>(Resource.Id.ProfileWinRateTV);
            lessoncountTV = FindViewById<TextView>(Resource.Id.ProfileLessonTV);
            DeleteBTN = FindViewById<Button>(Resource.Id.ProfileDeleteBTN);
            UpdateBTN = FindViewById<Button>(Resource.Id.ProfileUpdateBTN);
            PracticeBTN = FindViewById<Button>(Resource.Id.ProfilePracticeBTN);
            fnameTV.Text= Helper.SharePrefrence1(this).GetString("FName", null);
            lnameTV.Text = Helper.SharePrefrence1(this).GetString("LName", null);
            unameTV.Text = Helper.SharePrefrence1(this).GetString("UName", null);
            bdayTV.Text = Helper.SharePrefrence1(this).GetString("DOB", null);
            winRateTV.Text = ("win rate"+ Helper.SharePrefrence1(this).GetInt("winRate",0)+"%");
            //pfpIV = Helper.Base64ToBitmap(Helper.SharePrefrence1(this).GetString("photo", null));
            lessoncountTV.Text= ("lessons:" + Helper.SharePrefrence1(this).GetInt("winRate", 0));
            DeleteBTN.Click += DeleteBTN_Click;
            UpdateBTN.Click += UpdateBTN_Click;
            PracticeBTN.Click += PracticeBTN_Click;
        }

        private void PracticeBTN_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityNotesShow));
            StartActivity(intent);
        }

        private void UpdateBTN_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityUpdate));
            StartActivity(intent);
        }

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            var allData = Helper.dbCommand.Query<Notes>("DELETE FROM Customer WHERE Uname='"+ unameTV.Text+"'");
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}