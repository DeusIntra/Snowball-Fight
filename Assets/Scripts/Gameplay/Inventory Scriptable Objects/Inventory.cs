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
        if (stashedPassiveItems is null) stashedPassiveItems = new List<PassiveItem>();
        if (stashedActiveItems is null) stashedActiveItems = new List<ActiveItem>();
        if (activeItems is null) activeItems = new List<ActiveItem>();
        if (passiveItems is null) passiveItems = new List<PassiveItem>();
    }

    public void ClearPassiveItems()
    {
        passiveItems = new List<PassiveItem>();
    }

    public void StashItem(Item item)
    {
        if (item is ActiveItem)
        {
            stashedActiveItems.Add((ActiveItem)item);
        }
        else if (item is PassiveItem)
        {
            stashedPassiveItems.Add((PassiveItem)item);
        }            
    }

    public void EquipItem(Item item)
    {

    }
}
