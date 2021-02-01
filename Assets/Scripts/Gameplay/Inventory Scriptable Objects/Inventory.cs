using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 3)]
public class Inventory : ScriptableObject
{
    public List<ActiveItem> stashedActiveItems;
    public List<PassiveItem> stashedPassiveItems;

    public List<ActiveItem> activeItems;
    public List<PassiveItem> passiveItems;

    public int maxActiveItemsCount = 5;
    public int maxPassiveItemsCount = 3;

    public void OnEnable()
    {
        if (stashedPassiveItems is null)
        {
            stashedPassiveItems = new List<PassiveItem>();
        }
        if (stashedActiveItems is null)
        {
            stashedActiveItems = new List<ActiveItem>();
        }
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
