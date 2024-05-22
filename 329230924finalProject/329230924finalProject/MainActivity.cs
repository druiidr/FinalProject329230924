using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.Content;
using Plugin.Media;
using Android.Graphics;
namespace _329230924finalProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button BTNLogin,BTNRegister,BTNContinue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //הגדרת כפתורים
            BTNLogin = FindViewById<Button>(Resource.Id.MainLoginBTN);
            BTNRegister = FindViewById<Button>(Resource.Id.MainRegisterBTN);
            Helper.Initialize(this);
            SQLiteConnection dbCommand = new SQLiteConnection(Helper.Path());
            var allData = dbCommand.Query<Excercise>("SELECT * FROM Excercise");
            BTNContinue = FindViewById<Button>(Resource.Id.MainCAGBTN);
            BTNContinue.Click += BTNContinue_Click;
            BTNLogin.Click += BTNLogin_Click;
            BTNRegister.Click += BTNRegister_Click;
        }

        private void BTNRegister_Click(object sender, System.EventArgs e)
        {
            //מעבר לדף הירשמות
            Intent intent = new Intent(this, typeof(ActivityRegister));
            StartActivity(intent);
        }

        private void BTNLogin_Click(object sender, System.EventArgs e)
        {
            //מעבר לדף כניסת משתמש
            Intent intent = new Intent(this, typeof(ActivityLogin));
            StartActivity(intent);
        }

        private void BTNContinue_Click(object sender, System.EventArgs e)
        {
            //מעבר לדף תוכן כאורח
            Intent intent = new Intent(this, typeof(ActivityHome));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)

        {//מייצר מניו

            this.MenuInflater.Inflate(Resource.Menu.menuchophone, menu);
            return true;

        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)

        { 
           if (item.ItemId == Resource.Id.action_update)
            {
                // Start activity for updating details
                Intent intent = new Intent(this, typeof(ActivityUpdate));
                StartActivity(intent);
            }
            if (item.ItemId == Resource.Id.action_profile)

            {
                //מעבר לדף פרופיל
                Intent intent = new Intent(this, typeof(ActivityProfile));
                StartActivity(intent);
            }
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
                editor.PutString("DOB", null);
                editor.PutString("email", null);
                editor.PutInt("phone", 0);
                editor.PutInt("winRate", 0);
                editor.PutInt("lessonsCompleted", 0);
                editor.Commit();
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        public void menumaker()
        {

        }
    }
}