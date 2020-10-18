using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform snowballSpawnPoint;
    public Transform target;

    private float g;
    private float angle;

    private void Start()
    {
        g = Physics.gravity.y;
        angle = snowballSpawnPoint.rotation.eulerAngles.x;
    }

    public void ShootAtTarget()
    {
        // v = sqrt( dist * g / sin( 2 * angle ) )
        // assuming target and spawn point have equal height
        float dist = Vector3.Distance(snowballSpawnPoint.position, target.position);

        snowballSpawnPoint.LookAt(target);

        
    }


}
