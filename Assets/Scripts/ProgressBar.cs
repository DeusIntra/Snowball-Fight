using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float timeToFillSeconds = 2f;
    public float currentFill { get; private set; } = 0f;

    [SerializeField] private Image _mask = null;
    private Coroutine _coroutine;

    public void StartFilling()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartFillingCoroutine());
    }

    public void StopAndReset()
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);

        currentFill = 0f;
        _mask.fillAmount = 0f;
    }

    private IEnumerator StartFillingCoroutine()
    {
        while (currentFill < 1f)
        {            
            float currentFillSeconds = currentFill * timeToFillSeconds;

            currentFillSeconds += Time.deltaTime;
            currentFill = currentFillSeconds / timeToFillSeconds;
            _mask.fillAmount = currentFill;

            yield return null;
        }
    }
}
