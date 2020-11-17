using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _speed;

    private float _time;


    public void SetDestination(Vector3 destination, float speed = 1f)
    {
        _startPosition = transform.position;
        _endPosition = destination;
        _speed = speed;

        StartCoroutine(DriveCoroutine());
    }


    private IEnumerator DriveCoroutine()
    {
        float t = _time * _speed;
        while (t < 1)
        {
            t = _time * _speed;

            float x = Mathf.Lerp(_startPosition.x, _endPosition.x, t);
            float z = Mathf.Lerp(_startPosition.z, _endPosition.z, t);

            transform.position = new Vector3(x, transform.position.y, z);

            _time += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }
}
