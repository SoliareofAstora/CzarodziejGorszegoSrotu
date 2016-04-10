using UnityEngine;
using System.Collections;

namespace BaseUnits
{
    public class UnitBase : MonoBehaviour
    {
        Rigidbody2D rb;
        Animator anim;
        short _baseSpeed;
        Vector2 _vektorPoczątkowy;
        public short BaseSpeed
        {
            set { _baseSpeed = value; }
            get { return _baseSpeed; }
        }
       

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            _vektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y-1) * _baseSpeed / 100;
            float tg = _vektorPoczątkowy.x / _vektorPoczątkowy.y;
            if (tg <-1)
            {
                anim.SetBool("WalkingLeft", true);
            }
            else if (tg>1)
            {
                anim.SetBool("WalkingRight", true);
            }
            else
            {
                anim.SetBool("WalkingDown", true);
            }
            rb.velocity = _vektorPoczątkowy;
        }
        void Update()
        {
           
            rb.velocity = _vektorPoczątkowy;
        }
    }
}