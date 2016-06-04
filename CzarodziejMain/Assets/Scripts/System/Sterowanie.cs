using System.Collections.Generic;
using UnityEngine;

namespace Sterowanie
{
	public enum Akcja
	{
		Zakl1,
		Zakl2,
		Zakl3,
		Zakl4,
		RzucenieZaklęcia,
		RzucenieZaklęcia2,
		Pauza,
		Menue,
		Exit
	}

	public class Stery
	{
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
			stery.Add(Akcja.Zakl1, KeyCode.Q);
			stery.Add(Akcja.Zakl2, KeyCode.W);
			stery.Add(Akcja.Zakl3, KeyCode.E);
			stery.Add(Akcja.Zakl4, KeyCode.R);
			stery.Add(Akcja.RzucenieZaklęcia, KeyCode.Mouse1);
			stery.Add(Akcja.RzucenieZaklęcia2, KeyCode.Mouse2);
			stery.Add(Akcja.Pauza, KeyCode.P);
			stery.Add(Akcja.Menue, KeyCode.M);
			stery.Add(Akcja.Exit, KeyCode.Escape);
		}

		public static bool Strzel()
		{
			return Input.GetKeyDown(stery[Akcja.RzucenieZaklęcia]);
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