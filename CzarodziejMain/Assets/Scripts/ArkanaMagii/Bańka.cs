using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bańka : MonoBehaviour
{

	public int HP =200;
	public Animator anim;
	public Text HAPEKI;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (HP>0)
		{
			HAPEKI.text = "Bańka's HP = " + HP;
		} else
		{
			HAPEKI.text = "Bańka's HP = 0";
			anim.SetBool("Zepsute", true);
		}
		
	}

	public bool Hitit(int dmg)
	{
		HP -= dmg;
		return HP > 0;
	}
}
