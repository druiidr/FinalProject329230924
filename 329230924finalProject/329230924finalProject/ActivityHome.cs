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
    [Activity(Label = "ActivityHome")]
    public class ActivityHome : AppCompatActivity
    {
        //הצהרה על משתנים
        TextView welcomeTV;
        LinearLayout HomeLinLay;
        Button pianoBTN, NotesBTN;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל

            base.OnCreate(savedInstanceState);
            SetTheme(Android.Resource.Style.ThemeNoTitleBar);
            SetContentView(Resource.Layout.HomeLayout);
            HomeLinLay = FindViewById<LinearLayout>(Resource.Id.HomeLO);
            welcomeTV = new TextView(this);
            welcomeTV.TextSize=25;
            welcomeTV.SetTextColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            HeyYou();
            LinearLayout.LayoutParams layoutparams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, 180);
            pianoBTN = new Button(this);
            pianoBTN.LayoutParameters = layoutparams;
            pianoBTN.Text = "free play";
            pianoBTN.SetBackgroundColor(Android.Graphics.Color.DarkViolet);
            pianoBTN.SetX(420);
            pianoBTN.SetY(1080);
            NotesBTN = new Button(this);
            NotesBTN.LayoutParameters = layoutparams;
            NotesBTN.Text = "start practicing";
            NotesBTN.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
            NotesBTN.SetX(420);
            NotesBTN.SetY(1080);
            HomeLinLay.AddView(pianoBTN);
            HomeLinLay.AddView(NotesBTN);
            HomeLinLay.AddView(welcomeTV);
            pianoBTN.Click += PianoBTN_Click;
            NotesBTN.Click += NotesBTN_Click;

        }
        public void HeyYou()
        {//מוצא ומציג את השם הפרטי של המשתמש במידה ונשמר
            try
            {
                welcomeTV.Text = "hello " + Helper.SharePrefrence1(this).GetString("FName", null);
            }
            catch { }

        }
        private void NotesBTN_Click(object sender, EventArgs e)
        {
            //מעבר לדף שיעורים
            Intent intent = new Intent(this, typeof(ActivityNotesShow));
            StartActivity(intent);
        }

        private void PianoBTN_Click(object sender, EventArgs e)
        {
            //מעבר לנגינה חופשית
            Intent intent = new Intent(this, typeof(ActivityPiano));
            StartActivity(intent);
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