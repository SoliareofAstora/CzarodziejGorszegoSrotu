using System;
using EnemyEnums;
using MyClock;
using rodzajezaklęć;
using UnityEngine;
using zaklecie;
using Random = System.Random;

namespace BaseUnits
{
    public class EnemyBase : MonoBehaviour
    {
        //Renderowanie
        public SposóbRysowania SR;
        private Animator anim;
        private SpriteRenderer Sprite;


        //Atrybuty jednostki       
        public EnemyState state;
        public float DługośćAnimacjiUmierania;
        private int HP;
        public float atackspeed = 1;
        public int MaxHP;
        private int ZadawaneObrażenia = 25;      
        public short BaseSpeed;
        public Odporności Odporność = Odporności.Zero;
        public int Opancerzenie; //Odporność na ataki fizyczne
        public Podatności Podatność = Podatności.Zero;


        //System
        private Clock KlepsydraŚmierci;
        private Clock CzasNastępnegoAtaku;
        public Random rand;

        //Poruszanie się
        private Rigidbody2D rb;
       public Vector2 VektorPoczątkowy;

        
        
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;
        

        




        private void Awake()
        {
            state = new EnemyState();
            Sprite = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            HP = MaxHP;
            anim.SetBool("Alive", true);
            KlepsydraŚmierci = new Clock();
            CzasNastępnegoAtaku = new Clock();
            rand = new Random();
        }

        //Ustawienieprędkości początkowej, oraz ustawienie animacji
        private void Start()
        {
            SetVelocity();
            UstawAnimację();
        }

        //Służy do aktualizowania fizyki
        private void FixedUpdate()
        {
            if (state == EnemyState.AtakujeCzarodzieja || state == EnemyState.Umarty) return;
            UpdateMovementSpeed();
            if (state == EnemyState.Idzie) UpdateScale();
        }

        //Tu przebiega cała logika trolli
        private void Update()
        {
            switch (state)
            {
                case EnemyState.Idzie:
                    if (transform.position.y < 1)
                    {
                        state = EnemyState.WchodziDoZamku;
                    }
                    break;

                case EnemyState.WchodziDoZamku:
                    gameObject.tag = "Untagged";
                    if (transform.position.y < -2)
                    {
                        gameObject.tag = "Enemy";
                        if (rand.Next(100) > 50)
                        {
                            transform.position = new Vector3(-10, -1, 0);
                        } else
                        {
                            transform.position = new Vector3(10, -1, 0);
                        }

                        //TODO przesunięcia
                        LewoNaPrawo();
                        SetVelocity();
                        Sprite.sortingOrder = 31; //TODO Ogarnąć layery
                        state = EnemyState.JestWZamku;
                    }
                    break;

                case EnemyState.JestWZamku:
                    //TODO Usunąć stąd szerokość czarodzieja
                    if (transform.position.x < 2 && transform.position.x > -2)
                    {
                        rb.velocity = Vector2.zero;
                        anim.SetBool("Atak", true);
                        state = EnemyState.AtakujeCzarodzieja;
                    }


                    break;

                case EnemyState.AtakujeCzarodzieja:
                    CzasNastępnegoAtaku.StartCounting(atackspeed); //To przenieść w inne miejsca
                    if (CzasNastępnegoAtaku.IsAfterCountDown())
                    {
                        Player.instance.HitPlayer(ZadawaneObrażenia);
                        Debug.Log(gameObject.name + " HitPlayer with :" + ZadawaneObrażenia);
                    }
                    break;

                case EnemyState.Umarty:
                    Funeral();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LateUpdate()
        {
            if (HP < 0) {
                KillIt();
            }
        }
        //Zdrezenia z innymi obiektami
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy") return;
            if (other.tag == "Zaklęcie" && state!=EnemyState.WchodziDoZamku)
            {
                Oberwałem(other.GetComponentInParent<Zaklęcie>());
            }
        }

        //Ustawia początkową prędkość. Wywoływane po rozpoczęciu
        private void SetVelocity()
        {
            VektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y - 1)*BaseSpeed/100;
            rb.velocity = VektorPoczątkowy;
        }

        //Funkcja wywoływana podczas umierania
        private void KillIt()
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(rb);
            tag = "DeadEnemy";
            anim.SetBool("Alive", false);
            KlepsydraŚmierci.StartCounting(DługośćAnimacjiUmierania);
            state = EnemyState.Umarty;
        }

        //Funkcja wywoływana poczas oczekiwania na pogrzeb 
        private void Funeral()
        {
            if (KlepsydraŚmierci.IsAfterCountDown())
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


        //Todo zmienić nazwę
        public void LewoNaPrawo()
        {
            transform.localScale = transform.position.x > 0
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }

        //TODO Symulacja trzeciego wymiaru - zmiana skalowania
        private void UpdateScale()
        {
            var x = transform.position.x;
            var y = -transform.position.y;
            var Delta2 = 1/Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) + 1);
            transform.localScale = transform.position.x > 0
                ? new Vector3(-Delta2, Delta2, Delta2)
                : new Vector3(Delta2, Delta2, Delta2);
            rb.velocity *= Delta2;
        }


        private void UpdateMovementSpeed()
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

        //Zadawanie obrażeń przeciwnikowi w zależności od jego statystyk
        private void Oberwałem(Zaklęcie zaklęcie)
        {
            var Obrażenia = zaklęcie.GetDmg();
            switch (zaklęcie.GetTypeZaklęć())
            {
                case RodzajeZaklęć.KulaOgnia:
                    break;
                case RodzajeZaklęć.LodowaStrzała:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
            HP -= Obrażenia;
        }
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, 
     gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. 
     I to jest dobra myśl. 
*/