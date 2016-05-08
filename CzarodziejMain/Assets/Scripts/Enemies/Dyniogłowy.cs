using UnityEngine;
using System.Collections;
using BaseUnits;

public class Dyniogłowy : Base {
    public Dyniogłowy()
    {
        BaseSpeed = 10;
        CzasUmierania = 3;
        MaxHP = 100;

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