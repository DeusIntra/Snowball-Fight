using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyJumper : MonoBehaviour
{
    public float minPositionZ = 12.5f;
    public float maxPositionZ = 25f;

    public float minJumpTimeSeconds = 5f;
    public float maxJumpTimeSeconds = 20f;

    public float jumpingDistance = 2.5f;
    public float jumpingTime = 0.4f;
    public float jumpingHeight = 1f;

    public AnimationCurve curve;

    public UnityEvent onJump;

    [HideInInspector]
    public float pauseTimeSeconds = 0f;

    private float _timeToNextJump;
    private Coroutine _coroutine;

    private void Start()
    {
        resetJumpTime();
    }

    private void Update()
    {
        if (pauseTimeSeconds > 0f)
        {
            pauseTimeSeconds -= Time.deltaTime;
            return;
        }

        _timeToNextJump -= Time.deltaTime;

        if (_timeToNextJump <= 0)
        {
            _coroutine = StartCoroutine(Jump());
            resetJumpTime();
            _timeToNextJump += jumpingTime;
        }

    }

    private void OnDisable()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void resetJumpTime()
    {
        _timeToNextJump = Random.Range(minJumpTimeSeconds, maxJumpTimeSeconds);
    }

    private IEnumerator Jump()
    {
        float startingZ = transform.position.z;
        float endingZ;

        // decide where to jump
        int direction;
        if (startingZ >= maxPositionZ)
        {
            // jump forward
            direction = -1;
        }
        else if (startingZ <= minPositionZ + jumpingDistance)
        {
            // jump back
            direction = 1;
        }
        else
        {
            direction = Random.Range(0, 2) * 2 - 1;
        }

        endingZ = startingZ + jumpingDistance * direction;

        float t = 0f;

        while (t <= 1f)
        {
            float positionZ = Mathf.Lerp(startingZ, endingZ, t);
            float positionY = curve.Evaluate(t) * jumpingHeight;

            transform.position = new Vector3(transform.position.x, positionY, positionZ);

            t += 1f / jumpingTime * Time.deltaTime;
            yield return null;
        }
    }

}
