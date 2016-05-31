using UnityEngine;
using System.Collections;
using System;
public class PLAYER : MonoBehaviour {
    public static PLAYER instance;
    private Animator Anim;
    private Transform nos;
    
    private BoxCollider2D box;
    public PLAYER()
    {
        instance = this;
    }
    // Use this for initialization

    void Awake()
    {

    }
    void Start()
    {
        Anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
        }
    }
}
