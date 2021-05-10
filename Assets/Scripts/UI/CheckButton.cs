using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CheckButton : MonoBehaviour
{
    public enum CheckType { Music, Sounds }

    private bool _check;
    public GameObject checkMark;
    public GameParametersSingleton parameters;
    public CheckType checkType;
    public AudioMixerGroup mixer;

    private void Start()
    {
        if (checkType == CheckType.Music)
            _check = parameters.music;
        if (checkType == CheckType.Sounds)
            _check = parameters.sounds;

        checkMark.SetActive(_check);
        mixer.audioMixer.SetFloat(mixer.name, _check ? 0 : -80f);
    }

    public void Check()
    {
        _check = !_check;
        checkMark.SetActive(_check);
        SetParams(_check);
        parameters.Save();
        mixer.audioMixer.SetFloat(mixer.name, _check ? 0 : -80f);
    }

    private void SetParams(bool check)
    {
        if (checkType == CheckType.Music)
            parameters.music = check;
        if (checkType == CheckType.Sounds)
            parameters.sounds = check;
    }
}
