using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 1;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Break();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Break();
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}
