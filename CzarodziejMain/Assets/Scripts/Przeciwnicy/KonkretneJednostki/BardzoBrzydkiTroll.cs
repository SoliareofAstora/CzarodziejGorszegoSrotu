using UnityEngine;
using System.Collections;
using BaseUnits;
using EnemyStuff;

public class BardzoBrzydkiTroll : Base {
    public BardzoBrzydkiTroll()
    {
        BaseSpeed = 6;
        CzasUmierania = 2;
        MaxHP = 200;
        SR=SposóbRysowania.JedenKierunek;
    }
}
/*

float tg = VektorPoczątkowy.x / VektorPoczątkowy.y;
        if (tg< -1)
        {
            anim.SetBool("WalkingLeft", true);
        }
        else if (tg > 1)
        {
            anim.SetBool("WalkingRight", true);
        }
        else
        {
            anim.SetBool("WalkingDown", true);
        }
        */
