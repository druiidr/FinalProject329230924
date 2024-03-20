using System;
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

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPiano")]
    public class ActivityPiano : AppCompatActivity, View.IOnClickListener
    {
        AudioManager am;
       Button C1BTN, Csh1BTN, C2BTN, Csh2BTN, D1BTN, Dsh1BTN, D2BTN, Dsh2BTN, E1BTN, E2BTN, F1BTN, Fsh1BTN, F2BTN, Fsh2BTN, G1BTN, Gsh1BTN, G2BTN, Gsh2BTN, A1BTN, Ash1BTN, A2BTN, Ash2BTN, B1BTN, B2BTN;
        TextView playedNotesTV;
        Button playpauseBTN;
        MediaPlayer playingSoundMP=new MediaPlayer();
        bool isPlayinging = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
            D2BTN = (FindViewById<Button>(Resource.Id.PianoD2BTN));
           E2BTN = (FindViewById<Button>(Resource.Id.PianoE2BTN));
            F2BTN = (FindViewById<Button>(Resource.Id.PianoF2BTN));
           G2BTN = (FindViewById<Button>(Resource.Id.PianoG2BTN));
            A2BTN = (FindViewById<Button>(Resource.Id.PianoA2BTN));
            B2BTN = (FindViewById<Button>(Resource.Id.PianoG2BTN));
            playedNotesTV = FindViewById<TextView>(Resource.Id.PianoPlayedNotesTV);
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
            playpauseBTN.Click += PlaypauseBTN_Click;
        }

        private void PlaypauseBTN_Click(object sender, EventArgs e)
        {

            PlayMelody(playedNotesTV.Text);

        }
        async void PlayMelody(string str)
        {
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
                default:
                    break;

            }

        }

        //private void RecBTN_Click(object sender, EventArgs e)
        //{

        //    if (!isRecording)
        //    {
        //        מתחיל הקלטה
        //        recsiganlBTN.Visibility = Android.Views.ViewStates.Visible;
        //        playedNotesTV.Text = "";
        //        isRecording = !isRecording;
        //    }
        //    else
        //    {
        //        מסיים הקלטה
        //        recsiganlBTN.Visibility = Android.Views.ViewStates.Invisible;
        //        playedNotesTV.Text = "played notes here";
        //        if (playedNotesTV.Text.Length > 0)
        //            if ()
        //                isRecording = !isRecording;

        //    }
        //}

        public void OnClick(View v)
        {
            Button button = (Button)v;
            
            string buttonText = button.Text;
            //if (!button.Text.Contains("#"))
               

            PlayNotes(buttonText);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)

        {//מייצר מניו

            MenuInflater.Inflate(Resource.Menu.menuchophone, menu);
            return true;

        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)

        {

            if (item.ItemId == Resource.Id.action_login)

            {
                //מעבר לדף כניסת משתמש
                Intent intent = new Intent(this, typeof(ActivityLogin));
                StartActivity(intent);
            }

            else if (item.ItemId == Resource.Id.action_log_out)

            {
                //התנתקות
                var editor = Helper.SharePrefrence1(this).Edit();
                editor.PutString("password", null);
                editor.PutString("UName", null);
                editor.PutString("FName", null);
                editor.PutString("LName", null);
                editor.PutString("Date", null);
                editor.PutString("email", null);
                editor.PutInt("phone", 0);
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