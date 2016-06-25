using System;
using System.Security.Cryptography;
using BaseUnit;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Jednostka zatrzymująca się przed zamkiem i rozpoczynająca atakowanie czarodzieja
/// </summary>
public class JednostkaStrzelecka : EnemyBase
{
    public float Zasięg;


    private void Start()
    {
        Zasięg -= (float)new Random().Next(100)/100;

    }
    //Tu przebiega cała logika trolli
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idzie:
                if (Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2) < Zasięg)
                {
                    ZacznijAtakowaćCzarodzieja();
                }
                break;

            case EnemyState.AtakujeCzarodzieja:
            CzasNastępnegoAtaku.StartCounting();
            if (CzasNastępnegoAtaku.IsAfterCountDown()) {

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