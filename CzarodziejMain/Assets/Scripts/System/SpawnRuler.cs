using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SpawnRuler : MonoBehaviour
{
    public static SpawnRuler instance;
    public SpawnRuler() {
        instance = this;
    }

    public Text tekst;
    private int i;
    private Random rand;
    public Vector2 RozmiarMapy = new Vector2(10, 10); //PoleSpawnu
    private Vector2 SpawnPoint; //TODO usunięcia

    //tabliac wszystkich przeciwników którzy mogą wyjść.
    [SerializeField] public GameObject[] TablicaPrzeciwników;

    // Spawnowanie przeciwników
    private void Update() {
        //Stawianie przeciwników pierszego sortu naokoło mapy
        if (Input.GetKey(KeyCode.Space)) {
            if (new Random().Next(100)<70)
            {
                return;
            }
            //Wybór losowego przeciwnika
            var wybór = rand.Next(TablicaPrzeciwników.Length);
            ChooseSpawnPoint();

            //TODO Skasować i poprawić funkcję losującą położenie
            if (SpawnPoint.y<10)
            {
               return;
            }
            //Sespawnowanie przeciwnika
            i++;
            tekst.text = i.ToString();
            Instantiate(TablicaPrzeciwników[wybór], SpawnPoint, Quaternion.Euler(Vector3.zero));
        }
    }
    /// <summary>Funkcja wybierająca wektor3 na około widocznej mapki</summary>
    public Vector2 ChooseSpawnPoint() {
        float alfa = rand.Next(360) - 179;
        var Tangens = Mathf.Tan(alfa);
        if (Tangens > 1 || Tangens < -1) {
            Tangens = Mathf.Cos(alfa) / Mathf.Sin(alfa);
            if (Tangens < 0) {
                //Spawnowanie po bokach ekranu
                // SpawnPoint = new Vector2(-RozmiarMapy.x, -RozmiarMapy.y*Tangens);
            }
        } else {
            //Spawnowanie na górnej krawędzi ekranu
            SpawnPoint = new Vector2(RozmiarMapy.x * Tangens, RozmiarMapy.y);
        }
        return SpawnPoint;
    }





    private void Awake()
    {
        rand = new Random();
    }


}