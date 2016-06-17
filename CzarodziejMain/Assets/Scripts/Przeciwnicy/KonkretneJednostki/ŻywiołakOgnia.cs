using BaseUnits;
using EnemyEnums;

public class ŻuwiołakOgnia : EnemyBase
{
    public ŻuwiołakOgnia()
    {
        BaseSpeed = 10;
        MaxHP = 100;
        Odporność = Odporności.OdpornośćNaOgień;
        Podatność = Podatności.PodatnośćNaLód;
    }
}