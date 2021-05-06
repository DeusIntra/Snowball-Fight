using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : MonoBehaviour, IDebuffEffect
{
    public float duration;

    public GameObject icePrefab;

    public void Debuff(GameObject target)
    {
        MonoBehaviour mover = (MonoBehaviour)target.GetComponent<IMover>();
        if (mover == null) return;
        StartCoroutine(DebuffCoroutine(mover));
    }

    private IEnumerator DebuffCoroutine(MonoBehaviour mover)
    {
        mover.enabled = false;

        yield return new WaitForSeconds(duration);

        mover.enabled = true;

    }
}
