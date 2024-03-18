using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace _329230924finalProject
{
   public class KeyboardKey
    {
        private Button pressedKeyBTN;
        private MediaPlayer keysoundMP;
    public KeyboardKey(Button pressedkeyBTN,MediaPlayer keysoundMP)
        {
            this.pressedKeyBTN=pressedkeyBTN;
            this.keysoundMP = keysoundMP;
        }
        public Button GetKey()
        {
            return this.pressedKeyBTN;
        }
        public MediaPlayer GetSound()
        {
            return this.keysoundMP;
        }
        public void PlaySound()
        {
            this.keysoundMP.Start();
        }
    }
}