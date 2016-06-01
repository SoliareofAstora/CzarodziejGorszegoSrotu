using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public static PLAYER instance;
    private Animator Anim;
    private BoxCollider2D box;
    private Transform nos;

    public PLAYER()
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
        FocusAtMouse();
        if (Sterowanie.Wybierz(Akcja.Zakl1))
        {
        }
    }

    private void FocusAtMouse()
    {
        var lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookPos.y, lookPos.x)*Mathf.Rad2Deg - 90, Vector3.forward);
    }
}