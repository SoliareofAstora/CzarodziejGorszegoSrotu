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

    // Use this for initialization
    private void OnGUI()
    {
        MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
        var player = MusicManager.getMusicEmitter();
    }

    private void Awake()
    {
        var player = MusicManager.getMusicEmitter();
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region Obracanie_się

        var lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) -
                      transform.position;
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(lookPos.y, lookPos.x)*Mathf.Rad2Deg - 90, Vector3.forward);

        #endregion

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
        }
    }
}