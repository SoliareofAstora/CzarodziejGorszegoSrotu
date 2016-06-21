using System;
using System.Security.Cryptography;
using BaseUnit;
using UnityEngine;
using Random = System.Random;

public class JednostkaStrzelecka : EnemyBase
{
    public float Zasięg;
    public Random rand;

    private void Start()
    {
        rand = new Random();
        float i=rand.Next(100);
        i = i/100;
        Zasięg -= i;

    }
    //Tu przebiega cała logika trolli
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idzie:
                if (Mathf.Sqrt(Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2)) < Zasięg)
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
                }
                break;

            case EnemyState.Umarty:
                Funeral();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}