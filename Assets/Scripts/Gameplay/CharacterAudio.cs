using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _death;

    [Header("Mixers")]
    [SerializeField] private AudioMixerGroup _shootMixer;
    [SerializeField] private AudioMixerGroup _deathMixer;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        _audioSource.clip = _shoot;
        _audioSource.outputAudioMixerGroup = _shootMixer;
        _audioSource.Play();
    }

    public void Die()
    {
        _audioSource.clip = _death;
        _audioSource.outputAudioMixerGroup = _deathMixer;
        _audioSource.Play();
    }
}
