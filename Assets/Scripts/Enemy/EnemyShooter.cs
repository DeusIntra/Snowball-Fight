using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyShooter : MonoBehaviour
{
    public float minShootTimeSeconds = 3f;
    public float maxShootTimeSeconds = 8f;
    public float maxYAngleOffset = 5f;

    public Transform snowballSpawn;
    public GameObject snowballPrefab;

    public UnityEvent onShoot;

    [HideInInspector]
    public float pauseTimeSeconds = 0f;

    private float g;
    private float angle;
    private float _timeToShoot;

    private Shooter _shooter;
    private EnemyAnimator _enemyAnimator;
    private Transform _target;

    private const float hack = 0.57f;

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
        if (pauseTimeSeconds > 0f)
        {
            pauseTimeSeconds -= Time.deltaTime;
            return;
        }

        _timeToShoot -= Time.deltaTime;

        if (_timeToShoot <= 0)
        {
            _enemyAnimator.Throw();
            ShootAtTarget();
            resetShootTime();
            onShoot.Invoke();
        }
    }

    public void ShootAtTarget()
    {
        // v = sqrt( dist * g / sin( 2 * angle ) )
        // assuming target and spawn point have equal height
        float dist = Vector3.Distance(snowballSpawn.position, _target.position);
        float randomAngle = Random.Range(-maxYAngleOffset, maxYAngleOffset);

        snowballSpawn.LookAt(_target);
        snowballSpawn.Rotate(-angle, randomAngle, 0);

        float snowballVelocityMultiplier = Mathf.Sqrt(dist * g / Mathf.Sin(2 * angle)) * hack;
        _shooter.Shoot(snowballVelocityMultiplier, snowballPrefab);
    }

    private void resetShootTime()
    {
        _timeToShoot = Random.Range(minShootTimeSeconds, maxShootTimeSeconds);
    }
}
