using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    public float speed = 1.5f;
    public float debuffedSpeedFraction = 0.5f;
    public float boundaryDistance = 5f;

    [HideInInspector]
    public float pauseTimeSeconds;

    private float _targetPosition;
    private float _speedMultiplier = 1f;

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

        // if close to target position
        if (Mathf.Abs(_targetPosition - transform.position.x) < minDifference)
        {
            chooseTargetPosition();
        }

        float sign = _targetPosition - transform.position.x > 0 ? 1f : -1f;
        float x = sign * speed * _speedMultiplier;

        _rigidbody.velocity = new Vector3(x, 0f, 0f);
    }


    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }


    public void SlowDown(float seconds)
    {
        StartCoroutine(SlowDownCoroutine(seconds));
    }


    private IEnumerator SlowDownCoroutine(float seconds)
    {
        _speedMultiplier = debuffedSpeedFraction;

        yield return new WaitForSeconds(seconds);

        _speedMultiplier = 1f;
    }

    private void chooseTargetPosition()
    {
        _targetPosition = Random.Range(-boundaryDistance - minDifference, boundaryDistance + minDifference);
    }
}
