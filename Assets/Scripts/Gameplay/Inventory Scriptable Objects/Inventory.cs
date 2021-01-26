using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 3)]
public class Inventory : ScriptableObject
{
    public List<Item> stashedItems;
    public List<PassiveItem> passiveItems;
    public List<ActiveItem> activeItems;
    public int maxPassiveItemsCount = 3;
    public int maxActiveItemsCount = 5;

    public void OnEnable()
    {
        
    }

    public void ClearPassiveItems()
    {
        passiveItems = new List<PassiveItem>();
    }

    public void AddPassiveItem()
    {
        if (passiveItems == null)        
            passiveItems = new List<PassiveItem>();

        // TODO: add item        
    }
}
