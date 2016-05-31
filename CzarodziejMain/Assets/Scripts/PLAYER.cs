using UnityEngine;
using System.Collections;

public class PLAYER : MonoBehaviour {
    public static PLAYER instance;
    private Animator Anim;
    private Transform nos;

    private BoxCollider2D box;
    public PLAYER()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
	
	}
    void OnGUI()
    {
        MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
        GameObject player = MusicManager.getMusicEmitter();
    }
    void Awake()
    {
        GameObject player = MusicManager.getMusicEmitter();
        MusicManager.play("CzarnaMsza", 1.0f, 1.0f);
        nos = GetComponentInChildren<Transform>();
        Księga = new BookCzarów[2];
        Księga[0] = new BookCzarów("Fire", 12, 5, 1, "FBP");
        Księga[1] = new BookCzarów("Thunder", 12, 5, 1, "ThunderBall");
    }
    void Start()
    {
        Anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update () {
	
	}
}
