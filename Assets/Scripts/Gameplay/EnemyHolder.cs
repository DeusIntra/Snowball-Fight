using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies;

    public void Remove(GameObject gameObject)
    {
        enemies.Remove(gameObject);

        if (enemies.Count == 0)
        {
            Debug.Log("Win");
        }
    }
}
