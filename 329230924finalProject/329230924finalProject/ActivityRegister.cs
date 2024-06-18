
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Telephony;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using  Android.Widget;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android;
using Plugin.Media;
using Android.Graphics;
using Android.OS;
using System;
using Android.Graphics;
using Android;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityRegister")]
    public class ActivityRegister : Activity
    {
        //הצהרה על משתנים
        Random rng = new Random();
        Dialog d;
        DateTime bday;
        EditText dlgCodeET;
        int verificationCode;
        Button regBTN ,AgeInptBTN,upldBTN,dlgConfirmBTN,phoneBTN;
        ImageView pfpIV;
        bool flag = true;
        EditText FnameInptET, LnameInptET, UnameInptET, passInptET, emailInptET, phoneInptET, confirmInptET;
        int rowcount;
        readonly string[] permissionGroup =
       {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera,
                Manifest.Permission.SendSms

        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל
         
         
            base.OnCreate(savedInstanceState);
            SetTheme(Android.Resource.Style.ThemeNoTitleBar);
            SetContentView(Resource.Layout.RegisterLayout);
            regBTN = FindViewById<Button>(Resource.Id.RegisterRegistrationBTN);
            FnameInptET = FindViewById<EditText>(Resource.Id.RegisterFNameContentET);
            LnameInptET = FindViewById<EditText>(Resource.Id.RegisterLNameContentET);
            UnameInptET = FindViewById<EditText>(Resource.Id.RegisterUNameContentET);
            AgeInptBTN = FindViewById<Button>(Resource.Id.RegisterDateSelectionBTN);
            passInptET = FindViewById<EditText>(Resource.Id.RegisterPassContentET);
            emailInptET = FindViewById<EditText>(Resource.Id.RegisterEmailContentET);
            phoneInptET = FindViewById<EditText>(Resource.Id.RegisterPhoneContentET);
            phoneBTN = FindViewById<Button>(Resource.Id.RegisterphonecnfrmBTN);
            pfpIV = FindViewById<ImageView>(Resource.Id.RegisterpfpviewIV);
            upldBTN = FindViewById<Button>(Resource.Id.RegisterpfpSelectionBTN);
            confirmInptET = FindViewById<EditText>(Resource.Id.RegisterConPassContentET);
            regBTN.Click += RegBTN_Click;
            phoneBTN.Click += PhoneBTN_Click;
            AgeInptBTN.Click += AgeInptBTN_Click;
            upldBTN.Click += UpldBTN_Click;
        }

        private void PhoneBTN_Click(object sender, EventArgs e)
        {
            //הפעלת אימות טלפון
            if (Validate.ValidMail(emailInptET.Text))
            {
                verificationCode = rng.Next(1000, 10000);
                Helper.SendEmail(this, emailInptET.Text,verificationCode.ToString());
                createcnfrmDialog();
            }
            else
                Toast.MakeText(this, "Enter a valid phone", ToastLength.Short).Show();
        }

        async void UploadPhoto()
        {
            //העלאת תמונה מהמכשיר
            try
            {
                await CrossMedia.Current.Initialize();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this,ex.Message, ToastLength.Short).Show();
            }
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported on this device", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40
            });

            if (file != null)
            {
                // Convert file to byte array, bitmap, and set it to ImageView
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                pfpIV.SetImageBitmap(bitmap);
            }
            else
            {
                Toast.MakeText(this, "No photo selected", ToastLength.Short).Show();
            }
        }

        public void createcnfrmDialog()
        {
            //יצירת דיאלוג לאימות מייל
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.PhoneDialogLayout);

            d.SetTitle("cnfrm");
            d.SetCancelable(true);
            d.Show();
            dlgCodeET = d.FindViewById<EditText>(Resource.Id.PhoneDialogET);
            dlgConfirmBTN = d.FindViewById<Button>(Resource.Id.PhoneDialogBTN);
            dlgConfirmBTN.Click += DlgConfirmBTN_Click;

        }

        private void DlgConfirmBTN_Click(object sender, EventArgs e)
        {
            //אימות טלפון בתוך הדיאלוג
            dlgCodeET.Text = verificationCode.ToString();
            if (dlgCodeET.Text == verificationCode.ToString())
            {
                Toast.MakeText(this, "email confirmed", ToastLength.Long).Show();
                d.Dismiss();
            }
            else
                Toast.MakeText(this, "try again", ToastLength.Long).Show();
        }

        private void UpldBTN_Click(object sender, EventArgs e)
        {
            // טיפול בתמונה
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted ||
   CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(permissionGroup, 0);
            }
            UploadPhoto();

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
            //פתיחת דיאלוג בחרירת תאריך לידה
            string str = e.Date.ToLongDateString();
            bday = e.Date;
        Toast.MakeText(this, str, ToastLength.Long).Show();
        AgeInptBTN.Text = str;
        }

        private void RegBTN_Click(object sender, EventArgs e)
        {
            //לחיצה על כפתור רג'יסטר, ולידציה, הכנסת משתמש תקין לבסיס נתונים, מעבר לדף תוכן

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
            if (bday!=null&&!Validate.AgeRange(bday))
            {
                flag = false;
                Toast.MakeText(this, "you weren't born today!", ToastLength.Long).Show();

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
                            var editor = Helper.SharePrefrence1(this).Edit();
                            editor.PutString("password", passInptET.Text);
                            editor.PutString("UName", UnameInptET.Text);
                            editor.PutString("FName", FnameInptET.Text);
                            editor.PutString("LName", LnameInptET.Text);
                            editor.PutString("Date", AgeInptBTN.Text);
                            editor.PutString("email", emailInptET.Text);
                            editor.PutInt("phone", int.Parse(phoneInptET.Text));
                            editor.Commit();
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
            else
            {
                flag = true;
              
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
