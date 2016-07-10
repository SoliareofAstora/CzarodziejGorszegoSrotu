using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Enemies.BazaJednostek;


class Łucznik : RangedUnit
{
    public Łucznik()
    {
        range = 9;
        DeathAnimationDuration = 1;
        maxHP = 200;
        renderType = RenderTypes.JustSingleDirection;
    }
}