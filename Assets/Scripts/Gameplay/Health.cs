using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int max = 4;
    public string enemyProjectileTag;
    public UnityEvent onChange;
    public UnityEvent onHit;

    private int _current;

    public bool isAlive { get; private set; }

    public int current => _current;
    public float currentFraction => (float)_current / (float)max;

    private void Awake()
    {
        ResetCurrent();
        isAlive = true;
    }

    public void Sub(int amount)
    {
        _current -= amount;

        if (_current <= 0) isAlive = false;

        onChange.Invoke();
    }

    public void Add(int amount)
    {
        int healthAfter = _current + amount;

        if (healthAfter > max) healthAfter = max;

        if (healthAfter != _current)
        {
            _current = healthAfter;
            onChange.Invoke();
        }
    }

    public void ResetCurrent()
    {
        _current = max;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyProjectileTag))
        {
            GameObject projectile = other.gameObject;
            int damage = projectile.GetComponent<Snowball>().damage;
            Sub(damage);
            onHit.Invoke();
        }
    }
}
