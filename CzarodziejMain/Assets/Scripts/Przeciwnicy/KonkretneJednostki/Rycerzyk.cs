using BaseUnits;
using EnemyStuff;

public class Rycerzyk : Base
{
    public Rycerzyk()
    {
        BaseSpeed = 120;
        CzasUmierania = 2;
        MaxHP = 200;
        SR = SposóbRysowania.JedenKierunek;
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
