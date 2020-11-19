using UnityEngine;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public int max = 12;
    public UnityEvent onChange;

    private int current = 0;

    public float currentFraction => (float) current / (float) max;

    public void Add(int amount)
    {
        current += amount;

        if (current > max) current = max;
        onChange.Invoke();
    }

    public void Sub(int amount)
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
