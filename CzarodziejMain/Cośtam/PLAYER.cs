using UnityEngine;
using System.Collections;
public class PLAYER : MonoBehaviour {

    public static PLAYER instance;
    private Animator Anim;
    private Transform nos;
    public BookCzarów[] Księga;
    public GameObject[] zaklęcia;
    private BoxCollider2D box;
    public PLAYER()
    {
        instance = this;
    }
    void OnGUI()
    {
        MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
        GameObject player = MusicManager.getMusicEmitter();
    }
    void Awake(){
        GameObject player = MusicManager.getMusicEmitter();
        MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
        nos = GetComponentInChildren<Transform>();
        Księga = new BookCzarów[2];
        Księga[0] = new BookCzarów("Fire", 12, 5, 1, "FBP");
        Księga[1] = new BookCzarów("Thunder", 12, 5, 1, "ThunderBall");
    }
    void Start () {
        Anim = GetComponent<Animator>();

    }
   private void resetAnimations()
    {
        for (int i = 0; i < Księga.Length; i++)
        {
            Anim.SetBool(Księga[i].Nazwa, false);
        }
    }
    private void throwSpells()
    {
        for (int i = 0; i < Księga.Length; i++)
        {
            if (Księga[i].ThrowSpell())
            {
                Instantiate(zaklęcia[i],nos.position,transform.rotation);
            }
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Moher")
        {
            Debug.Log("Moher alert");
        }
    }
    private void CastSpell(int ID)
    {
        Anim.SetBool(Księga[ID].Nazwa, Księga[ID].CastSpell());
    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        resetAnimations();
        if (Input.GetKeyDown(KeyCode.Q)
            ||Input.GetKeyDown(KeyCode.Mouse0))          
            CastSpell(0);
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.Mouse1))   
            CastSpell(1);
        throwSpells();

    }
}

