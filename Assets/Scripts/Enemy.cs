using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour
{
    public float minShootTimeSeconds = 3f;
    public float maxShootTimeSeconds = 8f;

    private Shooter shooter;
    private float timeToShoot;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        resetShootTime();
    }

    private void Update()
    {
        timeToShoot -= Time.deltaTime;

        if (timeToShoot <= 0)
        {
            shooter.ShootAtTarget();
            resetShootTime();
        }
    }

    private void resetShootTime()
    {
        timeToShoot = Random.Range(minShootTimeSeconds, maxShootTimeSeconds);
    }

}
