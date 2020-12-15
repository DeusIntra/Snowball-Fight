using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    public Transform snowballSpawn;
    public GameObject snowballPrefab;
    public AudioClip throwSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(float force, float sideForce = 0f)
    {
        GameObject snowball = Instantiate(snowballPrefab, snowballSpawn.position, snowballSpawn.rotation);
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        Vector3 forwardVelocity = snowball.transform.forward * force;
        Vector3 sideVelocity = snowball.transform.right * sideForce;

        snowballRB.AddForce(forwardVelocity + sideVelocity, ForceMode.Impulse);

        _audioSource.clip = throwSound;
        _audioSource.Play();
    }
}
