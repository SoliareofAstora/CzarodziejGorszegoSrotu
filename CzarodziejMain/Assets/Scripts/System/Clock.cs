﻿using UnityEngine;

namespace StopWatches
{
    //Czasomież połączony ze stałym czasem 
    public class DefinedStopwatch : Stopwatch
    {
        private readonly float _DefinedCooldown;

        public DefinedStopwatch(float cooldown)
        {
            _DefinedCooldown = cooldown;
        }

        public void StartCounting()
        {
            StartCounting(_DefinedCooldown);
        }

        public void JustStartCounting()
        {
            JustStartCounting(_DefinedCooldown);
        }
    }

    public class Stopwatch
    {
        private bool _counting;
        private float _time;


        public void StartCounting(float deltaT)
        {
            if (_counting) return;
            _time = Time.time + deltaT;
            _counting = true;
        }

        public void Wait()
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
            _counting = true;
        }

        public bool IsBeforeCountDown()
        {
            return !IsAfterCountDown();
        }

        public bool IsAfterCountDown()
        {
            if (Time.time < _time) return false;
            _counting = false;
            return true;
        }
    }
}