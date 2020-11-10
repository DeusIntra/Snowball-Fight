﻿using UnityEngine;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public int max = 15;
    public UnityEvent onChange;

    private int current = 0;

    public float currentFraction => (float) current / (float) max;

    public void Add(int amount)
    {
        current += amount;

        if (current > max) current = max;
        onChange.Invoke();
    }

    public bool Sub(int amount)
    {
        if (current < amount)
            return false;

        current -= amount;
        onChange.Invoke();
        return true;
    }


}