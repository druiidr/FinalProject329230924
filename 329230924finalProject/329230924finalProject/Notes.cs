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
    public class Notes
    {
        [PrimaryKey, Column("NoteCode")]
        public int NoteCode { get; set; }
        [Column("PieceName")]
        public string PieceName { get; set; }
        [Column("NoteContent")]
        public string NoteContent { get; set; }
        [Column("Level")]
        public int Level { get; set; }
        public Notes()
        {

        }
        public Notes(int NoteCode, string piecename, string NoteContent, int level)
        {
            this.NoteCode = NoteCode;
            this.NoteContent = NoteContent;
            this.PieceName = piecename;
            this.Level = level;
        }
    }
}