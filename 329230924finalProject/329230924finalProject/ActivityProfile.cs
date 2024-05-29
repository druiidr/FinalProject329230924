﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using SQLite;
using Android.Views;
namespace _329230924finalProject
{
    [Activity(Label = "ActivityProfile")]
    public class ActivityProfile : AppCompatActivity, ListView.IOnItemClickListener
    {
        ImageView pfpIV;
        ExcerciseAdapter excerciseAdapter;
        public static List<Excercise> excerciseList { get; set; }
        TextView unameTV, fnameTV, lnameTV, bdayTV, winRateTV, lessoncountTV;
        Button DeleteBTN, UpdateBTN, PurchaseBTN;
        ListView lv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.ProfileLayout);
            base.OnCreate(savedInstanceState);

            pfpIV = FindViewById<ImageView>(Resource.Id.ProfilePFPIV);
            fnameTV = FindViewById<TextView>(Resource.Id.ProfileFnameTV);
            unameTV = FindViewById<TextView>(Resource.Id.ProfileUnameTV);
            lnameTV = FindViewById<TextView>(Resource.Id.ProfileLnameTV);
            fnameTV = FindViewById<TextView>(Resource.Id.ProfileFnameTV);
            lv = FindViewById<ListView>(Resource.Id.ProfileListviewlv);
            bdayTV = FindViewById<TextView>(Resource.Id.ProfileBirthdayTV);
            DeleteBTN = FindViewById<Button>(Resource.Id.ProfileDeleteBTN);
            UpdateBTN = FindViewById<Button>(Resource.Id.ProfileUpdateBTN);
            PurchaseBTN = FindViewById<Button>(Resource.Id.ProfilePurchaseBTN);
            fnameTV.Text= Helper.SharePrefrence1(this).GetString("FName", null);
            lnameTV.Text = Helper.SharePrefrence1(this).GetString("LName", null);
            unameTV.Text = Helper.SharePrefrence1(this).GetString("UName", null);
            bdayTV.Text = Helper.SharePrefrence1(this).GetString("DOB", null);
            //pfpIV = Helper.Base64ToBitmap(Helper.SharePrefrence1(this).GetString("photo", null));
            DeleteBTN.Click += DeleteBTN_Click;
            UpdateBTN.Click += UpdateBTN_Click;
            PurchaseBTN.Click += PurchaseBTN_Click;
            lv.OnItemClickListener = this;
            try
            {

                // Query the SQLite database to fetch notes based on the selected level
                Helper.dbCommand = new SQLiteConnection(Helper.Path());
                    excerciseList = Helper.dbCommand.Query<Excercise>("SELECT * FROM Excercise");
                excerciseAdapter = new ExcerciseAdapter(this, excerciseList);
                lv.Adapter = excerciseAdapter;

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Could not load notes: " + ex.Message, ToastLength.Long).Show();
            }
        }

        private void PurchaseBTN_Click(object sender, EventArgs e)
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
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // Get the NoteCode of the clicked item from the ListView position
            int noteCode = excerciseList[position].NoteCode; // Assuming NoteCode starts from 1 and increments by 1

            // Query the database for the note based on its NoteCode
            var thisNote = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE NoteCode=?", noteCode);

            // Check if the query returned any notes
            if (thisNote != null && thisNote.Count > 0)
            {
                // Retrieve the first note from the query result
                Notes selectedNote = thisNote[0];

                // Access the NoteContent property of the selected note
                string noteContent = selectedNote.NoteContent;

                // Store the NoteContent in SharedPreferences
                var composition = Helper.SharePrefrence1(this).Edit();
                composition.PutString("NoteContent", noteContent);
                composition.Commit();

                // Start the ActivityPiano activity
                Intent intent = new Intent(this, typeof(ActivityPiano));

                StartActivity(intent);
            }
            else
            {
                // Handle the case where the note with the specified NoteCode was not found
                Toast.MakeText(this, "Note not found", ToastLength.Short).Show();
            }
        }
    }
}