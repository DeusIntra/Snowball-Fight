using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public float minShootTimeSeconds = 3f;
    public float maxShootTimeSeconds = 8f;
    [HideInInspector] public bool isAlive = true;

    public MeshAnimations meshAnimations;

    private float timeToShoot;

    private BoxCollider boxCollider;
    private Shooter shooter;
    private Health health;
    private EnemyMove enemyMove;

    private Coroutine _delayCoroutine;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
        boxCollider = GetComponent<BoxCollider>();
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Start()
    {
        resetShootTime();
        meshAnimations.walk.Play();
    }

    private void Update()
    {
        if (health.isAlive)
        {
            timeToShoot -= Time.deltaTime;

            if (timeToShoot <= 0)
            {
                meshAnimations.walk.Stop();
                meshAnimations.throwing.Play();

                if (_delayCoroutine != null)
                    StopCoroutine(_delayCoroutine);
                _delayCoroutine = StartCoroutine(WalkAnimation());

                shooter.ShootAtTarget();
                resetShootTime();
            }
        }
        else
        {
            meshAnimations.StopAll();
            meshAnimations.death.Play();

            enemyMove.Stop();
            enemyMove.enabled = false;

            boxCollider.enabled = false;
        }
    }

    private void resetShootTime()
    {
        timeToShoot = Random.Range(minShootTimeSeconds, maxShootTimeSeconds);
    }

    private IEnumerator WalkAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        meshAnimations.throwing.Stop();
        meshAnimations.walk.Play();
    }

}
