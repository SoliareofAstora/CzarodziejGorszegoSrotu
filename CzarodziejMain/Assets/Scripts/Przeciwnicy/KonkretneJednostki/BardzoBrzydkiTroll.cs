using BaseUnits;
using EnemyEnums;

public class BardzoBrzydkiTroll : EnemyBase
{
    public BardzoBrzydkiTroll()
    {
        BaseSpeed = 150;
        DługośćAnimacjiUmierania = 5;
        MaxHP = 200;
        SR = SposóbRysowania.JedenKierunek;
    }
}