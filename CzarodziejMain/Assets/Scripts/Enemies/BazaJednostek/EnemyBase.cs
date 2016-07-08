using System;
using Assets.Scripts.System;
using rodzajezaklęć;
using UnityEngine;
using zaklecie;
using Random = System.Random;

/*
 
     TODO
     Zabijanie przeciwników, różne update dla różnych jednostek
     Każdy rodzaj zaklęcia musi mieć osobny OnTriggerEnter
     Stworzyć tag menager.
     
     
     */




namespace Assets.Scripts.Enemies.BazaJednostek
{
    public class EnemyBase : MonoBehaviour
    {
        #region Variables

        //Renderowanie
        public RenderTypes renderType;
        public Animator anim;
        public SpriteRenderer Sprite;


        //Atrybuty jednostki       
        public EnemyState state;
        public Defence defence;
        public float DeathAnimationDuration;
        private int HP;
        public int maxHP;
        public float atackSpeed = 1;
        public int atackPower = 5;
        public short BaseSpeed;
        public MovementSpeedEnum DeltaSpeed = MovementSpeedEnum.Normalnie;

        //System
        public DefinedStopwatch CzasNastępnegoAtaku;
        public WpływCzarów Wpływ;

        //Poruszanie się
        public Rigidbody2D rb;
        public Vector2 DirectionVector;




        #endregion

        #region UnityGameplay

        private void Awake()
        {
            Wpływ = GetComponentInChildren<WpływCzarów>();
            state = new EnemyState();
            Sprite = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            HP = maxHP;
            anim.SetBool("Alive", true);
            CzasNastępnegoAtaku = new DefinedStopwatch(atackSpeed);
            UpdateVelocity();
            UstawAnimację();
        }


        //Służy do aktualizowania fizyki
        private void FixedUpdate()
        {
            if (state == EnemyState.Atacking || state == EnemyState.Dead)
            {
                return;
            }
            UpdateVelocity();
            UpdateMovementSpeed();
            if (state != EnemyState.OnTheWizardsWall)
            {
                UpdateScale();
            }
        }


        private void LateUpdate()
        {
            if (!Wpływ.StillFrozen())
            {
                anim.speed = 1;
                DeltaSpeed= MovementSpeedEnum.Normalnie;
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
            if (other.tag == "Zaklęcie" && state != EnemyState.GettingIntoCastle)
            {
                OnSpellDMG(other.GetComponentInParent<Zaklęcie>());
            }
        }

        #endregion

        #region Funkcje i proceduty


        //Zadawanie obrażeń przeciwnikowi w zależności od jego statystyk
        public void OnSpellDMG(Zaklęcie zaklęcie)
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
                    DeltaSpeed = MovementSpeedEnum.Zatrzymany;
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


        public void StartAtacking()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Atak", true);
            state = EnemyState.Atacking;
            //TODO To może być neizbędne do usunięcia podczas zabawy z wiatrem
            Destroy(rb);
        }

        public void GetIntoCastle()
        {
            state = EnemyState.OnTheWizardsWall;
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
            Destroy(gameObject,DeathAnimationDuration);
            state = EnemyState.Dead;
        }
        //Ustawia początkową prędkość. Wywoływane po rozpoczęciu
        public void UpdateVelocity()
        {
            //todo nie sprawdzać warunku, tylko to ogarnąć w innej częsci kodu.
            if (state==EnemyState.GettingIntoCastle)
            {
                return;
            }
            var length = (float) Math.Sqrt(Math.Pow(transform.position.x, 2) +Math.Pow(transform.position.y+1,2));
            DirectionVector = new Vector2(-transform.position.x/length, (-transform.position.y-1)/length)*BaseSpeed/25;
        }



        public void UstawAnimację()
        {
            switch (renderType)
            {
                case RenderTypes.JustSingleDirection:
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
                case MovementSpeedEnum.Normalnie:
                    aktualnaPręskość = 1;
                    break;
                case MovementSpeedEnum.Spowolniony:
                    aktualnaPręskość = 0.75f;
                    break;
                case MovementSpeedEnum.BardzoSpowolniony:
                    aktualnaPręskość = 0.5f;
                    break;
                case MovementSpeedEnum.Zatrzymany:
                    aktualnaPręskość = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            rb.velocity = DirectionVector*aktualnaPręskość;
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