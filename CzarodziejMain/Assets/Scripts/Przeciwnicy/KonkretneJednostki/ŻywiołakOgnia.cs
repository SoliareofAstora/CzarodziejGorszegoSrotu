﻿using UnityEngine;
using System.Collections;
using BaseUnits;
using EnemyStuff;

public class ŻuwiołakOgnia : Base {
    public ŻuwiołakOgnia()
    {
        BaseSpeed = 10;
        MaxHP = 100;
        Odporność=Odporności.OdpornośćNaOgień;
        Podatność=Podatności.PodatnośćNaLód;
    }
}
