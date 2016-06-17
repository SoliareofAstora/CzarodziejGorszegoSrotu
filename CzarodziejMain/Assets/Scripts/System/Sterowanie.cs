using System.Collections.Generic;
using UnityEngine;

namespace Sterowanie
{
	public enum Akcja
	{
		Wybór1,
		Wybór2,
		Wybór3,
		Wybór4,
		Rzut1,
		Rzut2,
		Rzut3,
		Pauza,
		Menue,
		Exit
	}

	public class Stery
	{
        //TODO Zmiana sterowania za pomocą boola
		private static Dictionary<Akcja, KeyCode> stery;

		public Stery()
		{
			stery = new Dictionary<Akcja, KeyCode>();
			if (true)
			{
				UstawDomyślneStery();
			}
		}

		public void UstawDomyślneStery()
		{
			stery.Clear();
			stery.Add(Akcja.Wybór1, KeyCode.Q);
			stery.Add(Akcja.Wybór2, KeyCode.W);
			stery.Add(Akcja.Wybór3, KeyCode.E);
			stery.Add(Akcja.Wybór4, KeyCode.R);
			stery.Add(Akcja.Rzut1, KeyCode.Mouse0);
			stery.Add(Akcja.Rzut2, KeyCode.Mouse1);
			stery.Add(Akcja.Rzut3, KeyCode.Space);
			stery.Add(Akcja.Pauza, KeyCode.P);
			stery.Add(Akcja.Menue, KeyCode.M);
			stery.Add(Akcja.Exit, KeyCode.Escape);
		}

		public static bool Strzel()
		{
			return Input.GetKeyDown(stery[Akcja.Rzut1]);
		}

		public static bool WyjdźZGry()
		{
			return Input.GetKeyDown(stery[Akcja.Exit]);
		}

		public static bool GoToMenue()
		{
			return Input.GetKey(stery[Akcja.Menue]);
		}

		public static bool ZaóbPauzę()
		{
			return Input.GetKeyDown(stery[Akcja.Pauza]);
		}

		public static bool Wybierz(Akcja e)
		{
			return Input.GetKeyDown(stery[e]);
		}
	}
}