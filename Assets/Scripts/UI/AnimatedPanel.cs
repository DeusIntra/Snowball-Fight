using System.Collections;
using UnityEngine;

public class AnimatedPanel : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public AnimationCurve positionOverTime;

    private RectTransform _rectTransform;
    private Coroutine _coroutine;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        startPosition = _rectTransform.anchoredPosition;        
    }

    public void Open()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Animation(speed));
        else
            Debug.Log("stop coroutine");
    }

    public void Close()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Animation(-speed));
    }

    private IEnumerator Animation(float speed)
    {
        float time = 0f;
        if (speed < 0) time = 1f;

        while (0 <= time && time <= 1)
        {
            time += Time.deltaTime * speed;
            float t = positionOverTime.Evaluate(time);

            float x = Mathf.Lerp(startPosition.x, endPosition.x, t);
            float y = Mathf.Lerp(startPosition.y, endPosition.y, t);

            _rectTransform.anchoredPosition = new Vector3(x, y);

            yield return null;
        }

        _coroutine = null;
    }
}
