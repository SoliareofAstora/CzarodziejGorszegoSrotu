using GameMaster;
using Sterowanie;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    //Dlaczego tu nie const?
    public float  Size =2.5f;
	public Text HitPoints;
	public int HP=300;
	public Bańka DefenseSphere;
    public bool alive = true;
    //private Animator Anim;
    public Player()
    {
        instance = this;
    }

	private void Awake()
	{
	    DefenseSphere = GetComponentInChildren<Bańka>();
	    //MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
    }

    private void Start()
    {
      //  Anim = GetComponent<Animator>();
    }

	public void HitPlayer(int dmg)
	{
		if (DefenseSphere.Hitit(dmg))
		{
			return;
		}
		HP -= dmg;
	}

	private void Funeral()
	{
	    alive = false;
		GameRuler.Instance.GameOver();
	}

	public GameObject Fireball;
    public GameObject SopelLodu;
    // Update is called once per frame
    private void Update()
    {
	    HitPoints.text = "żyćko = " + HP;
		if (HP > 0) {

			
			
		} else {
		    if (alive)
		    {
                Funeral();
            }
			
		}
		if (!GameRuler.Playing) return;
        FocusAtMouse();
        if (Stery.Shoot1())
        {
	        Instantiate(Fireball, transform.position, transform.rotation);
        }
        if (Stery.Shoot2()) {
            Instantiate(SopelLodu, transform.position, transform.rotation);
        }
    }

    private void FocusAtMouse()
    {
        var lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookPos.y, lookPos.x)*Mathf.Rad2Deg, Vector3.forward);
    }
}