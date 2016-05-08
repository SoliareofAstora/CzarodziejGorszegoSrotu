using UnityEngine;
using System.Collections;
using BaseUnits;

public class Rycerzyk : Base {
    public Rycerzyk()
    {
        BaseSpeed = 8;
        CzasUmierania = 2;
        MaxHP = 200;

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