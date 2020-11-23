using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mana))]
public class Spells : MonoBehaviour
{
    public float stormDurationSeconds = 2f;
    public float icebergFallSeconds = 0.2f;
    public float hailDurationSeconds = 2f;

    public float freezeDebuffDurationSeconds = 7f;

    public Button spell1Button;
    public Button spell2Button;
    public Button spell3Button;

    public SnowballSpawner snowballSpawner;
    public IcebergSpawner icebergSpawner;

    private EnemyHolder _enemyHolder;

    private Mana _mana;

    private void Awake()
    {
        _mana = GetComponent<Mana>();
        _enemyHolder = FindObjectOfType<EnemyHolder>();
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
        // TODO: cast storm animation
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
        yield return new WaitForSeconds(stormDurationSeconds);

        List<GameObject> enemies = _enemyHolder.enemies;
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyDebuff enemyDebuff = enemies[i].GetComponent<EnemyDebuff>();
            if (enemyDebuff != null)
            {
                enemyDebuff.SlowDown(stormDurationSeconds * 2f);
                enemyDebuff.Stun(stormDurationSeconds * 2f);
            }
        }
    }


    private IEnumerator IcebergCoroutine()
    {
        yield return new WaitForSeconds(icebergFallSeconds);

        List<GameObject> enemies = _enemyHolder.enemies;
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyDebuff enemyDebuff = enemies[i].GetComponent<EnemyDebuff>();
            if (enemyDebuff != null)
            {
                enemyDebuff.Freeze(freezeDebuffDurationSeconds);
            }

            Health health = enemies[i].GetComponent<Health>();
            health.Sub(health.max / 4);
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
