using Assets.Scripts.System;
using rodzajezaklęć;
using UnityEngine;

namespace zaklecie
{
/*
    Rodzaj zaklęcia
        Lecący dalej na wprost
        Lecący do wyznaczonego punktu
        Globalny

     Efekt udedzenia 
        Wybuch obszarowy
        Single Target
        stworzenie obszaru z jakimś wpływem
        zmiana pogody
        single target i stworzenie odłamków innych zaklęć

    wpływ na przeciwnika
        podpalenie
        Zamrożenie
        spowolnienie
        zatrucie
        Odepchnięcie
        Wyrzucenie w górę
        Zszokowanie piorunem

    Rodzaje obszaru
        Bagno
        ognie z ziemi
        deszczora polanka
        burza z piorunami





         
         
         
         
         */
    public class Zaklęcie : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CircleCollider2D circleCollider2D;
        private Animator anim;
        public PreDefinedStopwatch czaskońca;
        public int Obrażenia;
        public RodzajeZaklęć rodzaj;

        public float Szybkość;
        private bool destroyed;

        public Zaklęcie()
        {
        }

        private void Awake()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            czaskońca = new PreDefinedStopwatch(1);
            destroyed = false;
            tag = "Zaklęcie";
        }

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

        //TODO Symulacja trzeciego wymiaru - zmiana skalowania
        private void UpdateScale()
        {
            var Delta2 = 1/Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) + 1);
            transform.localScale = new Vector3(Delta2, Delta2, Delta2);
            //rb.velocity *= Delta2;
        }

        // Use this for initialization
        private void Start()
        {
            rb.velocity = transform.right*Szybkość;
        }

        // Update is called once per frame
        private void Update()
        {
            // UpdateScale();
            if (!destroyed) return;
            if (czaskońca.IsAfterCountDown())
            {
                Destroy(gameObject);
            }
        }

        public void DestroySpell()
        {
            destroyed = true;
            //TODO Tags
            Destroy(circleCollider2D);
            czaskońca.StartCounting();
            rb.velocity = Vector2.zero;
            anim.SetBool("BlowUp", true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

        }
    }
}