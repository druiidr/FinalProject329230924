﻿using System;
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
using Plugin.Media;
using Android.Graphics;


namespace _329230924finalProject
{
    [Activity(Label = "ActivityRegister")]
    public class ActivityRegister : Activity
    {
        Random rng = new Random();
        Button regBTN ,AgeInptBTN,upldBTN;
        ImageView pfpIV;
        EditText FnameInptET, LnameInptET, UnameInptET, passInptET, emailInptET, phoneInptET, confirmInptET;
        int rowcount;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);
            regBTN = FindViewById<Button>(Resource.Id.RegisterRegistrationBTN);
            FnameInptET = FindViewById<EditText>(Resource.Id.RegisterFNameContentET);
            LnameInptET = FindViewById<EditText>(Resource.Id.RegisterLNameContentET);
            UnameInptET = FindViewById<EditText>(Resource.Id.RegisterUNameContentET);
            AgeInptBTN = FindViewById<Button>(Resource.Id.RegisterDateSelectionBTN);
            passInptET = FindViewById<EditText>(Resource.Id.RegisterPassContentET);
            Helper.Initialize();
            emailInptET = FindViewById<EditText>(Resource.Id.RegisterEmailContentET);
            phoneInptET = FindViewById<EditText>(Resource.Id.RegisterPhoneContentET);
            pfpIV = FindViewById<ImageView>(Resource.Id.RegisterpfpviewIV);
            upldBTN = FindViewById<Button>(Resource.Id.RegisterpfpSelectionBTN);
            confirmInptET = FindViewById<EditText>(Resource.Id.RegisterConPassContentET);

            regBTN.Click += RegBTN_Click;
            AgeInptBTN.Click += AgeInptBTN_Click;
            upldBTN.Click += UpldBTN_Click;
        }

        private void UpldBTN_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityPFP));
            StartActivity(intent);

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
           if(!Validate.ValidName(FnameInptET.Text))
            {
                    flag = false;
                Toast.MakeText(this, "first name should contain only letters", ToastLength.Long).Show();
            }
            if (!Validate.ValidName(LnameInptET.Text))
            {
                    flag = false;
                Toast.MakeText(this, "last name should contain only letters", ToastLength.Long).Show();
            }
            if (!Validate.ValidName(UnameInptET.Text))
            {
                    flag = false;
                Toast.MakeText(this, "username should contain only letters", ToastLength.Long).Show();
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
            if(!Validate.SamePass(passInptET.Text,confirmInptET.Text))
            {
                    flag = false;
                Toast.MakeText(this, "different passwords", ToastLength.Long).Show();

            }

            if (flag)
            {

                try
                {
                    var Costumer =  Helper.dbCommand.Get<Customer>(UnameInptET.Text);
                    Toast.MakeText(this, "username already taken", ToastLength.Short).Show();
                }
                catch
                {
                    Customer newCostumer = new Customer(UnameInptET.Text, FnameInptET.Text, LnameInptET.Text, emailInptET.Text, int.Parse(phoneInptET.Text), AgeInptBTN.Text, passInptET.Text, "aa");//TBD
                    try
                    {
                        Helper.dbCommand = new SQLiteConnection(Helper.Path());
                        int rowCount = Helper.dbCommand.Insert(newCostumer);
                        if (rowCount > 0)
                        {
                            Toast.MakeText(this, "register completed", ToastLength.Short).Show();
                            Intent intent = new Intent(this, typeof(ActivityHome));

                            StartActivity(intent);
                        }
                        else
                        {
                            Toast.MakeText(this, "there had been a problem connecting to the server", ToastLength.Short).Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, "code problem", ToastLength.Short).Show();
                    }
                }

            }
        }
    }
}