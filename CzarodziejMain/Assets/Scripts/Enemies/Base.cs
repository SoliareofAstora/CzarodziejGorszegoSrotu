using System;
using UnityEngine;
using System.Collections;
using ImportantEnemyStuff;


namespace BaseUnits
{
    public class Base : MonoBehaviour
    {
        Rigidbody2D rb;
        Animator anim;

        public int MaxHP;
        public int HP;//To Private

        public int CurrentHP
        {
            get { return HP;}
        }
        short _baseSpeed;
        public SpowolnieniaRuchu DeltaSpeed=SpowolnieniaRuchu.Normalnie;
        public Podatności Podatność=Podatności.Zero;
        public Odporności Odporność=Odporności.Zero; 

        Vector2 _vektorPoczątkowy;
        public short BaseSpeed
        {
            set { _baseSpeed = value; }
            get { return _baseSpeed; }
        }
        //Sposób na spawnowanie przeciwnika
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            _vektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y-1) * _baseSpeed / 100;
            float tg = _vektorPoczątkowy.x / _vektorPoczątkowy.y;
            if (tg <-1)
            {
                anim.SetBool("WalkingLeft", true);
            }
            else if (tg>1)
            {
                anim.SetBool("WalkingRight", true);
            }
            else
            {
                anim.SetBool("WalkingDown", true);
            }
            rb.velocity = _vektorPoczątkowy;
        }

        //Eventy przebiegające przez każdąturę
        void Update()
        {
            Funeral();
            float aktualnaPręskość=1;

            switch (DeltaSpeed)
            {
                case SpowolnieniaRuchu.Normalnie:
                    aktualnaPręskość = 1;
                    break;
                case SpowolnieniaRuchu.Spowolniony:
                    aktualnaPręskość = 0.75f;
                    break;
                case SpowolnieniaRuchu.BardzoSpowolniony:
                    aktualnaPręskość = 0.5f;
                    break;
                case SpowolnieniaRuchu.Stop:
                    aktualnaPręskość = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            rb.velocity = _vektorPoczątkowy*aktualnaPręskość;
        }

        void OnTriggerEnter()
        {

            switch (Podatność)
            {
                case Podatności.Zero:
                    break;
                case Podatności.PodarnośćNaOgień:
                    break;
                case Podatności.PodatnośćNaLód:
                    break;
                case Podatności.PodatnośćNaPrąd:
                    break;
                case Podatności.PodwójnaćNaOgień:
                    break;
                case Podatności.PodwójnaćNaLód:
                    break;
                case Podatności.PodwójnaćNaPrąd:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (Odporność )
            {
                case Odporności.Zero:
                    break;
                case Odporności.OdpornośćNaOgień:
                    break;
                case Odporności.OdpornośćNaLód:
                    break;
                case Odporności.OdpornośćNaPrąd:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Funeral()
        {
            if (HP<=0)
            {
                //dead
            }
            else
            {
                
            }
        }
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. I to jest dobra myśl. */