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

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPiano")]
    public class ActivityPiano : AppCompatActivity, View.IOnClickListener
    {
        AudioManager am;
        KeyboardKey C1KBK, Csh1KBK, C2KBK, Csh2KBK, D1KBK, Dsh1KBK, D2KBK, Dsh2KBK, E1KBK, E2KBK, F1KBK, Fsh1KBK, F2KBK, Fsh2KBK, G1KBK, Gsh1KBK, G2KBK, Gsh2KBK, A1KBK, Ash1KBK, A2KBK, Ash2KBK, B1KBK, B2KBK;
        TextView playedNotesTV;
        bool isRecording = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PianoLayout);

           C1KBK=new KeyboardKey(FindViewById<Button>(Resource.Id.PianoC1BTN), MediaPlayer.Create(this, Resource.Raw.pianoC));
            D1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoD1BTN), MediaPlayer.Create(this, Resource.Raw.pianoD));
            E1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoE1BTN), MediaPlayer.Create(this, Resource.Raw.pianoE));
            F1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoF1BTN), MediaPlayer.Create(this, Resource.Raw.pianoF));
            G1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoG1BTN), MediaPlayer.Create(this, Resource.Raw.pianoG));
            A1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoA1BTN), MediaPlayer.Create(this, Resource.Raw.painoA));
            B1KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoB1BTN), MediaPlayer.Create(this, Resource.Raw.pianoB));
            C2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoC2BTN), MediaPlayer.Create(this, Resource.Raw.pianoC));
            D2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoD2BTN), MediaPlayer.Create(this, Resource.Raw.pianoD));
           E2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoE2BTN), MediaPlayer.Create(this, Resource.Raw.pianoE));
            F2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoF2BTN), MediaPlayer.Create(this, Resource.Raw.pianoF));
           G2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoG2BTN), MediaPlayer.Create(this, Resource.Raw.pianoG));
            A2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoA2BTN), MediaPlayer.Create(this, Resource.Raw.painoA));
            B2KBK = new KeyboardKey(FindViewById<Button>(Resource.Id.PianoG2BTN), MediaPlayer.Create(this, Resource.Raw.pianoB));
            C1KBK.GetKey().SetOnClickListener(this);
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
            KeyboardKey key = (Keyboa)v;
            string buttonText = button.Text;
            

            if (isRecording)
            {
                playedNotesTV.Text += (buttonText + ",");
            }
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