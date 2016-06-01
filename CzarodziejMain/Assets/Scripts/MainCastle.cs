using UnityEngine;

public class MainCastle : MonoBehaviour
{
    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        UI.access.Przegraleś();
    }
}
