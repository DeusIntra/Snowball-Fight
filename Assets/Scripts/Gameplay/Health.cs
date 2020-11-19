﻿using UnityEngine;
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
        _current = max;
        isAlive = true;
    }

    public void Sub(int amount)
    {
        _current -= amount;

        if (_current <= 0)
            isAlive = false;

        onChange.Invoke();
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
