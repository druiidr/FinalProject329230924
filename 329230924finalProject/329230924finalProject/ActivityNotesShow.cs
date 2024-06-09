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
        //הצהרה על משתנים
        public static List<Notes> notesList { get; set; }
        NotesAdapter notesAdapter;

        TextView contentTV;
        List<string> levelLsS = new List<string> { "select level", "1", "2", "3", "4", "5" };
        List<string> genreLsS = new List<string> { "select genre", "rock", "pop", "classical", "kids", "misc" };
        Button searchBTN;
        EditText searchBarET;
        ListView lv;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Idמאתחל אובייקטים, משייך ל
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotesShowLayout);

            Notes defaultNote1 = new Notes(1, "יונתן הקטן", "C,C,C,C,.", 1, "kids", false);
            Notes defaultNote2 = new Notes(2, "ואיך שלא", "G,A,B,E,G,A,B,D,G,A,B,C,D,E,A,G,.", 1, "pop", false);
            Notes defaultNote3 = new Notes(3, "stairway to heaven", "A,C,E,A,B,E,C,B,C,E,C,C,f,D,A,D,E,C,A,C,E,C,A,.", 2, "rock", false);
            Notes defaultNote4 = new Notes(4, "יונתן הקטן auc", "G,E,E,F,D,D,C,D,E,F,G,G,G,G,E,E,F,D,D,C,E,G,G,C,.", 5, "kids", false);
            Notes defaultNote5 = new Notes(5, "test", "A,B,C,D,.", 3, "kids", true);
            ArrayAdapter<string> adapterLevel = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, levelLsS);
            ArrayAdapter<string> adapterGenre = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, genreLsS);
            searchBarET = FindViewById<EditText>(Resource.Id.NotesShowSearchBarET);
            searchBTN = FindViewById<Button>(Resource.Id.NotesShowSearchBTN);
            adapterLevel.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapterGenre.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spinner levelSpinner = FindViewById<Spinner>(Resource.Id.NotewShowLevelSelectionSP);
            Spinner genreSpinner = FindViewById<Spinner>(Resource.Id.NotewShowgenreSelectionSP);

            levelSpinner.Adapter = adapterLevel;
            genreSpinner.Adapter = adapterGenre;

            notesList = new List<Notes>();
            levelSpinner.ItemSelected += LevelSpinner_ItemSelected;
            genreSpinner.ItemSelected += GenreSpinner_ItemSelected;
            searchBTN.Click += SearchBTN_Click;
            Helper.dbCommand = new SQLiteConnection(Helper.Path());
            try
            {
                //בדיקת תוכן רשימת השיעורים
                var Note = Helper.dbCommand.Get<Notes>(1);

            }
            catch (Exception ex)
            {
                //הוספת שיעורים במידה ואינם כבר כלולים
                Helper.dbCommand.Insert(defaultNote1);
                Helper.dbCommand.Insert(defaultNote2);
                Helper.dbCommand.Insert(defaultNote3);
                Helper.dbCommand.Insert(defaultNote4);
                Helper.dbCommand.Insert(defaultNote5);
            }





            lv = FindViewById<ListView>(Resource.Id.NotesShowListviewlv);
            lv.OnItemClickListener = this;
        }

        private void SearchBTN_Click(object sender, EventArgs e)
        {//חיפוש שיעורים על פי שם
            string searched=searchBarET.Text;
            try
            {
                notesList = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE PieceName Like %" +searched+"%");
                notesAdapter = new NotesAdapter(this, notesList);
                lv.Adapter = notesAdapter;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void GenreSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //מיון רשימת שיעורים על פי זאנר
            try
            {
                Spinner s = (Spinner)sender;
                string selectedGenre = s.GetItemAtPosition(e.Position).ToString();
                notesList = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE Genre=?", selectedGenre);
                notesAdapter = new NotesAdapter(this, notesList);
                lv.Adapter = notesAdapter;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void LevelSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {//מיון רשית שיעורים על פי רמת קושי
            try
            {
                notesList = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE Level=?", e.Position.ToString());
                notesAdapter = new NotesAdapter(this, notesList);
                lv.Adapter = notesAdapter;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }




        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // Get the NoteCode of the clicked item from the ListView position
            int noteCode = notesList[position].NoteCode; // Assuming NoteCode starts from 1 and increments by 1

            try
            {

                // Query the database for the note based on its NoteCode
                var thisNote = Helper.dbCommand.Query<Notes>("SELECT * FROM Notes WHERE NoteCode=?", noteCode);

                // Check if the query returned any notes
                if (thisNote != null && thisNote.Count > 0)
                {
                    // Retrieve the first note from the query result
                    Notes selectedNote = thisNote[0];
                    if (!selectedNote.IsPro || selectedNote.IsPro == Helper.SharePrefrence1(this).GetBoolean("doesPay", true))

                    {
                        // Access the NoteContent property of the selected note
                        string noteContent = selectedNote.NoteContent;

                        // Store the NoteContent in SharedPreferences
                        var composition = Helper.SharePrefrence1(this).Edit();
                        composition.PutInt("NoteCode", noteCode);
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
                else
                {
                    // Handle the case where the note with the specified NoteCode was not found
                    Toast.MakeText(this, "Thats a premium song. please upgrade to a pro account to play it!", ToastLength.Long).Show();
                }
            }
            catch
            { }
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