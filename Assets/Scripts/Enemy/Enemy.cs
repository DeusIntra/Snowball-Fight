using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyJumper))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public ProgressBar healthBar;
    public AudioClip deathSound;
    public AudioMixerGroup deathMixer;

    private EnemyShooter _shooter;
    private EnemyMover _mover;
    private EnemyJumper _jumper;
    private EnemyAnimator _enemyAnimator;
    private Health _health;
    private BoxCollider _collider;
    private Coroutine _coroutine;
    private EnemyHolder _enemyHolder;
    private AudioSource _audioSource;


    private void Awake()
    {
        _shooter = GetComponent<EnemyShooter>();
        _mover = GetComponent<EnemyMover>();
        _jumper = GetComponent<EnemyJumper>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _health = GetComponent<Health>();
        _collider = GetComponent<BoxCollider>();
        _enemyHolder = FindObjectOfType<EnemyHolder>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnZeroHealth()
    {
        if (!_health.isAlive) Die();
    }


    public void OnHealthChange()
    {
        if (healthBar == null) return;

        healthBar.gameObject.SetActive(true);
        healthBar.SetFill(_health.currentFraction);

        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(HideHealthBarAfter(1f));
    }


    private IEnumerator HideHealthBarAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        healthBar.gameObject.SetActive(false);
    }


    private void Die()
    {
        _enemyAnimator.Die();

        _mover.enabled = false;
        _shooter.enabled = false;
        _jumper.enabled = false;

        _collider.enabled = false;

        PlayDeathSound();

        _enemyHolder.Remove(gameObject);
    }

    private void PlayDeathSound()
    {
        _audioSource.clip = deathSound;
        _audioSource.outputAudioMixerGroup = deathMixer;
        _audioSource.Play();
    }
}
