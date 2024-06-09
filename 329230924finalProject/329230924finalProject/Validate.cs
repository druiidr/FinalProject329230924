using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
namespace _329230924finalProject
{
    class Validate
    {
        public static bool ValidName(string name)//מוצא האם השם המוזן תקין
        {
            return (name.All(char.IsLetter)&&name.Length<255);
        }
        public static bool ValidPhone(string phone)//מוצא האם מס טלפון מוזן תקין
        {
            return(phone.All(char.IsDigit) && phone.Length == 10&&phone[0]=='0' && phone[1] == '5');
        }
        public static bool ValidMail(string mail)//מוצא האם כתובת אמייל מוזנת תקינה
        {
            return (Regex.IsMatch(mail, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"));
        }
        public static bool ValidPass(string password)//מוצא האם סיסמא מוזנת תקינה
        {
            return (password.Length >= 6 && password.Length <= 20 && ContainNum(password) && ContainLet(password));
        }
        public static bool ContainNum(string x)//מוצא האם מחרוזת מכילה ספרות
        {
            return (x.Any(char.IsDigit));
        }
        public static bool ContainLet(string x)//מוצא האם מחרוזת מכילה אותיות
        {
            return (x.Any(char.IsLetter));
        }
        public static bool AgeRange(DateTime x)
        {
            // Get current DateTime
            DateTime currentDate = DateTime.Now;

            // Get DateTime representing 100 years ago
            DateTime centuryAgo = currentDate.AddYears(-100);
            return (x < currentDate && x > centuryAgo);
        }
        public static bool SamePass(string p1,string p2)//בודק האם 2 מחרוזות שהוזנו זהות
        {
            if (p1.Length != p2.Length)
                return false;
            for (int i = 0; i < p1.Length; i++)
            {
                if (p1[i] != p2[i])
                    return false;
            }
            return true;
        }
    }
}