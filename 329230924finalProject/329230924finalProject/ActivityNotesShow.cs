using System;
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
    public class ActivityNotesShow : AppCompatActivity, ListView.IOnItemClickListener
    {
        public static List<Notes> notesList { get; set; }
        NotesAdapter notesAdapter;
        TextView contentTV;
        List<string> levelLsS = new List<string> { "1", "2", "3", "4", "5" };
        ListView lv;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotesShowLayout);
            Helper.Initialize();
            Notes defaultNote1 = new Notes(1, "יונתן הקטן", "G,E,E,F,D,D,C,D,E,F,G,G,G,G,E,E,F,D,D,C,E,G,G,C,.", 1);
            Notes defaultNote2 = new Notes(2, "ואיך שלא", "G,A,B,E,G,A,B,D,G,A,B,C,D,E,A,G,.", 1);
            Notes defaultNote3 = new Notes(3, "stairway to heaven", "A,C,E,A,B,E,C,B,C,E,C,C,f,D,A,D,E,C,A,C,E,C,A,.", 2);
            Notes defaultNote4 = new Notes(4, "יונתן הקטן auc", "G,E,E,F,D,D,C,D,E,F,G,G,G,G,E,E,F,D,D,C,E,G,G,C,.",5);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, levelLsS);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.NotewShowLevelSelectionSP);
            spinner.Adapter = adapter;
            notesList = new List<Notes>();
            spinner.ItemSelected += Spinner_ItemSelected;

            var unused = Helper.dbCommand.Insert(defaultNote1);
            Helper.dbCommand.Insert(defaultNote2);
            Helper.dbCommand.Insert(defaultNote3);
            Helper.dbCommand.Insert(defaultNote4);



            lv = FindViewById<ListView>(Resource.Id.NotesShowListviewlv);
            notesAdapter = new NotesAdapter(this, notesList);
            lv.OnItemClickListener = this;
            lv.Adapter = notesAdapter;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner s = (Spinner)sender;
            string message = String.Format("Your chosen level is {0}.", levelLsS[e.Position]);
            try
            {
                Helper.dbCommand = new SQLiteConnection(Helper.Path());
                var alldata = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE Level={0}", e.Position);
                notesList.Clear(); // Clear existing list
                foreach (var item in alldata)
                {
                    notesList.Add(item);
                }
                notesAdapter.NotifyDataSetChanged(); // Notify the adapter of data changes
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Could not load notes: " + ex.Message, ToastLength.Long).Show();
            }
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        private void LikeCB_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // Implement item click handling if needed
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menuchophone, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_login)
            {
                // Start login activity
                Intent intent = new Intent(this, typeof(ActivityLogin));
                StartActivity(intent);
            }
            else if (item.ItemId == Resource.Id.action_log_out)
            {
                // Log out logic
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
            else if (item.ItemId == Resource.Id.action_update)
            {
                // Start activity for updating details
                Intent intent = new Intent(this, typeof(ActivityUpdate));
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}