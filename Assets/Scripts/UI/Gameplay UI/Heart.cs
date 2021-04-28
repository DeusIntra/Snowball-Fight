using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public List<GameObject> parts;

    private int _value = 4;
    public int value 
    {
        get => _value;
        set 
        { 
            if (_value != value)
            {
                _value = value;
                onChange();
            }            
        } 
    }


    private void onChange()
    {
        if (_value < 0) _value = 0;
        else if (_value > 4) _value = 4;

        for (int i = 0; i < 4; i++)
        {
            if (i < _value) parts[i].SetActive(true);
            else parts[i].SetActive(false);
        }
    }
}
