using UnityEngine;

enum GameState
{
    MainGameplay,
    Shopping,
    Pause,
    MainMenue
}
public class GameRuler : MonoBehaviour
{
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
    }
}