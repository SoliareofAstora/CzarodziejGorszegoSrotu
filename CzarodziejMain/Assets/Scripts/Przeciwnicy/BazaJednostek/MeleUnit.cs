using System;
using UnityEngine;
using System.Collections;
using BaseUnit;


/// <summary>
/// Klasa któa wchodzi do zamku i atakuje czarodzieja wręcz
/// </summary>
public class MeleUnit : EnemyBase
{

    public void ZaatakujCzarodzieja() {
        CzasNastępnegoAtaku.StartCounting();
        if (CzasNastępnegoAtaku.IsAfterCountDown()) {
            Player.instance.HitPlayer(ZadawaneObrażenia);
        }
    }
    //Logika
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
                    GetIntoCastle();
                }
                break;

            case EnemyState.JestWZamku:
                //Usunąć stąd szerokość czarodzieja
                if (transform.position.x < Player.instance.szerokość && transform.position.x > -Player.instance.szerokość)
                {
                    ZacznijAtakowaćCzarodzieja();
                }


                break;

            case EnemyState.AtakujeCzarodzieja:
                ZaatakujCzarodzieja();
                break;

            case EnemyState.Umarty:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}