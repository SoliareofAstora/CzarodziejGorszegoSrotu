using System.Collections.Generic;
using UnityEngine;

namespace Sterowanie
{
    public enum ActionList
    {
        A,
        B,
        C,
        D,
        Shoot1,
        Shoot2,
        Shoot3,
        Pause,
        Menue,
        Exit
    }

    public class Stery
    {
        private static Dictionary<ActionList, KeyCode> _keys;

        public Stery()
        {
            _keys = new Dictionary<ActionList, KeyCode>();

            //todo zapisywanie wybranego sterowania, przygotować sterowanie domyślne do innych typów gier
            if (true)
            {
                DefaultKeys();
            }
        }

        //Rodzaj sterowania - MOBA
        public void DefaultKeys()
        {
            _keys.Clear();
            _keys.Add(ActionList.A, KeyCode.Q);
            _keys.Add(ActionList.B, KeyCode.W);
            _keys.Add(ActionList.C, KeyCode.E);
            _keys.Add(ActionList.D, KeyCode.R);
            _keys.Add(ActionList.Shoot1, KeyCode.Mouse0);
            _keys.Add(ActionList.Shoot2, KeyCode.Mouse1);
            _keys.Add(ActionList.Shoot3, KeyCode.Space);
            _keys.Add(ActionList.Pause, KeyCode.P);
            _keys.Add(ActionList.Menue, KeyCode.M);
            _keys.Add(ActionList.Exit, KeyCode.Escape);
        }

        //Jesli klawisz jest już w użyciu, zwraca false. Jeśli można zmienić zwraca true
        public static bool ChangeKey(ActionList action, KeyCode key)
        {
            if (_keys.ContainsValue(key))
            {
                Debug.LogWarning("Klawisz jest już w użyciu");
                return false;
            }
            Debug.Log("Zmiana sterowania");
            _keys.Remove(action);
            _keys.Add(action,key);
            return true;
        }

        public static bool Shoot1()
        {
            return Input.GetKeyDown(_keys[ActionList.Shoot1]);
        }
        public static bool Shoot2() {
            return Input.GetKeyDown(_keys[ActionList.Shoot2]);
        }

        public static bool ExitGame()
        {
            return Input.GetKeyDown(_keys[ActionList.Exit]);
        }

        public static bool GoToMenue()
        {
            return Input.GetKey(_keys[ActionList.Menue]);
        }

        public static bool PauseTheGame()
        {
            return Input.GetKeyDown(_keys[ActionList.Pause]);
        }

        public static bool Select(ActionList e)
        {
            return Input.GetKeyDown(_keys[e]);
        }
    }
}