using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sterowanie;
using UnityEngine;

namespace Assets.Scripts.System.GameRuler
{
    public class GameRuler : MonoBehaviour
    {
        private void Awake()
        {
            new UiRuler();
            new Stery();
            new Weather();
            new Tags();
        }

        private void Update()
        {
            if (Stery.PauseTheGame())
            {
                UiRuler.Instance.SwitchPause();
            }
            if (Stery.GoToMenue())
            {
                UiRuler.Instance.GoMainMenue();
            }
            if (Stery.ExitGame())
            {
                Application.Quit();
            }
        }

        public void StartPlaying()
        {
            UiRuler.Instance.StartPlaying();
        }
    }
}