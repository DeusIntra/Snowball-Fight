using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyShooter : MonoBehaviour
{
    public float minShootTimeSeconds = 3f;
    public float maxShootTimeSeconds = 8f;

    public Transform _snowballSpawn;

    private float g;
    private float angle;
    private float _timeToShoot;

    private Shooter _shooter;
    private EnemyAnimator _enemyAnimator;
    private Transform _target;

    private const float hack = 0.55f;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        g = Physics.gravity.y;
        angle = -_shooter.snowballSpawn.rotation.eulerAngles.x;
        resetShootTime();
    }

    private void Update()
    {
        _timeToShoot -= Time.deltaTime;

        if (_timeToShoot <= 0)
        {
            _enemyAnimator.Throw();
            ShootAtTarget();
            resetShootTime();
        }
    }

    public void ShootAtTarget()
    {
        // v = sqrt( dist * g / sin( 2 * angle ) )
        // assuming target and spawn point have equal height
        float dist = Vector3.Distance(_snowballSpawn.position, _target.position);

        _snowballSpawn.LookAt(_target);
        _snowballSpawn.Rotate(-angle, 0, 0);

        float snowballVelocityMultiplier = Mathf.Sqrt(dist * g / Mathf.Sin(2 * angle)) * hack;
        _shooter.Shoot(snowballVelocityMultiplier);
    }

    private void resetShootTime()
    {
        _timeToShoot = Random.Range(minShootTimeSeconds, maxShootTimeSeconds);
    }
}
