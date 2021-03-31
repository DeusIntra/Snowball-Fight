using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHolder : MonoBehaviour
{
    public UnityEvent onZeroEnemies;
    [HideInInspector] public List<GameObject> enemies;

    public void Remove(GameObject gameObject)
    {
        enemies.Remove(gameObject);

        if (enemies.Count == 0)
        {
            onZeroEnemies.Invoke();
        }
    }
}
