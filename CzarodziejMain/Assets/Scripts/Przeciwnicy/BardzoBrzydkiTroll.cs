using BaseUnit;

public class BardzoBrzydkiTroll : JednostkaWalczącaWręcz
{
    public BardzoBrzydkiTroll()
    {
        BaseSpeed = 40;
        DługośćAnimacjiUmierania = 5;
        MaxHP = 200;
        SR = SposóbRysowania.JedenKierunek;
    }
}