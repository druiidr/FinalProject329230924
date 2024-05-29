using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPayment")]
    public class ActivityPayment : Activity 
    {
        Button payBTN;
        EditText cvvET, numET, expMET, expYET;
        SeekBar sb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PaymentLayout);
            payBTN=FindViewById<Button>(Resource.Id.PaymentSubscriptionBTN);
            sb = FindViewById<SeekBar>(Resource.Id.PaymentSB);
            sb.Progress = 100;
            // Create your application here
        }
    }
}