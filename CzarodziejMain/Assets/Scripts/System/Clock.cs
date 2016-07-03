using UnityEngine;

namespace MyClock
{
    //Czasomież połączony ze stałym czasem 
    public class SuperClock : Clock
    {
        private readonly float _czasOdliczania;

        public SuperClock(float cooldown)
        {
            _czasOdliczania = cooldown;
        }

        public void StartCounting()
        {
            StartCounting(_czasOdliczania);
        }

        public void JustStartCounting()
        {
            JustStartCounting(_czasOdliczania);
        }
    }

    public class Clock
    {
        private bool _odlicza;
        private float _time;


        public void StartCounting(float deltaT)
        {
            if (_odlicza) return;
            _time = Time.time + deltaT;
            _odlicza = true;
        }

        public void WaitASecond()
        {
            _time += Time.deltaTime;
        }
        public void CheckIfDone()
        {
            IsAfterCountDown();
        }

        public void JustStartCounting(float deltaT)
        {
            _time = Time.time + deltaT;
            _odlicza = true;
        }

        public bool IsBeforeCountDown()
        {
            return !IsAfterCountDown();
        }

        public bool IsAfterCountDown()
        {
            if (Time.time < _time) return false;
            _odlicza = false;
            return true;
        }
    }
}