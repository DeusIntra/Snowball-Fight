using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    public GameParametersSingleton parameters;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = parameters.goldAmount.ToString();
    }
}
