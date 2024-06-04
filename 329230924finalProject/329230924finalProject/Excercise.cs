using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace _329230924finalProject
{
    [Table("Excercise")]
    public  class Excercise
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int id { get; set; }
        [Column("UName")]
        public string Uname { get; set; }
        [Column("NoteCode")]
        public int NoteCode { get; set; }
        [Column("DatePlayed")]
        public DateTime DatePlayed { get; set; }
        [Column("MisitakesMade")]
        public int MistakesMade { get; set; }
        public Excercise()

        {
        }
        public Excercise(string UName,int NoteCode, DateTime DatePlayed, int MistakesMade)
        {
            //פעולה בונה
            this.Uname = UName;
            this.NoteCode = NoteCode;
            this.MistakesMade = MistakesMade;
            this.DatePlayed = DatePlayed;
        }

    }
}