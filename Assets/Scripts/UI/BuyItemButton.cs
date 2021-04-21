﻿using UnityEngine;
using TMPro;

public class BuyItemButton : MonoBehaviour
{    
    public Item item;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;
    public TextMeshProUGUI badge;

    public float itemYRotation = 100f;

    private Inventory _inventory;
    private GameParametersSingleton _parameters;

    private TextMeshProUGUI _moneyAmountText;

    private float itemOffsetZ = -10f;

    private int counter;

    private void Awake()
    {
        Menu menu = FindObjectOfType<Menu>();
        _inventory = menu.inventory;
        _parameters = menu.parameters;

        //_badgeText = badge.GetComponentInChildren<TextMeshProUGUI>();

        ShopPanel shopPanel = GetComponentInParent<ShopPanel>();
        _moneyAmountText = shopPanel.moneyText;
    }

    private void Start()
    {
        counter = 0;
        if (item is ActiveItem)
        {
            foreach (ActiveItem stashedItem in _inventory.stashedActiveItems)
            {
                if (item == stashedItem) counter++;
            }
        }
        else if (item is PassiveItem)
        {
            foreach (PassiveItem stashedItem in _inventory.stashedPassiveItems)
            {
                if (item == stashedItem) counter++;
            }
        }

        if (badge)
        {
            if (counter == 0) badge.gameObject.SetActive(false);
            else badge.text = counter.ToString();
        }

        itemName.text = item.name;
        price.text = $"Price: {item.price}";

        // create 3d item
        GameObject itemGO = Instantiate(item.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
    }

    public void Buy()
    {
        if (_parameters.goldAmount <= item.price) return;

        _parameters.goldAmount -= item.price;
        _moneyAmountText.text = _parameters.goldAmount.ToString();

        if (counter == 0)
        {
            badge.gameObject.SetActive(true);
        }
        counter++;
        badge.text = counter.ToString();

        _inventory.StashItem(item);

        _parameters.Save();
    }
}
