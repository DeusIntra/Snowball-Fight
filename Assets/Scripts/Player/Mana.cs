using UnityEngine;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public float max = 120;
    public UnityEvent onChange;

    private float current = 0;

    public float currentFraction => current / max;

    public void Add(float amount)
    {
        current += amount;

        if (current > max) current = max;
        onChange.Invoke();
    }

    public void Sub(float amount)
    {
        current -= amount;

        if (current < 0) current = 0;
        onChange.Invoke();
    }

    public void Zero()
    {
        current = 0;
        onChange.Invoke();
    }


}
