using UnityEngine;
using System.Collections;
using System;

public class SpawnRuler : MonoBehaviour {

    System.Random rand;
    [SerializeField] public GameObject[] TablicaPrzeciwników;
    public int SpawnRadius;
    private Vector2 RozmiarMapy=new Vector2(10,9);//PoleSpawnu
    Vector2 SpawnPoint;
    public Vector2 ChooseSpawnPoint()
    {
        float alfa = rand.Next(SpawnRadius) - SpawnRadius/2;
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
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Instantiate(TablicaPrzeciwników[0], ChooseSpawnPoint(), Quaternion.Euler(Vector3.zero));
        }	
	}
}
