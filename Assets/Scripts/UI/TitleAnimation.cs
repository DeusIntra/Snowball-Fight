using System.Collections;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public bool animateOnStart = true;

    public float rotationSpeed = 1f;
    public float rotationMultiplier = 1f;
    public AnimationCurve rotationZCurve;

    [Space]
    public float scaleSpeed = 1f;
    public float scaleMultiplier = 1f;
    public AnimationCurve scaleXYCurve;

    private float _time;
    private Coroutine _coroutine;

    private void Start()
    {
        if (animateOnStart)
            Animate();
    }

    public void Animate()
    {
        _time = Random.Range(0f, 1f);
        Stop();
        _coroutine = StartCoroutine(AnimationCoroutine());
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator AnimationCoroutine()
    {
        Quaternion rotation = new Quaternion();
        Vector3 scale = Vector3.one;

        while (true)
        {
            _time += Time.deltaTime;

            float rotationZ = rotationZCurve.Evaluate(_time * rotationSpeed);            
            rotation.eulerAngles = new Vector3(0, 0, rotationZ * rotationMultiplier);
            transform.rotation = rotation;

            float scaleXY = scaleXYCurve.Evaluate(_time * scaleSpeed);
            scale.x = scaleXY;
            scale.y = scaleXY;
            transform.localScale = Vector3.one + scale * scaleMultiplier;

            yield return null;
        }
    }
}
