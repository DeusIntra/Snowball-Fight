using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int max = 4;
    public string enemyProjectileTag;
    public UnityEvent onChange;

    private int _current;

    public bool isAlive { get; private set; }

    public int current => _current;
    public float currentFraction => (float)_current / (float)max;

    private void Awake()
    {
        _current = max;
        isAlive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyProjectileTag))
        {
            GameObject projectile = other.gameObject;
            int damage = projectile.GetComponent<Snowball>().damage;
            _current -= damage;

            if (_current <= 0)
                isAlive = false;

            onChange.Invoke();
        }
    }
}
