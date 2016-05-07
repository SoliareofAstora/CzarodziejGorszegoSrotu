using UnityEngine;
using System.Collections;

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
