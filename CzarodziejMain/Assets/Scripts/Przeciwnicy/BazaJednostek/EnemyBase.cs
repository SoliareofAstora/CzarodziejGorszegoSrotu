using System;
using System.Runtime.Remoting;
using MyClock;
using rodzajezaklęć;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using zaklecie;
using Random = System.Random;

/*
 
     TODO
     Zabijanie przeciwników, różne update dla różnych jednostek
     Każdy rodzaj zaklęcia musi mieć osobny OnTriggerEnter
     Stworzyć tag menager.
     
     Poruszanie się jednostki jako wektor JEDNOSTKOWY!!!
     
     
     
     */















namespace BaseUnit
{

    #region Enums & Structs

    public enum EnemyState
    {
        Idzie,
        WchodziDoZamku,
        JestWZamku,
        AtakujeCzarodzieja,
        Umarty
    }

    public enum DziałająceEfekty
    {
        Zero,
        Podpalenie,
        Zamrożenie,
    }

    public enum SpowolnieniaRuchu
    {
        Normalnie,
        Spowolniony,
        BardzoSpowolniony,
        Zatrzymany
    }

    public enum SposóbRysowania
    {
        JedenKierunek
    }

    public enum RodzajAtaku {
         ogień,
         zimno,
         prąd,
         woda,
         spowolnienie,
         pancerz
    }
    public class Podatność
    {
        public int[] Podatności;
        public Podatność()
        {
            Podatności=new int[sizeof(RodzajAtaku)+1];
        }
        public float GetOdporność(RodzajAtaku rodzaj)
        {
            //TODO W tym miejscu chyba trzeba będzie dodać wartości pogodynkoe :)
            return (float)Podatności[(int) rodzaj]/100;
        }

    }
    #endregion

    public class EnemyBase : MonoBehaviour
    {
        #region Zmienne

        //Renderowanie
        public SposóbRysowania SR;
        public Animator anim;
        public SpriteRenderer Sprite;


        //Atrybuty jednostki       
        public EnemyState state;
        public float DługośćAnimacjiUmierania;
        private int HP;
        public int MaxHP;
        public float atackspeed = 1;
        public int ZadawaneObrażenia = 5;
        public short BaseSpeed;
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;

        //System
        public Clock KlepsydraŚmierci;
        public SuperClock CzasNastępnegoAtaku;
        public WpływCzarów Wpływ;

        //Poruszanie się
        public Rigidbody2D rb;
        public Vector2 VektorPoczątkowy;




        #endregion

        #region UnityGameplay

        private void Awake()
        {
            Wpływ = GetComponentInChildren<WpływCzarów>();
            state = new EnemyState();
            Sprite = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            HP = MaxHP;
            anim.SetBool("Alive", true);
            KlepsydraŚmierci = new Clock();
            CzasNastępnegoAtaku = new SuperClock(atackspeed);
            UpdateVelocity();
            UstawAnimację();
        }


        //Służy do aktualizowania fizyki
        private void FixedUpdate()
        {
            if (state == EnemyState.AtakujeCzarodzieja || state == EnemyState.Umarty)
            {
                return;
            }
            UpdateVelocity();
            UpdateMovementSpeed();
            if (state != EnemyState.JestWZamku)
            {
                UpdateScale();
            }
        }


        private void LateUpdate()
        {
            if (!Wpływ.StillFrozen())
            {
                anim.speed = 1;
                DeltaSpeed= SpowolnieniaRuchu.Normalnie;
            }
            
            if (HP < 0)
            {
                KillIt();
            }
        }

        //Zdrezenia z innymi obiektami
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy") return;
            if (other.tag == "Zaklęcie" && state != EnemyState.WchodziDoZamku)
            {
                Oberwałem(other.GetComponentInParent<Zaklęcie>());
            }
        }

        #endregion

        #region Funkcje i proceduty


        //Zadawanie obrażeń przeciwnikowi w zależności od jego statystyk
        public void Oberwałem(Zaklęcie zaklęcie)
        {

            var Obrażenia = zaklęcie.GetDmg();
            switch (zaklęcie.GetTypeZaklęć())
            {
                case RodzajeZaklęć.KulaOgnia:
                    break;
                case RodzajeZaklęć.LodowaStrzała:
                    Wpływ.Zamróź();
                    Obrażenia = 0;
                    anim.speed = 0;
                    DeltaSpeed = SpowolnieniaRuchu.Zatrzymany;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            HP -= Obrażenia;
        }

        //Funkcja wywoływana poczas oczekiwania na pogrzeb 
        //TODO Symulacja trzeciego wymiaru - zmiana skalowania
        public void UpdateScale()
        {
            float scale = 1/(Mathf.Sqrt(Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2)) + 1.5f) + 0.2f;
            transform.localScale = transform.position.x > 0
                ? new Vector3(-scale, scale, scale)
                : new Vector3(scale, scale, scale);
            rb.velocity *= scale;

       
        }


        public void ZacznijAtakowaćCzarodzieja()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Atak", true);
            state = EnemyState.AtakujeCzarodzieja;
            //TODO To może być neizbędne do usunięcia podczas zabawy z wiatrem
            Destroy(rb);
        }

        public void GetIntoCastle()
        {
            state = EnemyState.JestWZamku;
            gameObject.tag = "Enemy";
            transform.position = new Random().Next(100) > 50
                ? new Vector3(-10, -1, 0)
                : new Vector3(10, -1, 0);
            LewoNaPrawo();
            UpdateVelocity();
            Sprite.sortingOrder = 31;
            Wpływ.GetComponent<SpriteRenderer>().sortingOrder = 32;

        }
        //Funkcja wywoływana podczas umierania
        public void KillIt() {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(rb);
            tag = "DeadEnemy";
            anim.SetBool("Alive", false);
            KlepsydraŚmierci.StartCounting(DługośćAnimacjiUmierania);
            state = EnemyState.Umarty;
        }
        public void Funeral() {
            if (KlepsydraŚmierci.IsAfterCountDown()) {
                Destroy(gameObject);
            }
        }
        //Ustawia początkową prędkość. Wywoływane po rozpoczęciu
        public void UpdateVelocity()
        {
            //todo nie sprawdzać warunku, tylko to ogarnąć w innej częsci kodu.
            if (state==EnemyState.WchodziDoZamku)
            {
                return;
            }
            var length = (float) Math.Sqrt(Math.Pow(transform.position.x, 2) +Math.Pow(transform.position.y+1,2));
            VektorPoczątkowy = new Vector2(-transform.position.x/length, (-transform.position.y-1)/length)*BaseSpeed/25;
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


        //TODO Lepsza obsługa klasy, przejścia stanów jako funkcje

        //TODO Dostosować rzeczywiste wartości do spowolnienia. Czy na pewno wszystkie jednostki jednakowo zwalniają?
        public void UpdateMovementSpeed()
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

        public float GetDistance()
        {
            return  (float)Math.Sqrt(Math.Pow(transform.position.x, 2) + Math.Pow(transform.position.y + 1, 2));
        }


        #endregion
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, 
     gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. 
     I to jest dobra myśl. 
*/