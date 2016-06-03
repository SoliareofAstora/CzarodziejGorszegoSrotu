using BaseUnits;
using EnemyEnums;

public class ŻuwiołakOgnia : Base
{
    public ŻuwiołakOgnia()
    {
        BaseSpeed = 10;
        MaxHP = 100;
        Odporność = Odporności.OdpornośćNaOgień;
        Podatność = Podatności.PodatnośćNaLód;
    }
}