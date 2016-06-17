using GameMaster;
using Sterowanie;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
	public Text żyćko;
	public int HP=300;
	public Bańka bańka;
    public bool alive = true;
    //private Animator Anim;
    public Player()
    {
        instance = this;
    }

	private void Awake()
	{
	    bańka = GetComponentInChildren<Bańka>();
	    //MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
    }

    private void Start()
    {
      //  Anim = GetComponent<Animator>();
    }

	public void HitPlayer(int dmg)
	{
		if (bańka.Hitit(dmg))
		{
			Debug.Log("Bańka hp:" + bańka.HP);
			return;
		}
		HP -= dmg;
		Debug.Log("Czarodziej hp:"+HP);
	}

	private void Funeral()
	{
	    alive = false;
		GameRuler.instance.GameOver();

	}

	//public GameObject Fireball;
    // Update is called once per frame
    private void Update()
    {
	    żyćko.text = "żyćko = " + HP;
		if (HP > 0) {

			
			
		} else {
		    if (alive)
		    {
                Funeral();
            }
			
		}
		if (!GameRuler.Playing) return;
        FocusAtMouse();
        if (Stery.Strzel())
        {
	        //Instantiate(Fireball, transform.position, transform.rotation);
        }
    }

    private void FocusAtMouse()
    {
        var lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookPos.y, lookPos.x)*Mathf.Rad2Deg, Vector3.forward);
    }
}