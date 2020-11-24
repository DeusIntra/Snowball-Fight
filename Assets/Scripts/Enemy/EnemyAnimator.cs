using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public MeshAnimator meshAnimator;

    private Coroutine _coroutine;


    public void Walk()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        meshAnimator.Play("Walk");
    }


    public void Throw()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        meshAnimator.Play("Throw");

        _coroutine = StartCoroutine(DelayWalk());
    }


    public void Die()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        meshAnimator.Play("Die");
    }

    public void SetFPS(float FPS)
    {
        meshAnimator.SetFPS(FPS);
    }


    public float GetFPS()
    {
        return meshAnimator.GetFPS();
    }


    public void Pause(float seconds)
    {
        meshAnimator.Pause(seconds);
    }


    private IEnumerator DelayWalk()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        while (meshAnimator.isPaused) yield return null;
        Walk();
    }
}
