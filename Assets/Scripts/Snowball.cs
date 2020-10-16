using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
    }
}
