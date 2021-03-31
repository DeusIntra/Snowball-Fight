using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    public float fadeTimeSeconds = 0f;
    public float pauseBeforeFadeSeconds = 0f;
    public float speedMultiplier = 1f;
    public UnityEvent OnReachDestination;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _speed;
    private float _time;
    private Material _material;
    private float _startAlpha;

    private void Awake()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null) _material = renderer.material;
    }

    public void SetDestination(Vector3 destination, float speed = 1f, float t = 0)
    {
        _startPosition = transform.position;
        _endPosition = destination;
        _speed = speed * speedMultiplier;
        if (_material != null) _startAlpha = _material.color.a;

        StartCoroutine(DriveCoroutine(t));
    }

    private IEnumerator DriveCoroutine(float t)
    {
        _time = t / _speed;
        while (t < 1)
        {
            t = _time * _speed;

            transform.position = Vector3.Lerp(_startPosition, _endPosition, t);

            _time += Time.deltaTime;

            yield return null;
        }

        OnReachDestination.Invoke();

        if (_material != null)
        {
            yield return new WaitForSeconds(pauseBeforeFadeSeconds);

            float currentFadeTime = 0;
            Color color = _material.color;
            while (currentFadeTime < fadeTimeSeconds)
            {
                color.a = Mathf.Lerp(_startAlpha, 0, currentFadeTime / fadeTimeSeconds);
                _material.color = color;

                currentFadeTime += Time.deltaTime;

                yield return null;
            }

            color.a = _startAlpha;
            _material.color = color;
        }

        Destroy(gameObject);
        yield break;
    }
}
