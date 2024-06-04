﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.Graphics;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android;
using Plugin.Media;
using Android.Graphics;
using SQLite;
namespace _329230924finalProject
{
 public class Helper
        {
            private static Android.Content.ISharedPreferences sp;
            private static string dbname = "dbChophone2";
            public Helper()
            {

            }
            public static string path;
            public static SQLiteConnection dbCommand;
            public static string Path()
            {
            //יצירת PATH
                path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Helper.dbname);
                return path;
            }
            public static string BitmapToBase64(Bitmap bitmap)
            {
            //מעבר מביטמפ לטקסט
                string str = "";
                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    var bytes = stream.ToArray();
                    str = Convert.ToBase64String(bytes);
                }
                return str;
            }
            public static void Initialize(Activity context)
            {
            //מבצע את הפעולה initialize

                dbCommand = new SQLiteConnection(Path());

           // dbCommand = new SQLiteConnection(Path());
            try
            {
                dbCommand.CreateTable<Customer>();
                dbCommand.CreateTable<Notes>();
                dbCommand.CreateTable<Excercise>();
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
            }
        }
            public static ISharedPreferences SharePrefrence1(Context context)
            {
            //מייצר ומנצל Shareprefrence
                sp = context.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);
                return sp;
            }

            public static Bitmap Base64ToBitmap(String base64String)
            {
            //מעבר מטקסט לביטמפ
                byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
                return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
            }
      



    }

}