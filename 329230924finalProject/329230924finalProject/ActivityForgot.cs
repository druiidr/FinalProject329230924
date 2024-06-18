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

namespace _329230924finalProject
{
    [Activity(Label = "ActivityForgot")]
    public class ActivityForgot : Activity
    {
        //הצהרה על משתנים
        EditText UnameET,PassET, EmailET,CnfrmET;
        Button resetBTN;
        bool flag=false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל
            SetTheme(Android.Resource.Style.ThemeNoTitleBar);
            SetContentView(Resource.Layout.ForgotLayout);
            base.OnCreate(savedInstanceState);
            UnameET = FindViewById<EditText>(Resource.Id.ForgotUNameContentET);
            EmailET = FindViewById<EditText>(Resource.Id.ForgotEMailContentET);
            PassET = FindViewById<EditText>(Resource.Id.ForgotPassContentET);
           CnfrmET = FindViewById<EditText>(Resource.Id.ForgotconfirmPasswordContentET);
            resetBTN = FindViewById<Button>(Resource.Id.ForgotResetBTN);
            resetBTN.Click += ResetBTN_Click;

        }

        private void ResetBTN_Click(object sender, EventArgs e)
        {
            //מבצע אתחול סיסמא
            Helper.dbCommand = new SQLiteConnection(Helper.Path());
            try
                {
                    var allData = Helper.dbCommand.Query<Customer>("SELECT * FROM Customer WHERE email='" + EmailET.Text + "'AND UName='" + UnameET.Text + "'");
                    if (allData.Count != 0)
                    {
                    //וידוא תקינות פרטי זיהוי
                    if (!Validate.SamePass(PassET.Text, CnfrmET.Text))
                        Toast.MakeText(this, "confirmed password different from password", ToastLength.Short).Show();
                    else if (Validate.SamePass(PassET.Text, allData[0].password))
                        Toast.MakeText(this, "new password cant be the old password", ToastLength.Short).Show();
                    else
                    {
                        //עדכון הסיסמא
                        try
                        {
                            Helper.dbCommand.Execute("UPDATE Customer SET password = ?,  WHERE UName = ? ", PassET, UnameET);
                            Intent intent = new Intent(this, typeof(ActivityLogin));
                            Toast.MakeText(this, "password updated", ToastLength.Short).Show();
                            StartActivity(intent);
                        }
                        catch
                        {
                            //טיפול בחריגות
                            Toast.MakeText(this, "sql issue", ToastLength.Short).Show();
                        }
                    }
                }

                }
                catch
                {
                //טיפול בחריגות
                Toast.MakeText(this, "sql issue", ToastLength.Short).Show();
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
