using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace MyClock
{
    public class Clock
    {
        private float time=0;
        private bool odlicza=false;

        public void StartCounting(float deltaT)
        {
            if (odlicza) return;
            time = Time.time + deltaT;
            odlicza = true;
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
            return !(IsAfterCountDown());
        }

        public bool IsAfterCountDown()
        {
            if (Time.time < time) return false;
            odlicza = false;
            return true;
        }
    }
}

