using rodzajezaklęć;
using UnityEngine;

namespace zaklecie
{
    public class Zaklęcie : MonoBehaviour
    {
        public int Obrażenia;
        public RodzajeZaklęć rodzaj;
        public float Szybkość;


	    public RodzajeZaklęć GetTypeZaklęć()
        {
            return rodzaj;
        }

	    public int GetDmg()
	    {
		    return Obrażenia;
	    }
        /*
    public float rozmiar;
        public float szybkość;
        public RodzajeZaklęć rz;
 
             Dalej lecą spawnowanie, udrezanie, wybuchanie, spawnowanie innych sjawisk grywalnych
             pojawianie się obiektów na mapie

             */
        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}