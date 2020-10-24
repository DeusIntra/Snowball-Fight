using UnityEngine;

[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyJumper))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    private EnemyShooter _shooter;
    private EnemyMover _mover;
    private EnemyJumper _jumper;
    private EnemyAnimator _enemyAnimator;
    private Health _health;
    private BoxCollider _collider;

    private void Awake()
    {
        _shooter = GetComponent<EnemyShooter>();
        _mover = GetComponent<EnemyMover>();
        _jumper = GetComponent<EnemyJumper>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _health = GetComponent<Health>();
        _collider = GetComponent<BoxCollider>();
    }

    public void OnZeroHealth()
    {
        if (!_health.isAlive) Die();
    }

    private void Die()
    {
        _enemyAnimator.Die();

        _mover.enabled = false;
        _shooter.enabled = false;
        _jumper.enabled = false;

        _collider.enabled = false;
    }
}
