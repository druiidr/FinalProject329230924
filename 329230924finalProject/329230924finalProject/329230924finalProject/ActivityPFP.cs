using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using SQLite;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android;
using Plugin.Media;
using Android.Graphics;

namespace _329230924finalProject
{
    [Activity(Label = "ActivityPFP")]
    public class ActivityPFP : Activity
    { 
        Button captureBTN,confirmBTN, uploadBTN;
        ImageView thisImageView;
        readonly string[] permissionGroup =
       {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PFPLayout);

            // Create your application here

            captureBTN = (Button)FindViewById(Resource.Id.PFPcaptureBTN);
            uploadBTN = (Button)FindViewById(Resource.Id.PFPuploadPhotoBTN);
            thisImageView = (ImageView)FindViewById(Resource.Id.thisImageView);

            captureBTN.Click += CaptureButton_Click;
            uploadBTN.Click += UploadButton_Click;
            RequestPermissions(permissionGroup, 0);

        }

        private void UploadButton_Click(object sender, System.EventArgs e)
        {
            UploadPhoto();
        }

        private void CaptureButton_Click(object sender, System.EventArgs e)
        {
            TakePhoto();
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = "myimage.jpg",
                Directory = "sample"

            });

            if (file == null)
            {
                return;
            }

            // Convert file to byte array and set the resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            thisImageView.SetImageBitmap(bitmap);

        }

        async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

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

            // Convert file to byre array, to bitmap and set it to our ImageView

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            thisImageView.SetImageBitmap(bitmap);

        }
    }

}