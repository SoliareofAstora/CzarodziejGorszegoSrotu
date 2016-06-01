using UnityEngine;

public class UI:MonoBehaviour {

    public GameObject GameOver;

    public static UI access;
    public UI()
    {
        access = this;
    }
    void Awake()
    {
        GameOver.gameObject.SetActive(false);
    }
    void Start()
    {
    }
    void Update()
    {
    }



 
    

    public void Przegraleś()
    {
        GameOver.gameObject.SetActive(true);
    }
    public void Schowaj()
    {
        GameOver.gameObject.SetActive(false);
    }
}
