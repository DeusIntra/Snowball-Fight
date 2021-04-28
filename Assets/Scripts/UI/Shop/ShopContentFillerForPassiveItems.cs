using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentFillerForPassiveItems : MonoBehaviour
{
    public Transform content;
    public BuyItemButton buttonPrefab;
    public Inventory inventory;

    private List<PassiveItem> _passiveItems;

    private void Awake()
    {
        _passiveItems = new List<PassiveItem>();
        foreach (var item in inventory.passiveItemsList)
        {
            _passiveItems.Add(item);
        }
    }

    private void Start()
    {
        UpdateContent();
    }

    public void UpdateContent()
    {
        FilterItemsWithoutPrefabs();
        FilterOwnedItems();
        SortByPriceAsc();

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in _passiveItems)
        {
            BuyItemButton button = Instantiate(buttonPrefab, content);
            button.item = item;
            button.OnItemBought += UpdateContent;
        }
    }

    private void SortByPriceAsc()
    {
        // insertion sort because i like it
        for (int i = 0; i < _passiveItems.Count; i++)
        {
            int min = i;
            for (int j = i + 1; j < _passiveItems.Count; j++)
            {
                if (_passiveItems[min].price > _passiveItems[j].price)
                {
                    min = j;
                }
            }
            var tmp = _passiveItems[i];
            _passiveItems[i] = _passiveItems[min];
            _passiveItems[min] = tmp;
        }
    }

    private void FilterItemsWithoutPrefabs()
    {
        for (int i = _passiveItems.Count - 1; i >= 0; i--)
        {
            var item = _passiveItems[i];
            if (item.prefab == null)
            {
                _passiveItems.Remove(item);
            }
        }
    }

    private void FilterOwnedItems()
    {
        for (int i = _passiveItems.Count - 1; i >= 0; i--)
        {
            var item = _passiveItems[i];
            if (inventory.passiveItemsStashed.Contains(item) ||
                inventory.passiveItemsEquipped.Contains(item))
            {
                _passiveItems.Remove(item);
            }
        }
    }
}
