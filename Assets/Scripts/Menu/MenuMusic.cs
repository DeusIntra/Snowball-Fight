using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public float delay = 5f;

    private float _currentDelay;
    private float _clipLength;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _currentDelay = 0f;
        _clipLength = _audioSource.clip.length;
    }

    private void Update()
    {
        if (_currentDelay <= 0)
        {
            _currentDelay = _clipLength + delay;
            _audioSource.Play();
        }

        _currentDelay -= Time.deltaTime;
    }    
}
