using System.ComponentModel.Design.Serialization;
using MyClock;
using rodzajezaklęć;

namespace zaklęcie
{
    public class Zaklęcie:PodstawoweZaklęcie
    {
        private RodzajeZaklęć rodzaj;
        public string Nazwa;
        public int Obrażenia;
        public float Szybkość;
        private float CzasOdnowienia;
        

        public RodzajeZaklęć GetTypeZaklęć()
         {
            return RodzajeZaklęć.KulaOgnia;
         }
    }
}