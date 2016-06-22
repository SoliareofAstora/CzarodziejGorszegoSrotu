using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseUnit;


class Łucznik : JednostkaStrzelecka
{
    public Łucznik()
    {
        Zasięg = 9;
        BaseSpeed = 60;
        DługośćAnimacjiUmierania = 1;
        MaxHP = 200;
        SR = SposóbRysowania.JedenKierunek;
    }
}