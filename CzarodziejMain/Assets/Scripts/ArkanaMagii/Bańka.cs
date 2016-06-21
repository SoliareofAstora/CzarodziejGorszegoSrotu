using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bańka : MonoBehaviour
{
	public int Wytrzumałość =200;
	private Animator anim;
	public Text Hapeki;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Wytrzumałość>0)
		{
			Hapeki.text = "Bańka's Wytrzumałość = " + Wytrzumałość;
		} else
		{
			Hapeki.text = "Bańka's Wytrzumałość = 0";
			anim.SetBool("Zepsute", true);
		}
		
	}

	public bool Hitit(int dmg)
	{
		Wytrzumałość -= dmg;
		return Wytrzumałość > 0;
	}
}
