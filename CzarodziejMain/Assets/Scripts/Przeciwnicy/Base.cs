using System;
using EnemyStuff;
using MyClock;
using rodzajezaklęć;
using UnityEngine;
using zaklecie;

namespace BaseUnits
{
    public class Base : MonoBehaviour
    {
        public Animator anim;
        public float CzasUmierania;
        private Clock CzasZgonu;
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;
        public int HP; //To Private
        public int MaxHP;
        public Odporności Odporność = Odporności.Zero;
        public Podatności Podatność = Podatności.Zero;
        private Rigidbody2D rb;
        public SposóbRysowania SR;
        public Vector2 VektorPoczątkowy;

        public int CurrentHP { get { return HP; } }

        public short BaseSpeed { set; get; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            HP = MaxHP;
            anim.SetBool("Alive", true);
            CzasZgonu = new Clock();
        }

        //Sposób na spawnowanie przeciwnika
        private void Start()
        {
            VektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y - 1)*BaseSpeed/100;
            rb.velocity = VektorPoczątkowy;
            UstawAnimację();
        }

        private void Funeral()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Alive", false);
            CzasZgonu.StartCounting(CzasUmierania);
            if (CzasZgonu.IsAfterCountDown())
            {
                Destroy(gameObject);
            }
        }

        public void UstawAnimację()
        {
            switch (SR)
            {
                case SposóbRysowania.JedenKierunek:
                    LewoNaPrawo();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void LewoNaPrawo()
        {
            if (transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //Eventy przebiegające przez każdąturę
        private void Update()
        {
            if (HP > 0)
            {
                updateMovementSpeed();
            } else
            {
                Funeral();
            }
        }

        private void updateMovementSpeed()
        {
            float aktualnaPręskość = 1;
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
                case SpowolnieniaRuchu.Zatrzymany:
                    aktualnaPręskość = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            rb.velocity = VektorPoczątkowy*aktualnaPręskość;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Zaklęcie")
            {
                JednorazoweObrażenia(other.GetComponentInParent<Zaklęcie>().GetTypeZaklęć());
            }
        }

        private void JednorazoweObrażenia(RodzajeZaklęć Rodzaj)
        {
            switch (Rodzaj)
            {
                case RodzajeZaklęć.KulaOgnia:
                    HP = 0;
                    break;
                case RodzajeZaklęć.LodowaStrzała:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Rodzaj", Rodzaj, null);
            }
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (Odporność)
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
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. I to jest dobra myśl. */