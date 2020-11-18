using UnityEngine;

[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyJumper))]
public class PausableEnemy : MonoBehaviour
{
    private EnemyShooter _enemyShooter;
    private EnemyMover _enemyMover;
    private EnemyJumper _enemyJumper;

    private void Awake()
    {
        _enemyShooter = GetComponent<EnemyShooter>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyJumper = GetComponent<EnemyJumper>();
    }

    public void PauseShooter(float seconds)
    {
        _enemyShooter.pauseTimeSeconds = seconds;
    }

    public void PauseMover(float seconds)
    {
        _enemyMover.pauseTimeSeconds = seconds;
    }

    public void PauseJumper(float seconds)
    {
        _enemyJumper.pauseTimeSeconds = seconds;
    }
}
