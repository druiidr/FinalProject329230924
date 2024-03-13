using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace _329230924finalProject
{
   public class Customer
    {
        [PrimaryKey, Column("UName")]
        public string Uname { get; set; }
        [Column("FName")]
        public string Fname { get; set; }
        [Column("LName")]
        public string Lname { get; set; }
        [Column("email")]
        public string email { get; set; }
        [Column("phone")]
        public int phone { get; set; }
        [Column ("DOB")]
        public string DOB { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("photo")]
        public string photo { get; set; }
        public Customer()
        {

        }
        public Customer(string Uname,string Fname,string Lname,string email,int phone,string Date,string password,string photo)
        {
            this.Uname = Uname;
            this.Fname = Fname;
            this.Lname = Lname;
            this.email = email;
            this.phone = phone;
            this.DOB = Date;
            this.password = password;
            this.photo = photo;

        }
        public string GetUname()
        {
            return this.Uname;
        }
        public string GetFname()
        {
            return this.Fname;
        }
        public string GetLname()
        {
            return this.Lname;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public int GetPhone()
        {
            return this.phone;
        }
        public string GetDate()
        {
            return this.DOB;
        }
        public string GetPass()
        {
            return this.password;
        }
        public string GetPhoto()
        {
            return this.photo;
        }
        public void SetUname(string x)
        {
            this.Uname=x;
        }
        public void SetFname(string x)
        {
            this.Fname = x;
        }
        public void SetLname(string x)
        {
            this.Lname = x;
        }
        public void SetEmail(string x)
        {
            this.email = x;
        }
        public void SetDate(string x)
        {
            this.DOB = x;
        }
        public void SetPhone(int x)
        {
            this.phone = x;
        }
        public void SetPass(string x)
        {
            this.password = x;
        }
        public void SetPhoto(string x)
        {
            this.photo = x;
        }

    }
}