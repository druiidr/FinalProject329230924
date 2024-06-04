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
    }
}