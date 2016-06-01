using UnityEngine;

namespace MyClock
{
    public class Clock
    {
        private readonly float CzasOdliczania;
        private bool odlicza;
        private float time;

        public Clock()
        {
        }

        public Clock(float cooldown)
        {
            CzasOdliczania = cooldown;
        }

        public void StartCounting(float deltaT)
        {
            if (odlicza) return;
            time = Time.time + deltaT;
            odlicza = true;
        }

        public void StartCounting()
        {
            StartCounting(CzasOdliczania);
        }

        public void CheckIfDone()
        {
            IsAfterCountDown();
        }

        public void JustStartCounting(float deltaT)
        {
            time = Time.time + deltaT;
            odlicza = true;
        }

        public bool IsBeforeCountDown()
        {
            return !IsAfterCountDown();
        }

        public bool IsAfterCountDown()
        {
            if (Time.time < time) return false;
            odlicza = false;
            return true;
        }
    }
}