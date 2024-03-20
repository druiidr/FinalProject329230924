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
        ListView lv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotesShowLayout);
            Helper.Initialize();
            Notes defaultNote1 = new Notes(1, "יונתן הקטן", "G,E,E,F,D,D,C,D,E,F,G,G,G,G,E,E,F,D,D,C,E,G,G,C,.",1);
            Notes defaultNote2 = new Notes(2, "ואיך שלא", "G,A,B,E,G,A,B,D,G,A,B,C,D,E,A,G,.",1);
            Notes defaultNote3 = new Notes(3, "stairway to heaven", "A,C,E,A,B,E,C,B,C,E,C,C,f,D,A,D,E,C,A,C,E,C,A,.",2);

            notesList = new System.Collections.Generic.List<Notes>();

            notesList.Add(defaultNote1);
            notesList.Add(defaultNote2);
            notesList.Add(defaultNote3);

            lv = FindViewById<ListView>(Resource.Id.NotesShowListviewlv);
            notesAdapter = new NotesAdapter(this, notesList);
            lv.OnItemClickListener = this;
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
        }

        private void LikeCB_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {



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