using UnityEngine;
using UnityEngine.Audio;

public class LevelMusic : MonoBehaviour
{
    public AudioSource musicSource;

    public GameParametersSingleton parameters;

    public AudioClip levels15;
    public AudioClip levels69;
    public AudioClip level10;

    public AudioMixerGroup levels15Mixer;
    public AudioMixerGroup levels69Mixer;
    public AudioMixerGroup level10Mixer;

    private void Start()
    {
        if (parameters.currentLevelIndex < 5)
        {
            musicSource.clip = levels15;
            musicSource.outputAudioMixerGroup = levels15Mixer;
        }
        else if (parameters.currentLevelIndex < 9)
        {
            musicSource.clip = levels69;
            musicSource.outputAudioMixerGroup = levels69Mixer;
        }
        else
        {
            musicSource.clip = level10;
            musicSource.outputAudioMixerGroup = level10Mixer;
        }
        musicSource.Play();
    }
}
