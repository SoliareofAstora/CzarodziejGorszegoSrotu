using System;
using System.Security.Cryptography;
using Assets.Scripts.Enemies.BazaJednostek;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Jednostka zatrzymująca się przed zamkiem i rozpoczynająca atakowanie czarodzieja
/// </summary>
public class RangedUnit : EnemyBase
{
    public float range;


    private void Start()
    {
        range -= (float)new Random().Next(100)/50;

    }
    //Tu przebiega cała logika trolli
    private void Update()
    {
        switch (state)
        {
            case EnemyState.WalkingUp:
                if (Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2) < range)
                {
                    StartAtacking();
                }
                break;

            case EnemyState.Atacking:
            CzasNastępnegoAtaku.StartCounting();
            if (CzasNastępnegoAtaku.IsAfterCountDown()) {
                //TODO Strzel strzałą
            }
            break;

            case EnemyState.Dead:
                return;
               
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}