using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTest : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    private Coroutine _routine;

    public void Animate()
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        float startTime = Time.time;
        float t = 0;
        while (t < 1)
        {
            t = Time.time - startTime;
            float x = Mathf.Lerp(point1.position.x, point2.position.x, t);
            float y = Mathf.Lerp(point1.position.x, point2.position.x, t);
            transform.position = new Vector3(0, 2 * point1.position.y - Mathf.Pow(y, 2), 0);
            yield return null;
        }
    }
}
