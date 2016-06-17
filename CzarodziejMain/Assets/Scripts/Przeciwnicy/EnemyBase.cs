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
        private Animator anim;
        private SpriteRenderer Sprite;
        public SposóbRysowania SR;
        public float DługośćAnimacjiUmierania;

        //Poruszanie się
        private Rigidbody2D rb;
        public short BaseSpeed { set; get; }//TODO średnio podoba mi się to rozwiązanie
        public bool BędzieWZamku;
        public bool JestwZamku;
        public bool atacking;
        public Vector2 VektorPoczątkowy;
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;

        //Atrybuty jednostki
        private int HP;
        public int MaxHP;
        public float atackspeed = 1;
        public int ZadawaneObrażenia;
        public Odporności Odporność = Odporności.Zero;
        public Podatności Podatność = Podatności.Zero;

        //System
        private Clock KlepsydraŚmierci;
        private Clock CzasNastępnegoAtaku;
        public Random rand;

 

        private void Awake()
        {
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

        //Tu przebiega cała logika trolli
        private void LateUpdate()
        {
            if (HP > 0)
            {
                if (atacking)
                {
                    CzasNastępnegoAtaku.StartCounting(atackspeed);//To przenieść w inne miejsca
                    if (CzasNastępnegoAtaku.IsAfterCountDown())
                    {
                        Player.instance.HitPlayer(ZadawaneObrażenia);
                        Debug.Log(gameObject.name + " HitPlayer with :" + ZadawaneObrażenia);
                    }
                    return;
                } 
               


                    UpdateMovementSpeed();
                    if (!BędzieWZamku)
                    {
                        updateScale();
                    }
                    if (transform.position.y < 1 && !JestwZamku)
                    {
                        gameObject.tag = "Untagged";

                        BędzieWZamku = true;
                        if (transform.position.y < -2)
                        {
                            gameObject.tag = "Enemy";
                            BędzieWZamku = false;
                            JestwZamku = true;
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
                            Sprite.sortingOrder = 31;//TODO Ogarnąć layery
                        }
                   
                }

            } 
            else
            //HP poniżej zera po raz pierwszy
            {

                Funeral();
            }
        }

        //Zdrezenia z innymi obiektami
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy") return;
            if (other.tag == "Zaklęcie" && !BędzieWZamku)
            {
                Oberwałem(other.GetComponentInParent<Zaklęcie>());
            }
            if (other.tag == "Player" && JestwZamku)
            {
                atacking = true;
                rb.velocity = Vector2.zero;
                anim.SetBool("Atak", true);
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
            rb.velocity = Vector2.zero;
            anim.SetBool("Alive", false);
            KlepsydraŚmierci.StartCounting(DługośćAnimacjiUmierania);
        }
        //Funkcja wywoływana poczas oczekiwania na pogrzeb 
        private void Funeral()
        {
            ////
            Destroy(GetComponent<BoxCollider2D>());
            tag = "DeadEnemy";
            rb.velocity = Vector2.zero;
            anim.SetBool("Alive", false);
            KlepsydraŚmierci.StartCounting(DługośćAnimacjiUmierania);
            ////
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

        public void LewoNaPrawo()
        {
            if (transform.position.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
            }
        }


        private void updateScale()
        {
            var Delta = (-transform.position.y + 10)/10;
            transform.localScale = new Vector3(Delta, Delta, Delta);
            //TODO usunięcia bądź zoptymalizowania
            LewoNaPrawo();
            rb.velocity *= Delta;
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

            rb.velocity = VektorPoczątkowy*aktualnaPręskość*Time.deltaTime*10;
        }

        //Zadawanie obrażeń przeciwnikowi w zależności od jego statystyk
        private void Oberwałem(Zaklęcie zaklęcie)
        {
            var Obrażenia= zaklęcie.GetDmg(); 
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