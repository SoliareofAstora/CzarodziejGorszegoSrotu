using UnityEngine;
using Random = System.Random;

public class SpawnRuler : MonoBehaviour
{
    public static SpawnRuler instance;

    private Random rand;
    private Vector2 RozmiarMapy = new Vector2(10, 9); //PoleSpawnu
    private Vector2 SpawnPoint;
    [SerializeField] public GameObject[] TablicaPrzeciwników; //tabliac wszystkich przeciwników którzy mogą wyjść.

    public SpawnRuler()
    {
        instance = this;
    }

    /// <summary>Funkcja stawia przeciwników na około widocznej mapki</summary>
    public Vector2 ChooseSpawnPoint()
    {
        float alfa = rand.Next(360) - 179;
        var Tangens = Mathf.Tan(alfa);
        if (Tangens > 1 || Tangens < -1)
        {
            Tangens = Mathf.Cos(alfa)/Mathf.Sin(alfa);
            if (Tangens < 0)
            {
                SpawnPoint = new Vector2(-RozmiarMapy.x, -RozmiarMapy.y*Tangens);
            } else
            {
                SpawnPoint = new Vector2(RozmiarMapy.x, RozmiarMapy.y*Tangens);
            }
        } else
        {
            SpawnPoint = new Vector2(RozmiarMapy.x*Tangens, RozmiarMapy.y);
        }
        return SpawnPoint;
    }

    // Use this for initialization
    private void Start()
    {
        rand = new Random();
    }

    // Spawnowanie przeciwników
    private void Update()
    {
        //Stawianie przeciwników pierszego sortu naokoło mapy
        if (Input.GetKey(KeyCode.Space))
        {
            var wybór = rand.Next(TablicaPrzeciwników.Length);
            Instantiate(TablicaPrzeciwników[wybór], ChooseSpawnPoint(), Quaternion.Euler(Vector3.zero));
        }
    }
}