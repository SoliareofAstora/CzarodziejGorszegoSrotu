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


        //System
        public PreDefinedStopwatch CzasNastępnegoAtaku;
        public WpływCzarów Wpływ;

        //Poruszanie się
        public EnemyMovement Movement;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Movement = GetComponent<EnemyMovement>();

            Wpływ = GetComponentInChildren<WpływCzarów>();
            state = EnemyState.WalkingUp;
            Sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            HP = maxHP;
            anim.SetBool("Alive", true);
            CzasNastępnegoAtaku = new PreDefinedStopwatch(atackSpeed);
            Movement.UpdateDirection();
            UstawAnimację();
        }


        //Służy do aktualizowania fizyki
        private void FixedUpdate()
        {
            if (state == EnemyState.Atacking || state == EnemyState.Dead)
            {
                return;
            }
            if (state != EnemyState.GettingIntoCastle)
            {
                Movement.UpdateDirection();
            }
            Movement.UpdateSpeed();
            if (state != EnemyState.OnTheWizardsWall)
            {
                Movement.UpdateScale();
            }
        }


        private void LateUpdate()
        {
            if (!Wpływ.StillFrozen())
            {
                anim.speed = 1;
                Movement.DeltaSpeed = MovementSpeedEnum.Normalnie;
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
                    Movement.DeltaSpeed = MovementSpeedEnum.Zatrzymany;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            HP -= Obrażenia;
        }

        //Funkcja wywoływana poczas oczekiwania na pogrzeb 


        public void StartAtacking()
        {
            Movement.Stop();
            anim.SetBool("Atak", true);
            state = EnemyState.Atacking;
            //TODO To może być neizbędne do usunięcia podczas zabawy z wiatrem
        }

        public void GetIntoCastle()
        {
            state = EnemyState.OnTheWizardsWall;
            gameObject.tag = "Enemy";
            transform.position = new Random().Next(100) > 50
                ? new Vector3(-10, -1, 0)
                : new Vector3(10, -1, 0);
            LewoNaPrawo();
            Movement.UpdateDirection();
            ChangeSpriteLayer(31);
        }

        //Funkcja wywoływana podczas umierania
        public void KillIt()
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(Movement);
            tag = "DeadEnemy";
            anim.SetBool("Alive", false);
            Destroy(gameObject, DeathAnimationDuration);
            state = EnemyState.Dead;
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

        public void ChangeSpriteLayer(int i)
        {
            Sprite.sortingOrder = i;
            Wpływ.GetComponent<SpriteRenderer>().sortingOrder = i + 1;
        }

        //Todo zmienić nazwę
        public void LewoNaPrawo()
        {
            transform.localScale = transform.position.x > 0
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }


        //TODO Lepsza obsługa klasy, przejścia stanów jako funkcje

        #endregion
    }
}

/* 
 
     Obrażenia będą musiały pochodzić z instancji księgi czarów, 
     gdzie będą też zapisane aktualnie używane zaklęcia przez czarodzieja. 
     I to jest dobra myśl. 
*/