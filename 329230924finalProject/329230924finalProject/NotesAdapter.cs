using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace _329230924finalProject
{
    class NotesAdapter : BaseAdapter<Notes>
    {
        Context context;
        List<Notes> objects;

        public NotesAdapter(Context context, List<Notes> objects)
        {
            this.context = context;
            this.objects = objects;
        }

        public List<Notes> GetList()
        {
            return this.objects;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override Notes this[int position]
        {
            get { return this.objects[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater = ((ActivityNotesShow)context).LayoutInflater;
            var view = layoutInflater.Inflate(Resource.Layout.Listviewlayout, parent, false);
            TextView PiecenameTV = view.FindViewById<TextView>(Resource.Id.ListViewNameTV);
            TextView ViewNotesTV = view.FindViewById<TextView>(Resource.Id.ListviewNotesTV);
            ImageView IsLikedIV = view.FindViewById<ImageView>(Resource.Id.ListviewlikedIV);
            ImageView IsUnLikedIB = view.FindViewById<ImageButton>(Resource.Id.ListviewdislikedIB);

            Notes temp = objects[position];

            if (temp != null)
            {
                PiecenameTV.Text = temp.PieceName;
                ViewNotesTV.Text = "view notes";

                // Add click event for IsLikedIV
                IsUnLikedIB.Click += delegate
                {
                    if (temp.IsLiked)
                    {
                        IsLikedIV.Visibility = Android.Views.ViewStates.Invisible;
                        temp.DisLike();
                    }
                    else
                    {
                        IsLikedIV.Visibility = Android.Views.ViewStates.Visible;
                        temp.Like();
                    }
                };
            }

            return view;
        }
    }
}
