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
        List<string> levelLsS = new List<string> { "select level", "1", "2", "3", "4", "5" };
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
            Notes defaultNote5 = new Notes(5, "test", "A,B,C,D.",3);
            Helper.Initialize();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, levelLsS);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.NotewShowLevelSelectionSP);
            spinner.Adapter = adapter;
            notesList = new List<Notes>();
            spinner.ItemSelected += Spinner_ItemSelected;

            try
            {
                var Note = Helper.dbCommand.Get<Notes>(1);
              
            }
            catch (Exception ex)
            {
                Helper.dbCommand.Insert(defaultNote1);
                Helper.dbCommand.Insert(defaultNote2);
                Helper.dbCommand.Insert(defaultNote3);
                Helper.dbCommand.Insert(defaultNote4);
                Helper.dbCommand.Insert(defaultNote5);
            }





            lv = FindViewById<ListView>(Resource.Id.NotesShowListviewlv);
            lv.OnItemClickListener = this;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner s = (Spinner)sender;
            if (e.Position > 0) // Make sure a valid level is selected (excluding "select level" option)
            {
                try
                {
                    // Query the SQLite database to fetch notes based on the selected level
                    Helper.dbCommand = new SQLiteConnection(Helper.Path());
                    var alldata = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE Level=?", e.Position.ToString());

                    // Create a new adapter with the fetched notes data
                    notesAdapter = new NotesAdapter(this, alldata);

                    // Set the adapter to the ListView
                    lv.Adapter = notesAdapter;
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Could not load notes: " + ex.Message, ToastLength.Long).Show();
                }
            }
        }



        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // Get the NoteCode of the clicked item from the ListView position
            int noteCode = position + 1; // Assuming NoteCode starts from 1 and increments by 1

            // Query the database for the note based on its NoteCode
            var thisNote = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE NoteCode=?", noteCode);

            // Check if the query returned any notes
            if (thisNote != null && thisNote.Count > 0)
            {
                // Retrieve the first note from the query result
                Notes selectedNote = thisNote[0];

                // Access the NoteContent property of the selected note
                string noteContent = selectedNote.NoteContent;

                // Store the NoteContent in SharedPreferences
                var composition = Helper.SharePrefrence1(this).Edit();
                composition.PutString("NoteContent", noteContent);
                composition.Commit();

                // Start the ActivityPiano activity
                Intent intent = new Intent(this, typeof(ActivityPiano));
                
                StartActivity(intent);
            }
            else
            {
                // Handle the case where the note with the specified NoteCode was not found
                Toast.MakeText(this, "Note not found", ToastLength.Short).Show();
            }
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
                editor.PutString("DOB", null);
                editor.PutString("email", null);
                editor.PutInt("phone", 0);
                editor.PutInt("winRate", 0);
                editor.PutInt("lessonsCompleted", 0);
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
            if (item.ItemId == Resource.Id.action_profile)

            {
                //מעבר לדף פרופיל
                if (Helper.SharePrefrence1(this).GetString("FName", null) != null)

                {
                    Intent intent = new Intent(this, typeof(ActivityProfile));
                    StartActivity(intent);
                }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}