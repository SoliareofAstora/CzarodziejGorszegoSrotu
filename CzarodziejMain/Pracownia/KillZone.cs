using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

    public BoxCollider box;
	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider>();
	}
	public void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
