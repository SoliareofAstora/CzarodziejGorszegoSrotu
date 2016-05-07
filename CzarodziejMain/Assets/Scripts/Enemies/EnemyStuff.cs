using UnityEditor;

namespace EnemyStuff
{
    //Spowolnienie konkretne znajduje się w Base.
    public enum SpowolnieniaRuchu
    {
        Normalnie,
        Spowolniony,
        BardzoSpowolniony,
        Stop
    }

    public enum Odporności
    {
        Zero,
        OdpornośćNaOgień,
        OdpornośćNaLód,
        OdpornośćNaPrąd
    }

    public enum Podatności
    {
        Zero,
        PodarnośćNaOgień,
        PodatnośćNaLód,
        PodatnośćNaPrąd,
        PodwójnaćNaOgień,
        PodwójnaćNaLód,
        PodwójnaćNaPrąd,
    }

    public enum WpływyZaklęć
    {
        Zamrożony,
        Podpalony,
        Zatruty,
        Oszołomiony,
        Czysty
    }
}