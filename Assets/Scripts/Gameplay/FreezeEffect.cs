using UnityEngine;

public class FreezeEffect : MonoBehaviour, IDebuffEffect
{
    public float duration;

    public void Debuff(GameObject target)
    {
        EnemyDebuff enemyDebuff = target.GetComponent<EnemyDebuff>();
        if (enemyDebuff)
        {
            enemyDebuff.Freeze(duration);
        }
    }
}
