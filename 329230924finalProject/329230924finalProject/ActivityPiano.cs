using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPiano")]
    public class ActivityPiano : AppCompatActivity, View.IOnClickListener
    {
        Button recsiganlBTN,recBTN, C1BTN, Csh1BTN, C2BTN, Csh2BTN, D1BTN, Dsh1BTN, D2BTN, Dsh2BTN, E1BTN, E2BTN, F1BTN, Fsh1BTN, F2BTN, Fsh2BTN, G1BTN, Gsh1BTN, G2BTN, Gsh2BTN, A1BTN, Ash1BTN, A2BTN, Ash2BTN, B1BTN, B2BTN;
        TextView playedNotesTV;
        bool isRecording = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PianoLayout);

            C1BTN=FindViewById<Button>(Resource.Id.PianoC1BTN);
            Csh1BTN=FindViewById<Button>(Resource.Id.PianoCshrp1BTN);
            D1BTN=FindViewById<Button>(Resource.Id.PianoD1BTN);
            Dsh1BTN=FindViewById<Button>(Resource.Id.PianoDshrp1BTN);
            E1BTN=FindViewById<Button>(Resource.Id.PianoE1BTN);
            F1BTN=FindViewById<Button>(Resource.Id.PianoF1BTN);
            Fsh1BTN=FindViewById<Button>(Resource.Id.PianoFshrp1BTN);
            G1BTN=FindViewById<Button>(Resource.Id.PianoG1BTN);
            Gsh1BTN=FindViewById<Button>(Resource.Id.PianoGshrp1BTN);
            A1BTN=FindViewById<Button>(Resource.Id.PianoA1BTN);
            Ash1BTN=FindViewById<Button>(Resource.Id.PianoAshrp1BTN);
            B1BTN=FindViewById<Button>(Resource.Id.PianoB1BTN);
            C2BTN = FindViewById<Button>(Resource.Id.PianoC2BTN);
            Csh2BTN = FindViewById<Button>(Resource.Id.PianoCshrp2BTN);
            D2BTN = FindViewById<Button>(Resource.Id.PianoD2BTN);
            Dsh2BTN = FindViewById<Button>(Resource.Id.PianoDshrp2BTN);
            E2BTN = FindViewById<Button>(Resource.Id.PianoE2BTN);
            F2BTN = FindViewById<Button>(Resource.Id.PianoF2BTN);
            Fsh2BTN = FindViewById<Button>(Resource.Id.PianoFshrp2BTN);
            G2BTN = FindViewById<Button>(Resource.Id.PianoG2BTN);
            Gsh2BTN = FindViewById<Button>(Resource.Id.PianoGshrp2BTN);
            A2BTN = FindViewById<Button>(Resource.Id.PianoA2BTN);
            Ash2BTN = FindViewById<Button>(Resource.Id.PianoAshrp2BTN);
            B2BTN = FindViewById<Button>(Resource.Id.PianoB2BTN);
            recsiganlBTN =FindViewById<Button>(Resource.Id.PianorecordingsignalBTN);
            recBTN=FindViewById<Button>(Resource.Id.PianoRecordBTN);
            playedNotesTV=FindViewById<TextView>(Resource.Id.PianoPlayedNotesTV);
            C1BTN.SetOnClickListener(this);
            Csh1BTN.SetOnClickListener(this);
            D1BTN.SetOnClickListener(this);
           Dsh1BTN.SetOnClickListener(this);
            E1BTN.SetOnClickListener(this);
            F1BTN.SetOnClickListener(this);
            Fsh1BTN.SetOnClickListener(this);
            G1BTN.SetOnClickListener(this);
            Gsh1BTN.SetOnClickListener(this);
           A1BTN.SetOnClickListener(this);
            Ash1BTN.SetOnClickListener(this);
            B1BTN.SetOnClickListener(this);
            C2BTN.SetOnClickListener(this);
            Csh2BTN.SetOnClickListener(this);
            D2BTN.SetOnClickListener(this);
            Dsh2BTN.SetOnClickListener(this);
            E2BTN.SetOnClickListener(this);
            F2BTN.SetOnClickListener(this);
            Fsh2BTN.SetOnClickListener(this);
            G2BTN.SetOnClickListener(this);
            Gsh2BTN.SetOnClickListener(this);
            A2BTN.SetOnClickListener(this);
            Ash2BTN.SetOnClickListener(this);
            B2BTN.SetOnClickListener(this);
            recBTN.Click += RecBTN_Click;

        }

        private void RecBTN_Click(object sender, EventArgs e)
        {

            if (!isRecording)
            {
                recsiganlBTN.Visibility = Android.Views.ViewStates.Visible;
                playedNotesTV.Text = "";
                isRecording = !isRecording;
            }
            else
            {
                recsiganlBTN.Visibility = Android.Views.ViewStates.Invisible;
                playedNotesTV.Text = "played notes here";
                isRecording = !isRecording;

            }
        }

        public void OnClick(View v)
        {
            Button button = (Button)v;
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