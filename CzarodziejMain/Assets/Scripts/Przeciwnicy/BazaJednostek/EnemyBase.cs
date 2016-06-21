using System;
using MyClock;
using rodzajezaklęć;
using UnityEngine;
using zaklecie;

namespace BaseUnit
{

    #region Enums

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

    //Spowolnienie konkretne znajduje się w EnemyBase.
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
        public int ZadawaneObrażenia = 25;
        public short BaseSpeed;
        public int Opancerzenie; //Odporność na ataki fizyczne
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;

        //System
        public Clock KlepsydraŚmierci;
        public Clock CzasNastępnegoAtaku;


        //Poruszanie się
        public Rigidbody2D rb;
        public Vector2 VektorPoczątkowy;


        

        #endregion

        #region UnityGameplay

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
            SetVelocity();
            UstawAnimację();
        }


        //Służy do aktualizowania fizyki
        private void FixedUpdate()
        {
            if (state == EnemyState.AtakujeCzarodzieja || state == EnemyState.Umarty)
            {
                return;
            }
            UpdateMovementSpeed();
            if (state!=EnemyState.JestWZamku)
            {
                UpdateScale();
            }
        }


        private void LateUpdate()
        {
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
        //Funkcja wywoływana poczas oczekiwania na pogrzeb 
        //TODO Symulacja trzeciego wymiaru - zmiana skalowania
        public void UpdateScale() {
            var t = 2/ Mathf.Sqrt(Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2) + 1);
            transform.localScale = transform.position.x > 0
                ? new Vector3(-t, t, t)
                : new Vector3(t, t, t);
            rb.velocity *= t;
        }
        //Zadawanie obrażeń przeciwnikowi w zależności od jego statystyk
        public void Oberwałem(Zaklęcie zaklęcie) {
            var Obrażenia = zaklęcie.GetDmg();
            switch (zaklęcie.GetTypeZaklęć()) {
                case RodzajeZaklęć.KulaOgnia:
                break;
                case RodzajeZaklęć.LodowaStrzała:
                break;
                default:
                throw new ArgumentOutOfRangeException();
            }
            HP -= Obrażenia;
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
        public void SetVelocity()
        {
            VektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y - 1)*BaseSpeed/100;
            rb.velocity = VektorPoczątkowy;
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



        #endregion
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, 
     gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. 
     I to jest dobra myśl. 
*/