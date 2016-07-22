using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.System
{
    public enum Tagi
    {
        Przeciwnik,
        ZamrożonyP,
        PodpalonyP,
        MartwyPrzeciwnik,
    }


    public class Tags
    {
        public static Tags inst;
        public string[] Stringi;


        public Tags()
        {
            inst = this;
            Stringi = new string[sizeof (Tagi)];
            for (var i = 0; i < Stringi.Length; i++)
            {
                Stringi[i] = ((Tagi) i).ToString();
            }
        }
    }
}