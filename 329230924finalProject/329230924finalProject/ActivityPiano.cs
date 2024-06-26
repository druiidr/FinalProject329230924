﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Media;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;
using System.Threading.Tasks;
using SQLite;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPiano")]
    public class ActivityPiano : AppCompatActivity, View.IOnClickListener
    {

        //הצהרה על משתנים
        AudioManager am;
       Button C1BTN, Csh1BTN, C2BTN, Csh2BTN, D1BTN, Dsh1BTN, D2BTN, Dsh2BTN, E1BTN, E2BTN, F1BTN, Fsh1BTN, F2BTN, Fsh2BTN, G1BTN, Gsh1BTN, G2BTN, Gsh2BTN, A1BTN, Ash1BTN, A2BTN, Ash2BTN, B1BTN, B2BTN;
        TextView playedNotesTV;
        SeekBar volSliderSB;
        Button playpauseBTN;
        string composition;
       
        string xxx = "";
        int mistakes = 0;
        MediaPlayer playingSoundMP=new MediaPlayer();
        bool isPlayinging = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך 
            base.OnCreate(savedInstanceState);
            this.Title = "";
            SetContentView(Resource.Layout.PianoLayout);
            playpauseBTN = FindViewById<Button>(Resource.Id.PianoPlayPauseBTN);
           C1BTN=FindViewById<Button>(Resource.Id.PianoC1BTN);
            D1BTN =FindViewById<Button>(Resource.Id.PianoD1BTN);
            E1BTN = (FindViewById<Button>(Resource.Id.PianoE1BTN));
            F1BTN = (FindViewById<Button>(Resource.Id.PianoF1BTN));
            G1BTN = (FindViewById<Button>(Resource.Id.PianoG1BTN));
            A1BTN = (FindViewById<Button>(Resource.Id.PianoA1BTN));
            B1BTN = (FindViewById<Button>(Resource.Id.PianoB1BTN));
            C2BTN = (FindViewById<Button>(Resource.Id.PianoC2BTN));
            this.Title = "";
            D2BTN = (FindViewById<Button>(Resource.Id.PianoD2BTN));
           E2BTN = (FindViewById<Button>(Resource.Id.PianoE2BTN));
            F2BTN = (FindViewById<Button>(Resource.Id.PianoF2BTN));      
           G2BTN = (FindViewById<Button>(Resource.Id.PianoG2BTN));
            A2BTN = (FindViewById<Button>(Resource.Id.PianoA2BTN));
            B2BTN = (FindViewById<Button>(Resource.Id.PianoG2BTN));
            Csh1BTN = FindViewById<Button>(Resource.Id.PianoCshrp1BTN);
            Dsh1BTN = FindViewById<Button>(Resource.Id.PianoDshrp1BTN);
            Fsh1BTN = (FindViewById<Button>(Resource.Id.PianoFshrp1BTN));
            Gsh1BTN = (FindViewById<Button>(Resource.Id.PianoGshrp1BTN));
            Ash1BTN = (FindViewById<Button>(Resource.Id.PianoAshrp1BTN));
            Csh2BTN = (FindViewById<Button>(Resource.Id.PianoCshrp2BTN));
            Dsh2BTN = (FindViewById<Button>(Resource.Id.PianoDshrp2BTN));
            Fsh2BTN = (FindViewById<Button>(Resource.Id.PianoFshrp2BTN));
            Gsh2BTN = (FindViewById<Button>(Resource.Id.PianoGshrp2BTN));
            Ash2BTN = (FindViewById<Button>(Resource.Id.PianoAshrp2BTN));
            playedNotesTV = FindViewById<TextView>(Resource.Id.PianoPlayedNotesTV);
            SetComposition();
            
            C1BTN.SetOnClickListener(this);
            D1BTN.SetOnClickListener(this);
           E1BTN.SetOnClickListener(this);
            F1BTN.SetOnClickListener(this);
            G1BTN.SetOnClickListener(this);
            A1BTN.SetOnClickListener(this);
            B1BTN.SetOnClickListener(this);
            C2BTN.SetOnClickListener(this);
            D2BTN.SetOnClickListener(this);
            E2BTN.SetOnClickListener(this);
            F2BTN.SetOnClickListener(this);
            G2BTN.SetOnClickListener(this);
            A2BTN.SetOnClickListener(this);
            B2BTN.SetOnClickListener(this);
            Csh1BTN.SetOnClickListener(this);
            Dsh1BTN.SetOnClickListener(this);
            Fsh1BTN.SetOnClickListener(this);
            Gsh1BTN.SetOnClickListener(this);
            Ash1BTN.SetOnClickListener(this);
            Csh2BTN.SetOnClickListener(this);
            Dsh2BTN.SetOnClickListener(this);
            Fsh2BTN.SetOnClickListener(this);
            Gsh2BTN.SetOnClickListener(this);
            Ash2BTN.SetOnClickListener(this);

            playpauseBTN.Click += PlaypauseBTN_Click;
        }
        public void SetComposition()
        {
            //הצבת תוכן השיעור בתצוגת התווים
            try
            {
                if (Helper.SharePrefrence1(this).GetString("NoteContent", null) != null)
                {
                    playedNotesTV.Text = Helper.SharePrefrence1(this).GetString("NoteContent", null);
                    isPlayinging = true;
                    composition = playedNotesTV.Text;
                }
            }
            catch { }

        }
        public void ClearComposition()
        {
            //ניקוי לוח התווים של השיעור
            var editor = Helper.SharePrefrence1(this).Edit();
            editor.PutString("NoteContent", null);
            editor.Commit();
        }
        private void PlaypauseBTN_Click(object sender, EventArgs e)
        {
            //טיפול בתחילת הנגינה האוטומטית במידה והמשתמש בתוך שיעור
            if(isPlayinging)
                PlayMelody(playedNotesTV.Text);
            else
                Toast.MakeText(this, "autoplay only available in lessons!", ToastLength.Long).Show();

        }
        async void PlayMelody(string str)
        {
            //נגינה אוטומטית של השיר הנתון בשיעור
            string ch="";
            while(str.Length>3)
            {
                while (str[0] != ',')
                {
                    ch = "" + str[0];
                    str = str.Substring(1);
                }
                str = str.Substring(1);
                Toast.MakeText(this, ch, ToastLength.Long).Show();
                PlayNotes("  " + ch);
                await Task.Delay(400);
            }
        }
     
        public void PlayNotes(string x)
        {

            //מנגן את התו המוזן

            switch (x)
            {
                case ("  A"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoA1);
                    playingSoundMP.Start();
                    break;
                case ("  B"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoB1);
                    playingSoundMP.Start();
                    break;
                case ("  C"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoC1);
                    playingSoundMP.Start();
                    break;
                case ("  D"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoD1);
                    playingSoundMP.Start();
                    break;
                case ("  E"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoE1);
                    playingSoundMP.Start();
                    break;
                case ("  F"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoF1);
                    playingSoundMP.Start();
                    break;
                case ("  G"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.pianoG1);
                    playingSoundMP.Start();
                    break;
                case ("  A#"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.a1s);
                    playingSoundMP.Start();
                    break;
                case ("  C#"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.c1s);
                    playingSoundMP.Start();
                    break;
                case ("  D#"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.d1s);
                    playingSoundMP.Start();
                    break;
                case ("  F#"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.f1s);
                    playingSoundMP.Start();
                    break;
                case ("  G#"):
                    playingSoundMP = MediaPlayer.Create(this, Resource.Raw.g1s);
                    playingSoundMP.Start();
                    break;
                default:
                    break;

            }

        }

 
        public void OnClick(View v)
        {
            //לחיצה על קליד
            Button button = (Button)v;
            
            string buttonText = button.Text;
            PlayNotes(buttonText);
            mistakes += GradeLesson(buttonText);
            if (mistakes > 3 &&! Helper.SharePrefrence1(this).GetBoolean("doesPay",true))
                Boot();
        }
        public int GradeLesson(string key)
        {
            //בדיקת דיוק הקליד המנוגן וחלוקת ניקוד בהתאם
            if (playedNotesTV.Text != "free play mode")
            {
                char note = composition.First();
                composition = composition.Substring(2);
                playedNotesTV.Text = composition;
                if (composition.First() == '.')
                {
                    SetLessonResult();
                Intent intent = new Intent(this, typeof(ActivityNotesShow));
                StartActivity(intent);
            }
                if (key.Contains(note))
                    return 0;
                else
                {
                    xxx += "X";
                    Toast.MakeText(this, xxx, ToastLength.Short).Show();
                    return 1;
                }
            }
            return 0;
            


        }
        public void Boot()
        {
            //סיום שיעור לאחר מספיק טעויות
            ClearComposition();
            Toast.MakeText(this, "you failed this exercise!!!", ToastLength.Long).Show();
            Intent intent = new Intent(this, typeof(ActivityNotesShow));
            StartActivity(intent);
        }
        public void SetLessonResult()
        {
            //קביעת תוצאות השיעור
            ClearComposition();
            if (Helper.SharePrefrence1(this).GetString("FName", null) != null)
            { 
                try
                {
                    Helper.dbCommand = new SQLiteConnection(Helper.Path());
                    string uName = Helper.SharePrefrence1(this).GetString("UName", null);
                    int noteCode = Helper.SharePrefrence1(this).GetInt("NoteCode", 0);

                    var allData = Helper.dbCommand.Query<Excercise>("SELECT * FROM Excercise WHERE UName = ? AND NoteCode = ?", uName, noteCode);

                    if (allData.Count != 0)
                    {
                        var exercise = allData[0];
                        if (exercise != null)
                        {
                            if (exercise.MistakesMade > xxx.Length)
                            {
                                // Updating existing record if mistakes made is less than current mistakes
                                Helper.dbCommand.Execute("UPDATE Excercise SET MisitakesMade = ?, DatePlayed = ? WHERE UName = ? AND NoteCode = ?", xxx.Length, DateTime.Now, uName, noteCode);
                                // Displaying a message
                                Toast.MakeText(this, "You outdid yourself!!", ToastLength.Short).Show();
                            }
                            else
                            {
                                // Displaying a message if mistakes are less than 
                                Toast.MakeText(this, "Oh come on, you can do better!", ToastLength.Short).Show();
                            }
                        }
                        else
                        {
                            // Handle null reference
                        }
                    }
                    else
                    {
                        // Inserting new record into Excercise table
                        Excercise exercise = new Excercise(uName, noteCode, DateTime.Now, xxx.Length);
                        // Saving changes to the database
                        Helper.dbCommand.Insert(exercise);
                        // Displaying a message
                        Toast.MakeText(this, "Exercise logged", ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {
                    // Displaying a generic error message
                    Toast.MakeText(this, "An error occurred. Please try again later.", ToastLength.Short).Show();
                }
        }
            else
            {
                Toast.MakeText(this, "Please log in to save your results!", ToastLength.Short).Show();
            }
        }




        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)

        {//מייצר מניו

            MenuInflater.Inflate(Resource.Menu.menuchophone, menu);
            return true;

        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)

        {

            if (item.ItemId == Resource.Id.action_Home)

            {
                //מעבר לדף בית
                Intent intent = new Intent(this, typeof(ActivityLogin));
                StartActivity(intent);
            }
            if (item.ItemId == Resource.Id.action_profile)

            {
                //מעבר לדף פרופיל
                if (Helper.SharePrefrence1(this).GetString("FName", null) != null)

                {
                    Intent intent = new Intent(this, typeof(ActivityProfile));
                    StartActivity(intent);
                }
            }

            else if (item.ItemId == Resource.Id.action_log_out)

            {
                //התנתקות
                var editor = Helper.SharePrefrence1(this).Edit();
                editor.PutString("password", null);
                editor.PutString("UName", null);
                editor.PutString("FName", null);
                editor.PutString("LName", null);
                editor.PutString("DOB", null);
                editor.PutString("email", null);
                editor.PutInt("phone", 0);
                editor.PutBoolean("doesPay", false);
                editor.Commit();
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                return true;
            }
            if (item.ItemId == Resource.Id.action_update)

            {
                //מעבר לדף עדכון פרטים
                Intent intent = new Intent(this, typeof(ActivityUpdate));
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
        public void menumaker()
        {

        }
    }
}