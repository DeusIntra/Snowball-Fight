using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform snowballSpawn;
    public Transform target;
    public GameObject snowballPrefab;

    private float g;
    private float angle;

    const float hack = 0.55f;

    private void Start()
    {
        g = Physics.gravity.y;
        angle = -snowballSpawn.rotation.eulerAngles.x;
    }

    public void ShootAtTarget()
    {
        // v = sqrt( dist * g / sin( 2 * angle ) )
        // assuming target and spawn point have equal height
        float dist = Vector3.Distance(snowballSpawn.position, target.position);

        snowballSpawn.LookAt(target);
        snowballSpawn.Rotate(-angle, 0, 0);

        GameObject snowball = Instantiate(snowballPrefab, snowballSpawn.position, snowballSpawn.rotation);
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        float snowballVelocityMultiplier = Mathf.Sqrt(dist * g / Mathf.Sin(2 * angle)) * hack;

        snowballRB.AddForce(snowball.transform.forward * snowballVelocityMultiplier, ForceMode.Impulse);
    }
}
