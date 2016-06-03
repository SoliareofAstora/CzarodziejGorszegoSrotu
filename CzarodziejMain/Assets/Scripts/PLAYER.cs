using GameMaster;
using Sterowanie;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private Animator Anim;
    private BoxCollider2D box;
    private Transform nos;

    public Player()
    {
        instance = this;
    }

    private void Awake()
    {
        //MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameRuler.Playing) return;
        FocusAtMouse();
        if (Stery.Wybierz(Akcja.Zakl1))
        {
        }
    }

    private void FocusAtMouse()
    {
        var lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookPos.y, lookPos.x)*Mathf.Rad2Deg - 90, Vector3.forward);
    }
}