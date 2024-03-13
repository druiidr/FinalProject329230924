﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using SQLite;
using Android.Views;
namespace _329230924finalProject
{
    [Activity(Label = "ActivityNotesFavourites")]
    public class ActivityNotesShow :AppCompatActivity, ListView.IOnItemClickListener, ListView.IOnItemLongClickListener
    {
        public static List<Notes> notesList { get; set; }
        NotesAdapter notesAdapter;
        CheckBox likeCB;
        ImageView heartiv;
        TextView contentTV;
        ListView lv;
        Dialog d;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotesShowLayout);
            Helper.Initialize();
            Notes defaultNote1 = new Notes(1, "יונתן הקטן", "g,e,e,f,d,d,c,d,e,f,g,g,g,g,e,e,f,d,d,c,e,g,g,c");
            Notes defaultNote2 = new Notes(2, "ואיך שלא", "g,a,b,e,g,a,b,d,g,a,b,c,d,e,a,g");
            Notes defaultNote3 = new Notes(3, "stairway to heaven", "a,c,e,a,b,e,c,b,c,e,c,c,f#,d,a,d,e,c,a,c,e,c,a");
            likeCB = FindViewById<CheckBox>(Resource.Id.NotesShowLikedCB);
            notesList = new System.Collections.Generic.List<Notes>();

            notesList.Add(defaultNote1);
            notesList.Add(defaultNote2);
            notesList.Add(defaultNote3);

            lv = FindViewById<ListView>(Resource.Id.NotesShowListviewlv);
            notesAdapter = new NotesAdapter(this, notesList);
            lv.OnItemLongClickListener = this;
            lv.OnItemClickListener = this;
            likeCB.CheckedChange += LikeCB_CheckedChange;
            lv.Adapter = notesAdapter;
            try
            {
                Helper.dbCommand = new SQLiteConnection(Helper.Path());
                var alldata = Helper.dbCommand.Query<Notes>("");
                    alldata = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes");
                foreach (var items in alldata)
                {
                    notesList.Add(items);
                }
                notesAdapter = new NotesAdapter(this, notesList);
                lv.Adapter = notesAdapter;
            }
            catch
            {
                Toast.MakeText(this, "couldnt load notes", ToastLength.Long).Show();
            }
            // Create your application here
        }

        private void LikeCB_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            try
            {
                lv.Adapter = null;
                Helper.dbCommand = new SQLiteConnection(Helper.Path());
                var alldata = Helper.dbCommand.Query<Notes>("");
                alldata = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE IsLiked = TRUE");
                foreach (var items in alldata)
                {
                    notesList.Add(items);
                }
                notesAdapter = new NotesAdapter(this, notesList);
                lv.Adapter = notesAdapter;
            }
            catch
            {
                Toast.MakeText(this, "couldnt load notes", ToastLength.Long).Show();
            }
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {



        }
        public bool OnItemLongClick(AdapterView parent, View view,int position,long id )
        {

            d = new Dialog(this);
            d.SetContentView(Resource.Layout.DialogLayout);
            d.SetTitle("note Content");
            d.SetCancelable(true);
            contentTV = FindViewById<TextView>(Resource.Id.DialogContentTV);
            contentTV.Text = notesAdapter[position].NoteContent;
            d.Show();
                        return true;
        }



        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)

        {//מייצר מניו

            MenuInflater.Inflate(Resource.Menu.menuchophone, menu);
            return true;

        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)

        {

            if (item.ItemId == Resource.Id.action_login)

            {
                //מעבר לדף כניסת משתמש
                Intent intent = new Intent(this, typeof(ActivityLogin));
                StartActivity(intent);
            }

            else if (item.ItemId == Resource.Id.action_log_out)

            {
                //התנתקות
                var editor = Helper.SharePrefrence1(this).Edit();
                editor.PutString("password", null);
                editor.PutString("UName", null);
                editor.PutString("FName", null);
                editor.PutString("LName", null);
                editor.PutString("Date", null);
                editor.PutString("email", null);
                editor.PutInt("phone", 0);
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