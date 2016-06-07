using rodzajezaklęć;
using UnityEngine;

namespace zaklecie
{
    public class Zaklęcie : MonoBehaviour
    {
	    private Rigidbody2D rb;
	    private CircleCollider2D collider;
	    private Animator anim;
        public int Obrażenia;
        public RodzajeZaklęć rodzaj;
        public float Szybkość;

	    public Zaklęcie()
	    {
		}

	    private void Awake()
	    {
		    anim = GetComponent<Animator>();
		    rb = GetComponent<Rigidbody2D>();

	    }
	    public RodzajeZaklęć GetTypeZaklęć()
        {
            return rodzaj;
        }

	    public int GetDmg()
	    {
		    return Obrażenia;
	    }
        /*
    public float rozmiar;
        public float szybkość;
        public RodzajeZaklęć rz;
 
             Dalej lecą spawnowanie, udrezanie, wybuchanie, spawnowanie innych sjawisk grywalnych
             pojawianie się obiektów na mapie

             */
        // Use this for initialization
        private void Start()
        {
	        rb.velocity = transform.right*Szybkość;
        }

        // Update is called once per frame
        private void Update()
        {

        }

	    private void OnTriggerEnter2D(Collider2D other)
	    {
			anim.SetBool("BlowUp",true);
	    }

    }
}