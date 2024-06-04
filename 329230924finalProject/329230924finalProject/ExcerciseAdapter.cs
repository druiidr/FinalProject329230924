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
    class ExcerciseAdapter : BaseAdapter
    {
        Context context;
        List<Excercise> objects;

        public ExcerciseAdapter(Context context, List<Excercise> objects)
        {
            this.context = context;
            this.objects = objects;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override int Count
        {
            get { return objects.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        { //יצירת תצוגת אובייקט בADAPTER
            LayoutInflater layoutInflater = ((ActivityProfile)context).LayoutInflater;
            var view = layoutInflater.Inflate(Resource.Layout.RecordListviewLayout, parent, false);
            TextView PiecenameTV = view.FindViewById<TextView>(Resource.Id.RecordListViewNameTV);
            TextView DetailsTV = view.FindViewById<TextView>(Resource.Id.RecordListviewMistakesTV);


            Excercise temp = objects[position];

            if (temp != null)
            {
                PiecenameTV.Text = "lesson " + temp.NoteCode + "";
                DetailsTV.Text = "Mistakes:"+temp.MistakesMade.ToString()+"" ;


            }

            return view;
        }
    }
}