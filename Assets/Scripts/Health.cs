using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 4;
    public string enemyProjectileTag;

    private int currentHealth;

    public bool isAlive { get; private set; }

    private void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(enemyProjectileTag))
        {
            GameObject projectile = collision.gameObject;
            int damage = projectile.GetComponent<Snowball>().damage;
            currentHealth -= damage;

            if (currentHealth <= 0)
                isAlive = false;
        }
    }
}
