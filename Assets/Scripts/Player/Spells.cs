using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(AudioSource))]
public class Spells : MonoBehaviour
{
    public float stormDurationSeconds = 2f;
    public float icebergFallSeconds = 0.2f;
    public float hailDurationSeconds = 2f;

    public float freezeDebuffDurationSeconds = 7f;

    public Button spell1Button;
    public Button spell2Button;
    public Button spell3Button;

    public StormSpawner stormSpawner;
    public SnowballSpawner snowballSpawner;
    public IcebergSpawner icebergSpawner;
    public GameObject iceDestructionPrefab;
    public AudioClip stunSound;

    private EnemyHolder _enemyHolder;
    private Mana _mana;
    private AudioSource _audioSource;
    private bool _enemiesStunned = false;

    private void Awake()
    {
        _mana = GetComponent<Mana>();
        _enemyHolder = FindObjectOfType<EnemyHolder>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnManaChange()
    {
        spell1Button.interactable = false;
        spell2Button.interactable = false;
        spell3Button.interactable = false;

        if (_mana.currentFraction >= 0.33f)
        {
            spell1Button.interactable = true;
        }
        if (_mana.currentFraction >= 0.66f)
        {
            spell2Button.interactable = true;
        }
        if (_mana.currentFraction == 1f)
        {
            spell3Button.interactable = true;
        }
    }


    public void CastStorm()
    {
        _mana.Sub(_mana.max / 4);
        stormSpawner.Cast(stormDurationSeconds);
        StartCoroutine(StormCoroutine());
    }


    public void CastIceberg()
    {
        _mana.Sub(_mana.max / 2);
        icebergSpawner.Cast();
        StartCoroutine(IcebergCoroutine());
    }


    public void CastHail()
    {
        _mana.Zero();

        snowballSpawner.Cast(hailDurationSeconds);
        StartCoroutine(HailCoroutine());
    }


    private IEnumerator StormCoroutine()
    {
        yield return new WaitForSeconds(stormDurationSeconds / 2f);

        List<GameObject> enemies = _enemyHolder.enemies;
        float[] enemiesFPS = new float[enemies.Count];
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyDebuff enemyDebuff = enemies[i].GetComponent<EnemyDebuff>();
            if (enemyDebuff != null)
            {
                enemyDebuff.SlowDown(stormDurationSeconds * 2f);
                enemyDebuff.Stun(stormDurationSeconds * 2f);
                _enemiesStunned = true;
            }

            EnemyAnimator enemyAnimator = enemies[i].GetComponent<EnemyAnimator>();
            if (enemyAnimator != null)
            {
                enemiesFPS[i] = enemyAnimator.GetFPS();
                enemyAnimator.SetFPS(enemiesFPS[i] / 2f);
            }
        }

        if (_enemiesStunned)
        {
            _audioSource.clip = stunSound;
            _audioSource.Play();
            _enemiesStunned = false;
        }

        yield return new WaitForSeconds(stormDurationSeconds * 2f);

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyAnimator enemyAnimator = enemies[i].GetComponent<EnemyAnimator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetFPS(enemiesFPS[i]);
            }
        }
    }


    private IEnumerator IcebergCoroutine()
    {
        yield return new WaitForSeconds(icebergFallSeconds);

        Vector3 destructionPosition = icebergSpawner.endPosition.position;
        destructionPosition.y = 0;
        GameObject iceDestructionParticles = Instantiate(iceDestructionPrefab, destructionPosition, iceDestructionPrefab.transform.rotation);
        Destroy(iceDestructionParticles, 2f);

        List<GameObject> enemies = _enemyHolder.enemies;
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyDebuff enemyDebuff = enemies[i].GetComponent<EnemyDebuff>();
            if (enemyDebuff != null)
            {
                enemyDebuff.Freeze(freezeDebuffDurationSeconds);
            }

            EnemyAnimator enemyAnimator = enemies[i].GetComponent<EnemyAnimator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.Pause(freezeDebuffDurationSeconds);
            }

            Health health = enemies[i].GetComponent<Health>();
            health.Sub(7);            
        }
    }


    private IEnumerator HailCoroutine()
    {
        yield return new WaitForSeconds(hailDurationSeconds);

        List<GameObject> enemies = _enemyHolder.enemies;
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyDebuff enemyDebuff = enemies[i].GetComponent<EnemyDebuff>();
            if (enemyDebuff != null)
            {
                enemyDebuff.Stun(stormDurationSeconds * 2f);
            }

            Health health = enemies[i].GetComponent<Health>();
            health.Sub(health.max / 3);
        }
    }


    public void CastSpell()
    {
        _mana.Zero();

        spell1Button.interactable = false;
        spell2Button.interactable = false;
        spell3Button.interactable = false;
    }


    private IEnumerator CastSpellCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
}
