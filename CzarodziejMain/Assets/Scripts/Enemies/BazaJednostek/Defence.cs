
using System;
using System.Collections.Generic;
using Assets.Scripts.System;

namespace Assets.Scripts.Enemies.BazaJednostek
{
    public class Defence
    {
        public Dictionary<TypeOfDefence, short> Defences;

        public Defence()
        {
            Defences = new Dictionary<TypeOfDefence, short>();
            Defences = Weather.DeltaWeatherDefence;
        }

        public float GetDefence(TypeOfDefence rodzaj)
        {

            return (float) Defences[rodzaj]/100;
        }
        //todo Czaswa zmiana odporności.
    }
}