using gamestate;
using Sterowanie;
using UnityEngine;

namespace gamestate
{
	public enum GameState
	{
		Gameplay,
		Shop,
		Pauza,
		MainMenue
	}
}

namespace GameMaster
{
	public class GameRuler : MonoBehaviour
	{
		private GameObject[] _stateUi;
		private GameState _tempState;
		public static bool Playing { get; private set; }

		private void Awake()
		{
			// ReSharper disable once ObjectCreationAsStatement
			new Stery();
			_stateUi = new GameObject[sizeof (GameState)];
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
			GoMainMenue();
		}

		private void Update()
		{
			
			if (!Stery.ZaóbPauzę()) return;
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
				StartGameplay();
			} else if (_tempState == GameState.Shop)
			{
				GoShopping();
			} else if (_tempState == GameState.Pauza)
			{
				Debug.LogError("Coś się spiepszło z odpauzowywaniem");
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

		public void StartGameplay()
		{
			ChangheLayout(GameState.Gameplay);
			Playing = true;
		}

		public void GoShopping()
		{
		}

		public void ChangheLayout(GameState gamseState)
		{
			ResetUIlayout();
			_stateUi[(int) gamseState].SetActive(true);
		}

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