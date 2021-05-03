using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationsRoll : MonoBehaviour
{
    public float animationTime = 1f;
    public float inactiveButtonsScale = 0.7f;
    public GameParametersSingleton parameters;
    public AnimationCurve animationCurve;

    [SerializeField] private List<RectTransform> _locations;
    private RectTransform _rectTransform;
    private HorizontalLayoutGroup _horizontalLayoutGroup;

    private int _currentIndex;
    private int _previousIndex;
    private bool _isAnimating = false;

    private void Awake()
    {
        foreach (RectTransform child in transform)
        {
            _locations.Add(child);
        }
        _currentIndex = 0;
        _rectTransform = GetComponent<RectTransform>();
        _horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        // startpos
        float width = _locations[0].sizeDelta.x;
        float spacing = _horizontalLayoutGroup.spacing;
        float num = _locations.Count;
        float x;
        if (num % 2 == 0)
            x = (width + spacing) / 2 + (num / 2 - 1) * (width + spacing);
        else
            x = (num - 1) / 2 * (width + spacing);

        _rectTransform.anchoredPosition = new Vector2(x, _rectTransform.anchoredPosition.y);

        // scale
        for (int i = 1; i < _locations.Count; i++)
        {
            _locations[i].localScale = Vector3.one * inactiveButtonsScale;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i >= parameters.finishedLevelsOnLocation.Count) break;

            Transform child = transform.GetChild(i);

            Button button = child.GetComponentInChildren<Button>();
            button.interactable = true;

            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.gameObject.SetActive(false);
        }
    }

    [ContextMenu("next")]
    public void Next()
    {
        if (_isAnimating) return;
        if (_currentIndex == Mathf.Clamp(_currentIndex + 1, 0, _locations.Count - 1)) return;
        _previousIndex = _currentIndex;
        _currentIndex++;

        StartCoroutine(AnimationCoroutine(-1));
    }

    [ContextMenu("prev")]
    public void Previous()
    {
        if (_isAnimating) return;
        if (_currentIndex == Mathf.Clamp(_currentIndex - 1, 0, _locations.Count - 1)) return;
        _previousIndex = _currentIndex;
        _currentIndex--;

        StartCoroutine(AnimationCoroutine(1));
    }

    private IEnumerator AnimationCoroutine(float direction)
    {
        _isAnimating = true;

        var previousLocation = _locations[_previousIndex];
        var currentLocation = _locations[_currentIndex];

        float width = _locations[0].sizeDelta.x;
        float spacing = _horizontalLayoutGroup.spacing;

        float startX = _rectTransform.anchoredPosition.x;
        float endX = startX + (width + spacing) * direction;

        float t;
        float animT = 0;

        while (true)
        {
            t = animationCurve.Evaluate(animT);

            ChangeSize(previousLocation, currentLocation, t);
            ChangePosition(startX, endX, t);

            if (animT >= 1) break;
            animT += Time.deltaTime / animationTime;
            yield return null;
        }

        _isAnimating = false;
    }

    private void ChangeSize(RectTransform previousLocation, RectTransform currentLocation, float t)
    {
        previousLocation.localScale = Mathf.Lerp(1, inactiveButtonsScale, t) * Vector3.one;
        currentLocation.localScale = Mathf.Lerp(inactiveButtonsScale, 1, t) * Vector3.one;
    }

    private void ChangePosition(float startX, float endX, float t)
    {
        float x = Mathf.Lerp(startX, endX, t);
        _rectTransform.anchoredPosition = new Vector2(x, _rectTransform.anchoredPosition.y);
    }
}
