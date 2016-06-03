namespace EnemyEnums
{
    public enum DziałająceEfekty
    {
        Zero,
        Podpalenie,
        Zamrożenie,
    }
    //Spowolnienie konkretne znajduje się w Base.
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