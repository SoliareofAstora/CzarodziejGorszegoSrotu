using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainGameplay,
    Shopping,
    Pause,
    MainMenue,
}

public class GameRuler : MonoBehaviour
{
    public static GameObject asdf;

    private void Awake()
    {
        new Sterowanie();
        asdf=new GameObject() ;
        asdf = GameObject.Find("ShoppingCenter");
        CheckIfPlaying = false;
        resetUIlayout();
    }

    public static bool CheckIfPlaying { get; private set; }

    public static void ChangeUI(GameState GS)
    {
        
        switch (GS)
        {
            case GameState.MainGameplay:
                asdf.SetActive(false);
                CheckIfPlaying = true;
                break;
            case GameState.Shopping:
                break;
            case GameState.Pause:
                break;
            case GameState.MainMenue:
                break;
            default:
                throw new ArgumentOutOfRangeException("GS", GS, null);
        }
    }

    private void resetUIlayout()
    {

    }
}