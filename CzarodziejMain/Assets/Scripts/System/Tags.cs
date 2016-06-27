using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.System
{

    public enum Tagi
    {
        Przeciwnik,
        ZamrożonyP,
        PodpalonyP,
        MartwyPrzeciwnik,

    }



    class Tags
    {

        public static Tags inst;
        private string[] Stringi;



        public Tags()
        {
            Stringi = new string[sizeof (Tagi) + 1];
            for (var i = 0; i < Stringi.Length; i++)
            {
                Stringi[i] = ((Tagi) i).ToString();
            }
        }
    }
}
