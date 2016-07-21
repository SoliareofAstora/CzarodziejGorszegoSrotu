using System;
using UnityEngine;

namespace Assets.Scripts.Enemies.BazaJednostek
{
    public class EnemyMovement : MonoBehaviour
    {
        private int BaseSpeed;
        public MovementSpeedEnum DeltaSpeed;
        //Wektor jednostkowy
        public Vector2 DirectionVector;
        public Rigidbody2D rb;

        public void Awake()
        {
            BaseSpeed = 120;
            DeltaSpeed = new MovementSpeedEnum();
            rb = GetComponent<Rigidbody2D>();
        }

        public void UpdateDirection()
        {
            var length = GetDistance();
            DirectionVector = new Vector2(-rb.position.x/length, (-rb.position.y - 1)/length);
        }

        public void SetBaseSpeed(short i)
        {
            BaseSpeed = i;
        }

        public void Stop()
        {
            rb.velocity = Vector2.zero;
        }

        //Symulacja trzeciego wymiaru - zmiana skalowania
        //TODO Return float jako skala, bo ten transform nie będzie działał na większą ilość animacji
        public void UpdateScale()
        {
            var scale = 1/(Mathf.Sqrt(Mathf.Pow(transform.position.x, 2)/3 + Mathf.Pow(transform.position.y, 2)) + 1.5f) + 0.2f;
            transform.localScale = transform.position.x > 0
                ? new Vector3(-scale, scale, scale)
                : new Vector3(scale, scale, scale);
            rb.velocity *= scale;
        }

        //TODO Dostosować rzeczywiste wartości do spowolnienia. Czy na pewno wszystkie jednostki jednakowo zwalniają?
        public void UpdateSpeed()
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
            rb.velocity = DirectionVector*aktualnaPręskość*BaseSpeed/50;
        }

        public float GetDistance()
        {
            return (float) Math.Sqrt(Math.Pow(transform.position.x, 2) + Math.Pow(transform.position.y - 1, 2));
        }

        private void OnDestroy()
        {
            Destroy(rb);
        }
    }
}