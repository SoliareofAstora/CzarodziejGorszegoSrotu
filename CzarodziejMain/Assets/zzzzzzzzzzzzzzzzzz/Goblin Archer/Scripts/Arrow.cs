using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 a = new Vector3(0, -10, 0);
    public bool right = true;

    public Vector3 v = new Vector3(20, 20, 0);

    private void Start()
    {
        Destroy(gameObject, 10);
        if (!right)
            v.x = -v.x;
    }

    private void Update()
    {
        transform.position += v*Time.deltaTime;
        v += a*Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(v, new Vector3(0, 0, -1));
    }
}