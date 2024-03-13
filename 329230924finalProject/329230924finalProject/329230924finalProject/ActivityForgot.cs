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
        EditText UnameET,PassET, EmailET,CnfrmET;
        Button resetBTN;
        bool flag=false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
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
            Helper.dbCommand = new SQLiteConnection(Helper.Path());
            try
                {
                    var allData = Helper.dbCommand.Query<Customer>("SELECT*FROM Customer WHERE email='" + EmailET.Text + "'AND UName='" + UnameET.Text + "'");
                    if (allData.Count != 0)
                    {
                    if (!Validate.SamePass(PassET.Text, CnfrmET.Text))
                        Toast.MakeText(this, "confirmed password different from password", ToastLength.Short).Show();
                    else if (Validate.SamePass(PassET.Text, allData[0].password))
                        Toast.MakeText(this, "new password cant be the old password", ToastLength.Short).Show();
                    else
                    {
                        allData[0].SetPass(PassET.Text);
                        Intent intent = new Intent(this, typeof(ActivityLogin));
                        Toast.MakeText(this, "password updated", ToastLength.Short).Show();
                        StartActivity(intent);
                    }
                }

                }
                catch
                {
                    Toast.MakeText(this, "sql issue", ToastLength.Short).Show();
                }
            }
        }
}