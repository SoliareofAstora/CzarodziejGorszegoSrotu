using System;
using System.Collections.Generic;
using Sterowanie;
using UnityEngine;
using Assets.Scripts.System.GameRuler;

namespace Assets.Scripts.System
{
    public enum GameState
    {
        Gameplay,
        Shop,
        Pauza,
        MainMenue,
        GameOver
    }

    /// <summary>
    /// Klasa odpowiadająca za zmienianie UI w czasie rozgrywki.
    /// </summary>
    public class UiRuler
    {
        public static UiRuler Instance;
        private readonly Dictionary<GameState, GameObject> _stateUi;
        private GameState _tempState;
        public static bool HeroControl { get; set; }

        public UiRuler()
        {
            Instance = this;
            _stateUi = new Dictionary<GameState, GameObject>();

            //TODO Dlaczego do cholery tutaj musi być to +1 ?!?!!
            for (var i = 0; i < sizeof (GameState) + 1; i++)
            {
                if (GameObject.Find(((GameState) i).ToString()) == null)
                {
                    Debug.LogError("Zła nazwa UI -->  " + (GameState) i);
                }
                _stateUi[(GameState) i] = GameObject.Find(((GameState) i).ToString());
            }
            HeroControl = false;
            GoMainMenue();
        }


        public void SwitchPause()
        {
            if (GetCurrentState() == GameState.Pauza)
            {
                UnPause();
            } else
            {
                Pause();
            }
        }

        public GameState GetCurrentState()
        {
            for (var i = 0; i < _stateUi.Count; i++)
            {
                if (_stateUi[(GameState) i].activeSelf != true) continue;
                return (GameState) i;
            }
            return GameState.MainMenue;
        }

        public void Pause()
        {
            HeroControl = false;
            Time.timeScale = 0; //DO BAWIENIA SIĘ CZASEM!!!!
            _tempState = GetCurrentState();
            ChangheLayout(GameState.Pauza);
        }

        public void UnPause()
        {
            switch (_tempState)
            {
                case GameState.Gameplay:
                    StartPlaying();

                    break;
                case GameState.Shop:
                    GoShopping();
                    break;
                case GameState.Pauza:
                    Debug.LogError("Próbujesz zapauzować pauzę?");
                    GoMainMenue();
                    break;
                case GameState.MainMenue:
                    GoMainMenue();
                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Time.timeScale = 1;
        }

        public void GoMainMenue()
        {
            ChangheLayout(GameState.MainMenue);
        }

        public void GameOver()
        {
            ChangheLayout(GameState.GameOver);
        }

        public void StartPlaying()
        {
            ChangheLayout(GameState.Gameplay);
            HeroControl = true;
        }

        public void GoShopping()
        {
        }

        //Zamyka interfejs, następnie uruchamia jeden
        public void ChangheLayout(GameState gamseState)
        {
            ResetUIlayout();
            _stateUi[gamseState].SetActive(true);
        }

        //Zamyka wszystkie interfejsy
        private void ResetUIlayout()
        {
            HeroControl = false;
            foreach (var t in _stateUi)
            {
                t.Value.SetActive(false);
            }
        }
    }
}