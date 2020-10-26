using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 1;
    public float destructionChance = 0.1f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Snowball other = otherCollider.GetComponent<Snowball>();
        if (other != null)
        {
            float p = 1 - Mathf.Sqrt(1 - destructionChance);
            if (Random.Range(0f, 1f) < p)
            {
                other.Break();
                Break();
            }
        }
        else
        {
            Break();
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }
}
