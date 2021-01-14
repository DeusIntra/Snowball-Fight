using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthBar : MonoBehaviour
{
    public int value = 20;
    public List<Heart> hearts;

    public void onChange()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            int n = value - ((i+1) * 4);
            if (n >= 0) hearts[i].value = 4;
            else if (n < -4) hearts[i].value = 0;
            else hearts[i].value = 4 + n;
        }
    }
}
