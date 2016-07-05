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
            Player.instance.HitPlayer(atackPower);
        }
    }
    //Logika
    private void Update()
    {
        switch (state)
        {
            case EnemyState.WalkingUp:

                if (transform.position.y < 1)
                {
                    state = EnemyState.GettingIntoCastle;
                }
                break;

            case EnemyState.GettingIntoCastle:
                gameObject.tag = "Untagged";
                if (transform.position.y < -2)
                {
                    GetIntoCastle();
                }
                break;

            case EnemyState.OnTheWizardsWall:
                //Usunąć stąd Size czarodzieja
                if (transform.position.x < Player.instance.Size && transform.position.x > -Player.instance.Size)
                {
                    StartAtacking();
                }


                break;

            case EnemyState.Atacking:
                ZaatakujCzarodzieja();
                break;

            case EnemyState.Dead:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}