namespace EnemyEnums
{
    public enum AkcjaPrzeciwnika
    {
        Idzie, 
        WchodziDoZamku,
        JestWZamku,
        AtakujeCzarodzieja
    }
    public enum DziałająceEfekty
    {
        Zero,
        Podpalenie,
        Zamrożenie,
    }
    //Spowolnienie konkretne znajduje się w EnemyBase.
    public enum SpowolnieniaRuchu
    {
        Normalnie,
        Spowolniony,
        BardzoSpowolniony,
        Zatrzymany
    }

    public enum SposóbRysowania
    {
        JedenKierunek
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
        PodatnośćNaPrąd
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