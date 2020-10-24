using UnityEngine;

[RequireComponent(typeof(Health))]
public class ManaGiver : MonoBehaviour
{
    public int onHitAmount = 1;
    public int onKillAmount = 2;

    private Health _health;
    private Mana _playerMana;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerMana = GameObject.FindWithTag("Player").GetComponent<Mana>();
    }

    public void onHit()
    {
        if (_health.isAlive)
        {
            _playerMana.Add(onHitAmount);
        }
        else
        {
            _playerMana.Add(onKillAmount);
        }
    }
}
