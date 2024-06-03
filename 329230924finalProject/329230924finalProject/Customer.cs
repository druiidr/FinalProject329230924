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
        [Column("DOB")]
        public string DOB { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("photo")]
        public string photo { get; set; }
        [Column("doesPay")]
        public bool doesPay { get; set; }
        public Customer()
        {

        }
        public Customer(string Uname, string Fname, string Lname, string email, int phone, string Date, string password, string photo)
        {
            this.Uname = Uname;
            this.Fname = Fname;
            this.Lname = Lname;
            this.email = email;
            this.phone = phone;
            this.DOB = Date;
            this.password = password;
            this.photo = photo;
            this.doesPay = false;

        }
    }
}