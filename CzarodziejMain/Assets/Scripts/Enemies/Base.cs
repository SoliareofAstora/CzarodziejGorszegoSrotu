﻿using UnityEngine;
using System.Collections;

namespace BaseUnits
{
    public class UnitBase : MonoBehaviour
    {
        Rigidbody2D rb;
       // Animator anim;
        short BaseSpeed;
        public short Speed
        {
            set { BaseSpeed = value; }
            get { return BaseSpeed; }
        }
        Vector2 VektorPoczątkowy;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
           // anim = GetComponent<Animator>();
            VektorPoczątkowy = new Vector2(-transform.position.x, -transform.position.y-1) * BaseSpeed / 100;
            rb.velocity = VektorPoczątkowy;
        }
        void Update()
        {
           
            rb.velocity = VektorPoczątkowy;
        }
    }
}