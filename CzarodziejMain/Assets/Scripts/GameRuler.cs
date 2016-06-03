using Sterowanie;
using UnityEngine;

namespace GameMaster
{
    public class GameRuler : MonoBehaviour
    {
        private static GameObject[] _stateUi;

        public static bool Playing { get; private set; }

        private void Awake()
        {
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
            Playing = false;
            StartGameplay();
        }

        // takie tam proste przełączanie pomiędzy UI. do usunięcia
        private void Update()
        {
            if (Stery.GoToMenue())
            {
                MainMenue();
            }
            if (Stery.ZaóbPauzę())
            {
                Zapauzuj();
            }
        }

        public static void Zapauzuj()
        {
            ChangheUILayout(GameState.Pauza);
            Time.timeScale = 0;//DO BAWIENIA SIĘ CZASEM!!!!
        }

        public static void Odpauzuj()
        {
            ChangheUILayout(GameState.Gameplay);
            Time.timeScale = 1;
        }
        public static void MainMenue()
        {
            ChangheUILayout(GameState.MainMenue);
        }

        public static void StartGameplay()
        {
            ChangheUILayout(GameState.Gameplay);
            Playing = true;
        }

        public static void ChangheUILayout(GameState gs)
        {
            ResetUIlayout();
            _stateUi[(int) gs].SetActive(true);
        }

        private static void ResetUIlayout()
        {
            foreach (var t in _stateUi)
            {
                t.SetActive(false);
            }
            Playing = false;
        }
    }
}