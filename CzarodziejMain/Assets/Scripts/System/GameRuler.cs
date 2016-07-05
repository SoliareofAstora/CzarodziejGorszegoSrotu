
using Assets.Scripts.System;
using Sterowanie;
using UnityEngine;


namespace GameMaster
{
    public enum GameState {
        Gameplay,
        Shop,
        Pauza,
        MainMenue,
        GameOver,
    }
    /// <summary>
    /// Klasa odpowiadająca za zmienianie UI w czasie rozgrywki.
    /// </summary>
	public class GameRuler : MonoBehaviour
	{
		public static GameRuler Instance;
		public GameObject[] _stateUi;
		private GameState _tempState;
		public static bool Playing { get; private set; }

		private void Awake()
		{
			Instance = this;
			new Stery();
		  //  new Tags();





			_stateUi = new GameObject[sizeof (GameState)+1];
			for (var i = 0; i < _stateUi.Length; i++)
			{
				if (GameObject.Find(((GameState) i).ToString()) == null)
				{
					Debug.LogError("Zła nazwa UI -->  " + (GameState) i);
				}
				_stateUi[i] = GameObject.Find(((GameState) i).ToString());
			}
		}

		private void Start()
		{
			Playing = false;
			StartPlaying();
		}

		private void Update()
		{
			if (Stery.PauseTheGame())
			{
				if (GetCurrentState() == GameState.Pauza)
				{
					UnPause();
				} else
				{
					Pause();
				}

			}
			if (Stery.GoToMenue()) {
				GoMainMenue();
			}
		    if (Stery.ExitGame())
		    {
		        Application.Quit();
		    }
		}

		public GameState GetCurrentState()
		{
			for (var i = 0; i < _stateUi.Length; i++)
			{
				if (_stateUi[i].activeSelf != true) continue;
				return (GameState) i;
			}
			return GameState.MainMenue;
		}

		public void Pause()
		{
			Time.timeScale = 0; //DO BAWIENIA SIĘ CZASEM!!!!
			Playing = false;
			_tempState = GetCurrentState();
			ChangheLayout(GameState.Pauza);
		}

		public void UnPause()
		{
			if (_tempState == GameState.Gameplay)
			{
				StartPlaying();
			} else if (_tempState == GameState.Shop)
			{
				GoShopping();
			} else if (_tempState == GameState.Pauza)
			{
				Debug.LogError("Próbujesz zapauzować pauzę?");
				GoMainMenue();
			} else if (_tempState == GameState.MainMenue)
			{
				GoMainMenue();
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
			Playing = true;
		}

		public void GoShopping()
		{
		}

        //Zamyka interfejs, następnie uruchamia jeden
		public void ChangheLayout(GameState gamseState)
		{
			ResetUIlayout();
			_stateUi[(int) gamseState].SetActive(true);
		}
        //Zamyka wszystkie user interfejsy
		private void ResetUIlayout()
		{
			foreach (var t in _stateUi)
			{
				t.SetActive(false);
			}
			Playing = false;
		}
	}
}