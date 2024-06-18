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
using SQLite;
using Android.Views;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPayment")]
    public class ActivityPayment : Activity 
    {
        //הצהרה על משתנים
        Button payBTN;
        EditText cvvET, numET, expMET, expYET;
        TextView sumTV;
        SeekBar sb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל
            base.OnCreate(savedInstanceState);
            SetTheme(Android.Resource.Style.ThemeNoTitleBar);
            SetContentView(Resource.Layout.PaymentLayout);
            payBTN=FindViewById<Button>(Resource.Id.PaymentSubscriptionBTN);
            cvvET = FindViewById<EditText>(Resource.Id.paymentCVVET);
            numET = FindViewById<EditText>(Resource.Id.paymentCardNumET);
            expMET = FindViewById<EditText>(Resource.Id.paymentExpDateMET);
           expYET = FindViewById<EditText>(Resource.Id.paymentExpDateYET);
            sb = FindViewById<SeekBar>(Resource.Id.PaymentSB);
            sumTV = FindViewById<TextView>(Resource.Id.PaymentSumTV);
            sb.Progress = 100;
            sb.ProgressChanged += Sb_ProgressChanged;
            payBTN.Click += PayBTN_Click;
            // Create your application here
        }

        private void PayBTN_Click(object sender, EventArgs e)
        {
            //מבצע את הליך התשלום 
            if(cvvET.Text!=""&&Validate.ContainLet(cvvET.Text)|| numET.Text != "" && Validate.ContainLet(numET.Text)|| expMET.Text != "" && Validate.ContainLet(expMET.Text)|| expYET.Text != "" && Validate.ContainLet(expYET.Text))
            { 
                //מטפל בשגיאות בהזנת פרטי תשלום
                   Toast.MakeText(this, "please fill in your payment details properly!", ToastLength.Short).Show();
            }
            else
            {
                //"רושם את הלקוח לפרו ומבצע "תשלום
                Toast.MakeText(this, "thank you for believing chophone pro is worth "+ sumTV.Text + "$! we hope you'll enjoy it", ToastLength.Long).Show();
                try
                {
                    var allData = Helper.dbCommand.Execute("UPDATE Customer SET doesPay = ? WHERE UName = ?", true, Helper.SharePrefrence1(this).GetString("UName", null));
                    var editor = Helper.SharePrefrence1(this).Edit();
                    editor.PutBoolean("doesPay", true);
                    editor.Commit();
                    Intent intent = new Intent(this, typeof(ActivityProfile));
                    StartActivity(intent);
                }
                catch

                {

                }
            }

        }

        private void Sb_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            //בחירת סכום לתשלום
            sumTV.Text ="$"+ e.Progress.ToString();
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