using UnityEngine;

public class SnowballEnemyMagnet : MonoBehaviour
{
    [HideInInspector] public float forceMultiplier = 1f;

    private EnemyHolder _enemyHolder;
    private Rigidbody _rb;
    private float enemyHeight = 1.6f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_enemyHolder == null) _enemyHolder = FindObjectOfType<EnemyHolder>();
    }

    private void FixedUpdate()
    {
        Vector3 closestEnemyPosition = Vector3.zero;
        float closestDistance = 100000f;
        foreach (var enemyGO in _enemyHolder.enemies)
        {
            var enemyPosition = enemyGO.transform.position + Vector3.up * enemyHeight;
            float distance = Vector3.Distance(enemyPosition, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyPosition = enemyPosition;
            }
        }

        if (closestEnemyPosition == Vector3.zero) return;

        var directionUnscaled = closestEnemyPosition - transform.position;
        var direction = directionUnscaled.normalized;        
        _rb.AddForce(direction * (forceMultiplier / Mathf.Pow(closestDistance, 2)));
    }
}
