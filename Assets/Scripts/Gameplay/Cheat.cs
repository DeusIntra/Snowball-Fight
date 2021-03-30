using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void KillAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.Sub(enemyHealth.max);
            }
        }
    }
}
