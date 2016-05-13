using System.ComponentModel.Design.Serialization;
using rodzajezaklęć;

namespace zaklęcie
{
    public class Zaklęcie:PodstawoweZaklęcie
    {
         public RodzajeZaklęć GetTypeZaklęć()
         {
            return RodzajeZaklęć.KulaOgnia;
         }
    }
}