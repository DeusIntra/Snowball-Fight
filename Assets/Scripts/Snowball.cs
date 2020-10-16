using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
