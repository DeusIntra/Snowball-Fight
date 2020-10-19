using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMove : MonoBehaviour
{
    public float speed = 1.5f;
    public float boundaryDistance = 5f;

    private Rigidbody _rigidbody;
    private float _targetPosition;

    private const float minDifference = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_targetPosition - transform.position.x) < minDifference)
        {
            chooseTargetPosition();
        }

        float sign = _targetPosition - transform.position.x > 0 ? 1f : -1f;
        float x = sign * speed;

        _rigidbody.velocity = new Vector3(x, 0f, 0f);

    }

    private void chooseTargetPosition()
    {
        _targetPosition = Random.Range(-boundaryDistance - minDifference, boundaryDistance + minDifference);
    }
}
