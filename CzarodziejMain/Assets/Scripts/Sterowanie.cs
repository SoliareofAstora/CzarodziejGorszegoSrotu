using System.Collections.Generic;
using UnityEngine;

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

public class Sterowanie
{
    private static Dictionary<Akcja, KeyCode> stery;

    public Sterowanie()
    {
        stery = new Dictionary<Akcja, KeyCode>();
        if (true)
        {
            UstawDomyślneSterowanie();
        }
    }

    public void UstawDomyślneSterowanie()
    {
        stery.Clear();
        stery.Add(Akcja.Zakl1, KeyCode.Q);
        stery.Add(Akcja.Zakl2, KeyCode.W);
        stery.Add(Akcja.Zakl3, KeyCode.E);
        stery.Add(Akcja.Zakl4, KeyCode.R);
        stery.Add(Akcja.RzucenieZaklęcia, KeyCode.Mouse1);
        stery.Add(Akcja.RzucenieZaklęcia2, KeyCode.Mouse2);
        stery.Add(Akcja.Pauza, KeyCode.P);
        stery.Add(Akcja.Menue, KeyCode.Tab);
        stery.Add(Akcja.Exit, KeyCode.Escape);
    }

    public static bool Wybierz(Akcja e)
    {
        return Input.GetKeyDown(stery[e]);
    }
}