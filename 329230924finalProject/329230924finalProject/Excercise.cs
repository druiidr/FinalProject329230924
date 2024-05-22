﻿using System;
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
    class Excercise
    {
        [PrimaryKey, Column("UName")]
        public string Uname { get; set; }
        [PrimaryKey, Column("NoteCode")]
        public string NoteCode { get; set; }
        [Column("DatePlayed")]
        public DateTime DatePlayed { get; set; }
        [Column("MisitakesMade")]
        public int MistakesMade { get; set; }
        public Excercise()
            {}
        public Excercise(string UName,string NoteContent, DateTime DatePlayed, int MistakesMade)
        {
            this.Uname = UName;
            this.NoteCode = NoteCode;
            this.MistakesMade = MistakesMade;
            this.DatePlayed = DatePlayed;
        }

    }
}