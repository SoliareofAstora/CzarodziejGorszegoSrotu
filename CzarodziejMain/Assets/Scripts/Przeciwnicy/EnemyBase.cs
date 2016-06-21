using System;
using MyClock;
using rodzajezaklęć;
using UnityEngine;
using zaklecie;
using Random = System.Random;

namespace BaseUnits
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
        private Animator anim;
        private SpriteRenderer Sprite;


        //Atrybuty jednostki       
        private EnemyState state;
        public float DługośćAnimacjiUmierania;
        private int HP;
        public int MaxHP;
        public float atackspeed = 1;
        public int ZadawaneObrażenia = 25;
        public short BaseSpeed;
        public int Opancerzenie; //Odporność na ataki fizyczne
        public SpowolnieniaRuchu DeltaSpeed = SpowolnieniaRuchu.Normalnie;

        //System
        private Clock KlepsydraŚmierci;
        private Clock CzasNastępnegoAtaku;
        public Random rand;

        //Poruszanie się
        private Rigidbody2D rb;
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
            switch (state)
            {
                case EnemyState.AtakujeCzarodzieja:
                case EnemyState.Umarty:
                    return;
            }
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
                        if (transform.position.x>0)
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

                        //TEST
                        Destroy(rb);
                    }


                    break;

                case EnemyState.AtakujeCzarodzieja:
                    CzasNastępnegoAtaku.StartCounting(atackspeed); //To przenieść w inne miejsca
                    if (CzasNastępnegoAtaku.IsAfterCountDown())
                    {
                        Player.instance.HitPlayer(ZadawaneObrażenia);
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
            var Delta2 = 1/Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) + 1);
            transform.localScale = transform.position.x > 0
                ? new Vector3(-Delta2, Delta2, Delta2)
                : new Vector3(Delta2, Delta2, Delta2);
            rb.velocity *= Delta2;
        }

        //TODO Dostosować rzeczywiste wartości do spowolnienia. Czy na pewno wszystkie jednostki jednakowo zwalniają?
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
            HP -= Obrażenia;
        }

        #endregion
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, 
     gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. 
     I to jest dobra myśl. 
*/