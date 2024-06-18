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
    [Activity(Label = "ActivityLogin")]
    public class ActivityLogin : Activity, Android.Views.View.IOnClickListener
    {
        //הצהרה על משתנים
        Button LogBTN,ForgetBTN;
        EditText passET, UnameET;
        CheckBox memberberriesCB,showCB;
        public string FirName;
        public string LasName;
  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל

            base.OnCreate(savedInstanceState);
            SetTheme(Android.Resource.Style.ThemeNoTitleBar);
            SetContentView(Resource.Layout.LoginLayout);
            // Create your application here
            
            LogBTN = FindViewById<Button>(Resource.Id.LoginLogInBTN);
            ForgetBTN = FindViewById<Button>(Resource.Id.LoginIForgorBTN);
            ForgetBTN.Click += ForgetBTN_Click;
            LogBTN.SetOnClickListener(this);
            showCB = FindViewById<CheckBox>(Resource.Id.LoginShowPassCB);
            showCB.Checked = false;
            passET = FindViewById<EditText>(Resource.Id.LoginPasswordContentET);
            UnameET = FindViewById<EditText>(Resource.Id.LoginUNameContentET);
            passET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
            memberberriesCB = FindViewById<CheckBox>(Resource.Id.LoginRememberMeCB);
            showCB.CheckedChange += ShowCB_CheckedChange;

        }

        private void ShowCB_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //הצגת והסתרת סיסמא 
            if (showCB.Checked)
                passET.TransformationMethod = null;
            else
                passET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
        }

        private void ForgetBTN_Click(object sender, EventArgs e)
        {
            //מעבר לדף שינוי סיסמא
            Intent intent = new Intent(this, typeof(ActivityForgot));
            StartActivity(intent);
        }

        public void OnClick(Android.Views.View view)
        {
            //לחיצה על כפתור לוגין, שמירת פרטי משתמש בהתאם לבקשה, מעבר לדף בית
            Helper.dbCommand = new SQLiteConnection(Helper.Path());
            try
            {
                var allData = Helper.dbCommand.Query<Customer>("SELECT*FROM Customer WHERE UName='" + UnameET.Text + "'");
                if (allData.Count != 0)
                {
                    if (passET.Text == allData[0].password)
                    {
                        if (memberberriesCB.Checked)
                        {
                            var editor = Helper.SharePrefrence1(this).Edit();
                            editor.PutString("password", passET.Text);
                            editor.PutString("UName", UnameET.Text);
                            editor.PutString("FName", allData[0].Fname);
                            editor.PutString("LName", allData[0].Lname);
                            editor.PutString("DOB", allData[0].DOB);
                            editor.PutString("email", allData[0].email);
                            editor.PutInt("phone", allData[0].phone);
                            editor.PutBoolean("doesPay",allData[0].doesPay);
                            editor.Commit();
                        }
                        else
                        {
                            var editor = Helper.SharePrefrence1(this).Edit();
                            editor.PutString("password", null);
                            editor.PutString("UName", null);
                            editor.PutString("FName", null);
                            editor.PutString("LName", null);
                            editor.PutString("DOB", null);
                            editor.PutBoolean("doesPay",false);
                            editor.PutString("email", null);
                            editor.PutInt("phone", 0);
                            editor.Commit();
                        }


                        Intent intent = new Intent(this, typeof(ActivityHome));
                        StartActivity(intent);
                    }
                    else
                        Toast.MakeText(this, "wrong password", ToastLength.Long).Show();
                }
                else
                    Toast.MakeText(this, "user not found", ToastLength.Long).Show();
            }
            catch
            {
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
