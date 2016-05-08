using UnityEngine;
using System.Collections;
using System;

public class SpawnRuler : MonoBehaviour {

    System.Random rand;
    [SerializeField] public GameObject[] TablicaPrzeciwników;//tabliac wszystkich przeciwników którzy mogą wyjść.
    private Vector2 RozmiarMapy=new Vector2(10,9);//PoleSpawnu
    Vector2 SpawnPoint;

    /// <summary>Funkcja stawia przeciwników na około widocznej mapki</summary> 

    public Vector2 ChooseSpawnPoint()
    {
        float alfa = rand.Next(360) - 179;
        float Tangens = Mathf.Tan(alfa);
        if (Tangens > 1 || Tangens < -1)
        {
            Tangens = Mathf.Cos(alfa) / Mathf.Sin(alfa);
            if (Tangens < 0)
            {
                SpawnPoint = new Vector2(-RozmiarMapy.x, -RozmiarMapy.y * Tangens);
            }
            else
            {
                SpawnPoint = new Vector2(RozmiarMapy.x, (RozmiarMapy.y * Tangens));
            }
        }
        else
        {
            SpawnPoint = new Vector2(RozmiarMapy.x * Tangens, RozmiarMapy.y);
        }
        return SpawnPoint;
    }
    
    public static SpawnRuler instance;
    public SpawnRuler()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        rand = new System.Random();
	}
	
	// Spawnowanie przeciwników
	void Update () {
        //Stawianie przeciwników pierszego sortu naokoło mapy
        if (Input.anyKey)
        {
            var wybór = rand.Next(TablicaPrzeciwników.Length);
            Instantiate(TablicaPrzeciwników[wybór], ChooseSpawnPoint(), Quaternion.Euler(Vector3.zero));
        }	
	}
}
