using UnityEngine;
using System.Collections;

public class BookCzarów {

    public string Nazwa;
    public int dmg;
    public float speed;
    private float Cooldown;
    public string prefab;
    private float time=0;
    float smallerTime = 0;
    public BookCzarów(){}
    public BookCzarów(string nazwa, int DMG, float SPEED, float COOLDOWN, string PREFABY)
    {
        Nazwa = nazwa;
        dmg = DMG;
        speed = SPEED;
        Cooldown = COOLDOWN;
        prefab = PREFABY;
    }
    public bool CastSpell()
    {
        if (Time.time < time) return false;
        time = Time.time + Cooldown;
        smallerTime = Time.time + 0.8f;
        Zezwolenie = true;
        return true;
    }
    bool Zezwolenie;
    public bool ThrowSpell(){
        if (Time.time < smallerTime) return false;
        if (!Zezwolenie) return false;
        Zezwolenie = false;
        return true;
    }
}
