using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    public Transform snowballSpawnPoint;

    public GameObject Shoot(float force, GameObject snowballPrefab, float sideForce = 0f)
    {
        GameObject snowball = Instantiate(snowballPrefab, snowballSpawnPoint.position, snowballSpawnPoint.rotation);
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        Vector3 forwardVelocity = snowball.transform.forward * force;
        Vector3 sideVelocity = snowball.transform.right * sideForce;

        snowballRB.AddForce(forwardVelocity + sideVelocity, ForceMode.Impulse);

        //PlayThrowSound();

        return snowball;
    }
}
