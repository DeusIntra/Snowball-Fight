using UnityEngine;

[RequireComponent(typeof(PausableEnemy))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyDebuff : MonoBehaviour
{
    public GameObject icePrefab;

    private PausableEnemy _pausable;
    private EnemyMover _mover;


    private void Awake()
    {
        _pausable = GetComponent<PausableEnemy>();
        _mover = GetComponent<EnemyMover>();
    }


    public void SlowDown(float seconds)
    {
        _mover.SlowDown(seconds);
    }


    public void Stun(float seconds)
    {
        _pausable.PauseShooter(seconds);
        _pausable.PauseJumper(seconds);
    }


    public void Freeze(float seconds)
    {
        _pausable.PauseShooter(seconds);
        _pausable.PauseJumper(seconds);
        _pausable.PauseMover(seconds);
        
        GameObject ice = Instantiate(icePrefab, transform.position, transform.rotation);
        Destroy(ice, seconds);
    }
}
