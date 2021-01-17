using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthGiver : MonoBehaviour
{
    public bool isEnabled = false;
    public int onHitAmount = 0;
    public int onKillAmount = 1;

    private Health _health;
    private Health _playerHealth;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
    }

    public void onHit()
    {
        if (isEnabled)
        {
            if (_health.isAlive)
            {
                _playerHealth.Add(onHitAmount);
            }
            else
            {
                _playerHealth.Add(onKillAmount);
            }
        }        
    }
}
