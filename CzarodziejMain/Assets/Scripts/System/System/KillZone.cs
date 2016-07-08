using UnityEngine;

public class KillZone : MonoBehaviour
{
    public BoxCollider2D box;
    // Use this for initialization
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}