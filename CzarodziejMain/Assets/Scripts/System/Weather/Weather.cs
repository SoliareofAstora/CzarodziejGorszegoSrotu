using System;
using System.Collections.Generic;
using Assets.Scripts.Enemies;

namespace Assets.Scripts.System
{
    public class Weather {
        public static Dictionary<TypeOfDefence, short> DeltaWeatherDefence;

        //TODO public static Dictionary<int, short> 
        //DeltaSpell;

        private TypeOfWeather weather;

        public Weather() {
            DeltaWeatherDefence = new Dictionary<TypeOfDefence, short>();
            weather = TypeOfWeather.Normalnie;
        }

        public void UpdateWeather() {
            ResetWeather();
            switch (weather) {
                case TypeOfWeather.Normalnie:

                break;
                case TypeOfWeather.BardzoCiepło:
                DeltaWeatherDefence.Add(TypeOfDefence.Fire, 20);
                break;
                case TypeOfWeather.LekkiDeszczyk:
                DeltaWeatherDefence.Add(TypeOfDefence.Fire, -10);
                break;
                case TypeOfWeather.Ulewa:
                DeltaWeatherDefence.Add(TypeOfDefence.Fire, -30);
                break;
                case TypeOfWeather.Śnieg:
                DeltaWeatherDefence.Add(TypeOfDefence.Fire, -0);
                break;

                default:
                throw new ArgumentOutOfRangeException();
            }
        }

        public void ResetWeather() {
            DeltaWeatherDefence.Clear();
        }

        //TODO Change Weather
    }
}