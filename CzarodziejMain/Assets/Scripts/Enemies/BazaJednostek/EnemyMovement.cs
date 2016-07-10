using System;
using UnityEngine;

namespace Assets.Scripts.Enemies.BazaJednostek
{
    public class EnemyMovement :MonoBehaviour
    {
        public short BaseSpeed;
        public Vector2 DirectionVector;
        public Rigidbody2D rb;
        public Transform trans;
        public void Awake() {
             BaseSpeed = 100;
             DeltaSpeed = new MovementSpeedEnum();
             rb = GetComponent<Rigidbody2D>();
             trans = GetComponent<Transform>();
         }

        public MovementSpeedEnum DeltaSpeed;

        public void UpdateDirection() {
            var length = (float)Math.Sqrt(Math.Pow(rb.position.x, 2) + Math.Pow(rb.position.y + 1, 2));
            DirectionVector = new Vector2(-rb.position.x / length, (-rb.position.y - 1) / length) * BaseSpeed / 25;
        }

        public void Stop() {
            rb.velocity = Vector2.zero;
        }

        //TODO Symulacja trzeciego wymiaru - zmiana skalowania
        public void UpdateScale() {
            var scale = 1 / (Mathf.Sqrt(Mathf.Pow(trans.position.x, 2) / 3 + Mathf.Pow(trans.position.y, 2)) + 1.5f) + 0.2f;
            trans.localScale = trans.position.x > 0
                ? new Vector3(-scale, scale, scale)
                : new Vector3(scale, scale, scale);
            rb.velocity *= scale;
        }

        //TODO Dostosować rzeczywiste wartości do spowolnienia. Czy na pewno wszystkie jednostki jednakowo zwalniają?
        public void UpdateSpeed() {
            float aktualnaPręskość = 1;
            switch (DeltaSpeed) {
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
            rb.velocity = DirectionVector * aktualnaPręskość;
        }

        public float GetDistance() {

            return (float)Math.Sqrt(Math.Pow(trans.position.x, 2) + Math.Pow(trans.position.y + 1, 2));
        }
    }
}