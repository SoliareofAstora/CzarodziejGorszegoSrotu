using UnityEngine;

public class KillZone : MonoBehaviour
{
    public BoxCollider2D box;
    // Use this for initialization
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}