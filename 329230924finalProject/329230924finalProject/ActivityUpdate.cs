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
using Android.Support.V7.App;
using Android;
using Plugin.Media;
using Android.Graphics;



namespace _329230924finalProject
{
    [Activity(Label = "ActivityUpdate")]
    public class ActivityUpdate : Activity
    { 
   Random rng = new Random();
    Button regBTN, AgeInptBTN, upldBTN;
    ImageView pfpIV;
    TextView UnameInptTV;
    EditText FnameInptET, LnameInptET, passInptET, emailInptET, phoneInptET, confirmInptET;
    int rowcount;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.UpdateLayout);
        regBTN = FindViewById<Button>(Resource.Id.updateUpdatingBTN);
        FnameInptET = FindViewById<EditText>(Resource.Id.UpdateFNameContentET);
        LnameInptET = FindViewById<EditText>(Resource.Id.UpdateLNameContentET);
        UnameInptTV = FindViewById<TextView>(Resource.Id.UpdateUNameContentTV);
        AgeInptBTN = FindViewById<Button>(Resource.Id.UpdateDateSelectionBTN);
        passInptET = FindViewById<EditText>(Resource.Id.UpdatePassContentET);
        emailInptET = FindViewById<EditText>(Resource.Id.UpdateEmailContentET);
        phoneInptET = FindViewById<EditText>(Resource.Id.UpdatePhoneContentET);
        pfpIV = FindViewById<ImageView>(Resource.Id.UpdatepfpviewIV);
        upldBTN = FindViewById<Button>(Resource.Id.UpdatepfpSelectionBTN);
        confirmInptET = FindViewById<EditText>(Resource.Id.UpdateConPassContentET);
            try
            {
                FnameInptET.Text = Helper.SharePrefrence1(this).GetString("FName", null);
               LnameInptET.Text =  Helper.SharePrefrence1(this).GetString("LName", null);
                UnameInptTV.Text =  Helper.SharePrefrence1(this).GetString("UName", null);
                emailInptET.Text =  Helper.SharePrefrence1(this).GetString("email", null);
                phoneInptET.Text = Helper.SharePrefrence1(this).GetString("phone", null);
                AgeInptBTN.Text = Helper.SharePrefrence1(this).GetString("DOB", null);
                passInptET.Text = Helper.SharePrefrence1(this).GetString("password", null);
                pfpIV.SetImageBitmap(Helper.Base64ToBitmap(Helper.SharePrefrence1(this).GetString("photo", null)));

            }
            catch

            { Toast.MakeText(this, "username must be the same", ToastLength.Short).Show(); }
        regBTN.Click += RegBTN_Click;
        AgeInptBTN.Click += AgeInptBTN_Click;
        upldBTN.Click += UpldBTN_Click;
    }

    private void UpldBTN_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AgeInptBTN_Click(object sender, EventArgs e)
    {
        //יצירת דיאלוג לבחירת גיל
        DateTime today = DateTime.Today;
        DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
        datePickerDialog.Show();
    }
    void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
    {

        String str = e.Date.ToLongDateString();
        Toast.MakeText(this, str, ToastLength.Long).Show();
        AgeInptBTN.Text = str;
    }

    private void RegBTN_Click(object sender, EventArgs e)
    {
        //לחיצה על כפתור רג'יסטר, ולידציה, הכנסת משתמש תקין לבסיס נתונים, מעבר לדף תוכן
        bool flag = true;
        if (!Validate.ValidName(FnameInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "first name should contain only letters", ToastLength.Long).Show();
        }
        if (!Validate.ValidName(LnameInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "last name should contain only letters", ToastLength.Long).Show();
        }
        if (!Validate.ValidPhone(phoneInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "phone number invalid", ToastLength.Long).Show();
        }
        if (!Validate.ValidMail(emailInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "invalid mail", ToastLength.Long).Show();
        }
        if (!Validate.ValidPass(passInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "password must be 6<x<20 charachters long, and contain both numbers and letters", ToastLength.Long).Show();
        }
        if (!Validate.SamePass(passInptET.Text, confirmInptET.Text))
        {
            flag = false;
            Toast.MakeText(this, "different passwords", ToastLength.Long).Show();

        }

            if (flag)
            {
                string path = Helper.Path();
                var editor = Helper.SharePrefrence1(this).Edit();
                SQLiteConnection dbcommand = new SQLiteConnection(path);
                var cus = dbcommand.Get<Customer>(UnameInptTV.Text);
                try
                {
                    dbcommand.Query<Customer>("UPDATE Customer SET email = '" + emailInptET.Text + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");
                    dbcommand.Query<Customer>("UPDATE Customer SET FName = '" + FnameInptET.Text + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");
                    dbcommand.Query<Customer>("UPDATE Customer SET LName = '" + LnameInptET.Text + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");
                    dbcommand.Query<Customer>("UPDATE Customer SET DOB = '" + AgeInptBTN.Text + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");
                    dbcommand.Query<Customer>("UPDATE Customer SET password = '" + passInptET.Text + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");
                    dbcommand.Query<Customer>("UPDATE Customer SET phone = '" + int.Parse(phoneInptET.Text) + "' WHERE Uname = '" + Helper.SharePrefrence1(this).GetString("Uname", null) + "'");

                    editor.PutString("password", passInptET.Text);
                    editor.PutString("FName", FnameInptET.Text);
                    editor.PutString("LName", LnameInptET.Text);
                    editor.PutString("DOB", AgeInptBTN.Text);
                    editor.PutString("email", emailInptET.Text);
                    editor.PutInt("phone", int.Parse(phoneInptET.Text));
                    editor.Commit();
                    Toast.MakeText(this, "GREAT SUCCSESS!!!", ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(ActivityHome));
                    StartActivity(intent);

                }
                catch

                { Toast.MakeText(this, "sql issue", ToastLength.Short).Show(); }
            }
        }
    }
}