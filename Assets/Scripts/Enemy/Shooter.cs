using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    public Transform snowballSpawnPoint;
    public AudioClip throwSound;
    public AudioMixerGroup throwMixer;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public GameObject Shoot(float force, GameObject snowballPrefab, float sideForce = 0f)
    {
        GameObject snowball = Instantiate(snowballPrefab, snowballSpawnPoint.position, snowballSpawnPoint.rotation);
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        Vector3 forwardVelocity = snowball.transform.forward * force;
        Vector3 sideVelocity = snowball.transform.right * sideForce;

        snowballRB.AddForce(forwardVelocity + sideVelocity, ForceMode.Impulse);

        PlayThrowSound();

        return snowball;
    }

    private void PlayThrowSound()
    {
        _audioSource.clip = throwSound;
        _audioSource.outputAudioMixerGroup = throwMixer;
        _audioSource.Play();
    }
}
