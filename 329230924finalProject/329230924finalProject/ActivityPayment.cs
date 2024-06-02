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
        Button payBTN;
        EditText cvvET, numET, expMET, expYET;
        TextView sumTV;
        SeekBar sb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
            if(cvvET==null||numET==null||expMET==null||expYET==null)
            { 
                   Toast.MakeText(this, "please fill in your payment details properly!", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "thank you for believing chophone pro is worth "+ sumTV.Text + "$! we hope you'll enjoy it", ToastLength.Long).Show();
                //try
                //{
                var allData = Helper.dbCommand.Execute("UPDATE Customer SET doesPay = ? WHERE UName = ?", true, Helper.SharePrefrence1(this).GetString("UName", null));
                var editor = Helper.SharePrefrence1(this).Edit();
                editor.PutBoolean("doesPay", true);
                editor.Commit();
                Intent intent = new Intent(this, typeof(ActivityProfile));
                StartActivity(intent);
                //}
                //catch

                //{

                //}
            }
        
        }

        private void Sb_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            sumTV.Text = e.Progress.ToString();
        }
    }
}