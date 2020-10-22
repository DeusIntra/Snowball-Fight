using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public MeshAnimator meshAnimator;

    private Coroutine _coroutine;

    public void Walk()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        animator.Play("walk");
        meshAnimator.Play("Walk");
    }

    public void Swing()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        animator.Play("swing");
        meshAnimator.Play("Swing");
    }

    public void Throw()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        animator.Play("throw");
        meshAnimator.Play("Throw");

        _coroutine = StartCoroutine(DelayWalk());
    }

    public void Die()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        animator.Play("die");
        meshAnimator.Play("Die");
    }

    public void SetFPS(float FPS)
    {
        meshAnimator.SetFPS(FPS);
    }

    public void SetSpeed(float speed)
    {
        animator.speed = speed;
    }

    private IEnumerator DelayWalk()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Walk();
    }
}
