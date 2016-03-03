using UnityEngine;
using System.Collections;

public class MainCastle : MonoBehaviour
{
    BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        UI.access.Przegraleś();
    }
}
