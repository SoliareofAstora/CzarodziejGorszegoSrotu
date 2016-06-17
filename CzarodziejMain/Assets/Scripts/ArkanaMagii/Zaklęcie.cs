using MyClock;
using rodzajezaklęć;
using UnityEngine;

namespace zaklecie
{
	public class Zaklęcie : MonoBehaviour
	{
		private Rigidbody2D rb;
		private CircleCollider2D collider;
		private Animator anim;
		public SuperClock czaskońca;
		public int Obrażenia;
		public RodzajeZaklęć rodzaj;
		public float Szybkość;
		private bool destroyed;

		public Zaklęcie()
		{
		}

		private void Awake()
		{
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			czaskońca = new SuperClock(1);
			destroyed = false;

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
			if (!destroyed) return;
			if (czaskońca.IsAfterCountDown())
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag != "Enemy") return;
			destroyed = true;
			czaskońca.StartCounting();
			rb.velocity = Vector2.zero;
			anim.SetBool("BlowUp", true);
		}

	}
}