using UnityEngine;

[RequireComponent(typeof(Health))]
public class ManaGiver : MonoBehaviour
{
    public float onHitAmount = 10;
    public float onKillAmount = 20;

    private Health _health;
    private Mana _playerMana;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerMana = FindObjectOfType<Player>().GetComponent<Mana>();
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
