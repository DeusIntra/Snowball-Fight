using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 3)]
public class Inventory : ScriptableObject
{
    public List<ActiveItem> stashedActiveItems;
    public List<PassiveItem> stashedPassiveItems;

    public List<ActiveItem> activeItemsEquipped;
    public List<PassiveItem> passiveItemsEquipped;

    public int maxActiveItemsCount = 5;
    public int maxPassiveItemsCount = 3;

    public List<ActiveItem> activeItemsList;
    public List<PassiveItem> passiveItemsList;

    public void OnEnable()
    {
        if (stashedPassiveItems is null) stashedPassiveItems = new List<PassiveItem>();
        if (stashedActiveItems is null) stashedActiveItems = new List<ActiveItem>();
        if (activeItemsEquipped is null) activeItemsEquipped = new List<ActiveItem>();
        if (passiveItemsEquipped is null) passiveItemsEquipped = new List<PassiveItem>();
    }

    public void ClearPassiveItems()
    {
        passiveItemsEquipped = new List<PassiveItem>();
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

    public List<ItemData> GetItemData()
    {
        List<ItemData> list = new List<ItemData>();

        foreach (Item item in stashedActiveItems)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Active));
        }

        foreach (Item item in stashedPassiveItems)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Passive));
        }

        return list;
    }

    public void SetStashedItems(List<ItemData> listOfItems)
    {
        stashedActiveItems = new List<ActiveItem>();
        stashedPassiveItems = new List<PassiveItem>();

        foreach(ItemData itemData in listOfItems)
        {
            Item item = GetItem(itemData);
            if (itemData.type == ItemData.Type.Active)
                stashedActiveItems.Add((ActiveItem)item);
            else
                stashedPassiveItems.Add((PassiveItem)item);
        }
    }

    private Item GetItem(ItemData itemData)
    {
        string name = itemData.name;
        ItemData.Type type = itemData.type;

        switch (type)
        {
            case ItemData.Type.Active:
                foreach (ActiveItem item in activeItemsList)
                {
                    if (name.Equals(item.name))
                        return item;
                }
                Debug.LogError("Item not found");
                return null;
            case ItemData.Type.Passive:
                foreach (PassiveItem item in passiveItemsList)
                {
                    if (name.Equals(item.name))
                        return item;
                }
                Debug.LogError("Item not found");
                return null;
            default:
                Debug.LogError("No items of type " + type.ToString());
                return null;
        }
    }
}
