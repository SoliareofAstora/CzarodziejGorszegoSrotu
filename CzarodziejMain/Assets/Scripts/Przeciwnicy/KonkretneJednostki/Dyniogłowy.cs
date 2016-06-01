using BaseUnits;
using EnemyStuff;

public class Dyniogłowy : Base {
    public Dyniogłowy()
    {
        BaseSpeed = 10;
        CzasUmierania = 3;
        MaxHP = 100;
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