using UnityEngine;

public class Spell : MonoBehaviour
{
    public Animator animator;

    public void playAnimation()
    {
        animator.Play("Spell");
    }
}
