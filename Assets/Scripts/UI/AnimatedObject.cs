using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    public bool animateOnStart = false;
    public List<UIAnimationData> animations;

    private RectTransform _rectTransform;
    private Coroutine _coroutine;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();     
    }

    private void Start()
    {
        if (animateOnStart) Animate();
    }

    public void Animate(int index = 0)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(AnimationCoroutine(index));
    }

    private void SetAnchors(Vector2 min, Vector2 max)
    {
        _rectTransform.anchorMin = min;
        _rectTransform.anchorMax = max;
    }

    private IEnumerator AnimationCoroutine(int i)
    {
        float speed = animations[i].speed;
        AnimationCurve positionOverTime = animations[i].positionOverTime;
        Vector2 startPosition = animations[i].startPosition;
        Vector2 endPosition = animations[i].endPosition;

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

        if (animations[i].setAnchorsAfterAnimation)
        {
            SetAnchors(animations[i].min, animations[i].max);
        }
    }

    [ContextMenu("Log position")]
    public void LogPosition()
    {
        Debug.Log(GetComponent<RectTransform>().anchoredPosition);
    }
}
