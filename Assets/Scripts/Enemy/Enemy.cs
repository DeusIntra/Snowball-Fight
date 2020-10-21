using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class Enemy : MonoBehaviour
{
    public float minShootTimeSeconds = 3f;
    public float maxShootTimeSeconds = 8f;

    public MeshAnimations meshAnimations;

    private Shooter shooter;
    private float timeToShoot;
    private Coroutine _delayCoroutine;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        resetShootTime();
        meshAnimations.walk.Play();
    }

    private void Update()
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
