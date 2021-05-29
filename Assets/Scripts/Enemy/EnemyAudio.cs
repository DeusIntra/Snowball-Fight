using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class EnemyAudio : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _death;
    [SerializeField] private AudioClip _jump;

    [Header("Mixers")]
    [SerializeField] private AudioMixerGroup _shootMixer;
    [SerializeField] private AudioMixerGroup _deathMixer;
}
