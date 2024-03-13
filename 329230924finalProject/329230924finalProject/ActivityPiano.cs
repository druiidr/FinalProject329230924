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
    [Activity(Label = "ActivityPiano")]
    public class ActivityPiano : AppCompatActivity, android.views.viewIOnClickListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PianoLayout);

            // Create your application here
        }
    }
}