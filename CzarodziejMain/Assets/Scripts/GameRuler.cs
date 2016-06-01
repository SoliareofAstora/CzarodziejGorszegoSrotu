using UnityEngine;

internal enum GameState
{
    MainGameplay,
    Shopping,
    Pause,
    MainMenue
}

public class GameRuler : MonoBehaviour
{
    public Canvas UI;

    private void Awake()
    {
        new Sterowanie();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Sterowanie.Wybierz(Akcja.Exit))
        {
            Application.Quit();
        }
    }
}