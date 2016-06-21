using System;
using UnityEngine;
using System.Collections;
using BaseUnits;

public class Wręcz : EnemyBase {



    //Tu przebiega cała logika trolli
    private void Update() {
        switch (state) {
            case EnemyState.Idzie:
            if (transform.position.y < 1) {
                state = EnemyState.WchodziDoZamku;
            }
            break;

            case EnemyState.WchodziDoZamku:
            gameObject.tag = "Untagged";
            if (transform.position.y < -2) {
                gameObject.tag = "Enemy";
                if (transform.position.x > 0) {
                    transform.position = new Vector3(-10, -1, 0);
                } else {
                    transform.position = new Vector3(10, -1, 0);
                }
                LewoNaPrawo();
                SetVelocity();
                Sprite.sortingOrder = 31;
                state = EnemyState.JestWZamku;
            }
            break;

            case EnemyState.JestWZamku:
            //TODO Usunąć stąd szerokość czarodzieja
            if (transform.position.x < 2 && transform.position.x > -2) {
                rb.velocity = Vector2.zero;
                anim.SetBool("Atak", true);
                state = EnemyState.AtakujeCzarodzieja;

                //TEST
                Destroy(rb);
            }


            break;

            case EnemyState.AtakujeCzarodzieja:
            CzasNastępnegoAtaku.StartCounting(atackspeed); //To przenieść w inne miejsca
            if (CzasNastępnegoAtaku.IsAfterCountDown()) {
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
