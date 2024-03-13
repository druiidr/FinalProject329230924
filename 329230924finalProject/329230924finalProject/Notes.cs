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
        [Column("IsLiked")]
        public bool IsLiked { get; set; }
        public Notes()
        {

        }
        public Notes(int NoteCode, string piecename,string NoteContent)
        {
            this.NoteCode = NoteCode;
            this.NoteContent = NoteContent;
            this.PieceName = piecename;
            this.IsLiked = false;
        }
        public int GetNoteCode()
        {
            return this.NoteCode;
        }
        public string GetNoteContent()
        {
            return this.NoteContent;
        }
        public string GetPieceName()
        {
            return this.PieceName;
        }
        public bool GetIsLiked()
        {
            return this.IsLiked;
        }
        public void Setcode(int x)
        {
            this.NoteCode = x;
        }
        public void SetContent(string x)
        {
            this.NoteContent = x;
        }
        public void SetName(string x)
        {
            this.PieceName = x;
        }
        public void SetIsLiked(bool x)
        {
            this.IsLiked = x;
        }
        public void Like()
        {
            this.IsLiked = true;
        }
        public void DisLike()
        {
            this.IsLiked = false;
        }
    }
}