using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerShooter : MonoBehaviour
{
    public float maxShootForce = 15f;
    public float minShootForceFraction = 0.4f;
    public float snowballSideVelocityMultiplier = 1f;

    public ProgressBarTimed progressBar;

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

        _shooter.Shoot(force, sideForce);

        progressBar.StopAndReset();
    }

}
