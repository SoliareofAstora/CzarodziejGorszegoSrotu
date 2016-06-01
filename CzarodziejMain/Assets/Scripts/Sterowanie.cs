using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Akcje
{
    Zakl1,
    Zakl2,
    Zakl3,
    Zakl4,
    RzucenieZaklęcia,
    RzucenieZaklęcia2,
    Pauza,
    Menue,
    EXIT
}

public class Sterowanie
{
    public static Sterowanie Graj=null;

    public static Dictionary<Akcje, KeyCode> stery; 

    public Sterowanie()
    {
        Graj = this;
        stery =new Dictionary<Akcje, KeyCode>();
        stery.Add(Akcje.Zakl1,KeyCode.Q);
        stery.Add(Akcje.Zakl2, KeyCode.W);


        stery.Add(Akcje.RzucenieZaklęcia, KeyCode.Mouse1);

        stery.Add(Akcje.Pauza, KeyCode.P);
        stery.Add(Akcje.Menue, KeyCode.Tab);
        stery.Add(Akcje.EXIT, KeyCode.Escape);

    }

    public static bool Wybierz(Akcje e)
    {
        return Input.GetKeyDown(stery[e]);
    }

}
    
