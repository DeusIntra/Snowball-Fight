using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerShooter : MonoBehaviour
{
    public float maxShootForce = 15f;
    public float minShootForceFraction = 0.4f;
    public float snowballSideVelocityMultiplier = 1f;
    public float snowballScale = 1f;

    [HideInInspector] public float doubleShotChance = 0f;

    public ProgressBarTimed progressBar;
    public GameObject snowballPrefab;
    [HideInInspector] public GameObject nextSnowballPrefab = null;

    private Shooter _shooter;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _rigidbody = GetComponent<Rigidbody>();        
    }

    public void Shoot()
    {
        float shootForce = progressBar.currentFill * maxShootForce * (1f - minShootForceFraction);

        // if shootForce = 0, snowball will get at least a minimal shoot force
        float force = shootForce + maxShootForce * minShootForceFraction;

        float sideForce = _rigidbody.velocity.x * snowballSideVelocityMultiplier;

        GameObject sbPrefab;
        if (nextSnowballPrefab != null)
        {
            sbPrefab = nextSnowballPrefab;
            nextSnowballPrefab = null;
        }
        else sbPrefab = snowballPrefab;

        CreateSnowball(force, sbPrefab, sideForce);

        if (doubleShotChance > Random.Range(0f, 1f))
            StartCoroutine(SecondShotCoroutine(force, sbPrefab, sideForce));

        progressBar.StopAndReset();
    }

    private void CreateSnowball(float force, GameObject sbPrefab, float sideForce)
    {
        GameObject snowball = _shooter.Shoot(force, sbPrefab, sideForce);
        snowball.transform.localScale = snowball.transform.lossyScale * snowballScale;
        snowball.tag = "Player Snowball";
    }

    private IEnumerator SecondShotCoroutine(float force, GameObject sbPrefab, float sideForce)
    {
        yield return new WaitForSeconds(0.2f);
        CreateSnowball(force, sbPrefab, sideForce);
    }

}
