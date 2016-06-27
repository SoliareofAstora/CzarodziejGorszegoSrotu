using UnityEngine;
using System.Collections;
using MyClock;
using UnityEditorInternal;

public class WpływCzarów : MonoBehaviour
{
    public Animator anim;
    public Clock CzasLodu;
	// Use this for initialization
	void Start ()
	{
        CzasLodu= new Clock();
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Zamróź()
    {
        CzasLodu.StartCounting(3);
        anim.SetBool("Frozen",true);
    }

    public void Rozmróź()
    {

        anim.SetBool("Frozen",false);
    }

    public bool StillFrozen()
    {
        if (CzasLodu.IsAfterCountDown())
        {
            Rozmróź();
            return false;
        }
        return true;
    }
}
