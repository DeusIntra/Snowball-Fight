using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mana))]
public class Spells : MonoBehaviour
{
    public float hailDurationSeconds = 2f;

    public Button spell1Button;
    public Button spell2Button;
    public Button spell3Button;

    public SnowballSpawner snowballSpawner;

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

    }


    public void CastIceberg()
    {

    }


    public void CastHail()
    {
        _mana.Zero();

        snowballSpawner.Cast(hailDurationSeconds);
        StartCoroutine(HailCoroutine());
    }


    private IEnumerator HailCoroutine()
    {
        yield return new WaitForSeconds(hailDurationSeconds);

        List<GameObject> enemies = _enemyHolder.enemies;
        foreach (GameObject enemy in enemies)
        {
            Health health = enemy.GetComponent<Health>();
            health.Sub(health.max / 4);
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
