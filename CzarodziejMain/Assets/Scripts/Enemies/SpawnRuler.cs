using UnityEngine;
using UnityEngine.UI;
using Sterowanie;
using UnityEditor;
using Random = System.Random;

public class SpawnRuler : MonoBehaviour
{
    public static SpawnRuler instance;

    public SpawnRuler()
    {
        instance = this;
    }

    public Text tekst;
    private int i;
    public int seed_0_to_100 = 100;
    private Random rand;
    public Vector2 RozmiarMapy = new Vector2(10, 10); //Obszar spawnu

    //tabliac wszystkich przeciwników którzy mogą wyjść.
    [SerializeField] public GameObject[] TablicaPrzeciwników;

    // Spawnowanie przeciwników
    private void Update()
    {
        //Stawianie przeciwników pierszego sortu naokoło mapy
        if (Input.GetKey(KeyCode.Space))
        {
            //todo inny warunek
            if (!(new Random().Next(100) < seed_0_to_100))
            {
                return;
            }
            //Wybór losowego przeciwnika
            var wybór = rand.Next(TablicaPrzeciwników.Length);
            ChooseSpawnPoint();

            //TODO Skasować i poprawić funkcję losującą położenie

            //Sespawnowanie przeciwnika
            i++;
            tekst.text = i.ToString();
            Instantiate(TablicaPrzeciwników[wybór], ChooseSpawnPoint(), Quaternion.Euler(Vector3.zero));
        }
        if (Stery.Select(ActionList.A))
        {
            Instantiate(TablicaPrzeciwników[0], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position, Quaternion.Euler(Vector3.zero));
        }
    }

    /// <summary>Funkcja wybierająca wektor3 na około widocznej mapki</summary>
    public Vector2 ChooseSpawnPoint()
    {
        var SpawnPoint = new Vector2();
        do
        {
            float alfa = rand.Next(360) - 179;
            var Tangens = Mathf.Tan(alfa);
            if (Tangens > 1 || Tangens < -1)
            {
                Tangens = Mathf.Cos(alfa)/Mathf.Sin(alfa);
                if (Tangens < 0)
                {
                    //Spawnowanie po bokach ekranu
                    // SpawnPoint = new Vector2(-RozmiarMapy.x, -RozmiarMapy.y*Tangens);
                }
            } else
            {
                //Spawnowanie na górnej krawędzi ekranu
                SpawnPoint = new Vector2(RozmiarMapy.x*Tangens, RozmiarMapy.y);
            }
        } while (SpawnPoint.y < 9);
        return SpawnPoint;
    }


    private void Awake()
    {
        rand = new Random();
    }
}