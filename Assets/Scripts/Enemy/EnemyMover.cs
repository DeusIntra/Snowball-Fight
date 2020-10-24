using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    public float speed = 1.5f;
    public float boundaryDistance = 5f;

    [HideInInspector]
    public float pauseTimeSeconds;

    private float _targetPosition;

    private Rigidbody _rigidbody;

    private const float minDifference = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (pauseTimeSeconds > 0f)
        {
            _rigidbody.velocity = Vector3.zero;
            pauseTimeSeconds -= Time.deltaTime;
            return;
        }

        if (Mathf.Abs(_targetPosition - transform.position.x) < minDifference)
        {
            chooseTargetPosition();
        }

        float sign = _targetPosition - transform.position.x > 0 ? 1f : -1f;
        float x = sign * speed;

        _rigidbody.velocity = new Vector3(x, 0f, 0f);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void chooseTargetPosition()
    {
        _targetPosition = Random.Range(-boundaryDistance - minDifference, boundaryDistance + minDifference);
    }

    
}
