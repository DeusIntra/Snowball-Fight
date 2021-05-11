using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 3)]
public class Inventory : ScriptableObject
{
    public List<ActiveItem> activeItemsStashed;
    public List<PassiveItem> passiveItemsStashed;

    public List<ActiveItem> activeItemsEquipped;
    public List<PassiveItem> passiveItemsEquipped;

    public int maxActiveItemsCount = 5;
    public int maxPassiveItemsCount = 3;

    public List<ActiveItem> activeItemsList;
    public List<PassiveItem> passiveItemsList;

    public bool IsEmpty
    {
        get
        {
            bool empty =
                activeItemsStashed.Count == 0 &&
                passiveItemsStashed.Count == 0 &&
                activeItemsEquipped.Count == 0 &&
                passiveItemsEquipped.Count == 0;
            return empty;
        }
    }

    public void OnEnable()
    {
        if (passiveItemsStashed is null) passiveItemsStashed = new List<PassiveItem>();
        if (activeItemsStashed is null) activeItemsStashed = new List<ActiveItem>();
        if (activeItemsEquipped is null) activeItemsEquipped = new List<ActiveItem>();
        if (passiveItemsEquipped is null) passiveItemsEquipped = new List<PassiveItem>();
    }

    public void UnequipPassiveItems()
    {
        for (int i = passiveItemsEquipped.Count -1; i >= 0; i--)
        {
            Unequip(passiveItemsEquipped[i]);
        }
    }

    public void UnequipActiveItems()
    {
        for (int i = activeItemsEquipped.Count - 1; i >= 0; i--)
        {
            Unequip(activeItemsEquipped[i]);
        }
    }

    public void StashItem(Item item)
    {
        if (item is ActiveItem)
        {
            activeItemsStashed.Add((ActiveItem)item);
        }
        else if (item is PassiveItem)
        {
            passiveItemsStashed.Add((PassiveItem)item);
        }
        
    }

    public void Equip(Item item)
    {
        if (item is ActiveItem)
        {
            activeItemsStashed.Remove((ActiveItem)item);
            activeItemsEquipped.Add((ActiveItem)item);
        }
        else
        {
            passiveItemsStashed.Remove((PassiveItem)item);
            passiveItemsEquipped.Add((PassiveItem)item);
        }
    }

    public void Unequip(Item item)
    {
        if (item is ActiveItem)
        {
            Debug.Log("unequipped " + item.name);
            activeItemsEquipped.Remove((ActiveItem)item);
            activeItemsStashed.Add((ActiveItem)item);
        }
        else
        {
            passiveItemsEquipped.Remove((PassiveItem)item);
            passiveItemsStashed.Add((PassiveItem)item);
        }
    }

    public List<ItemData> GetItemData()
    {
        List<ItemData> list = new List<ItemData>();

        foreach (Item item in activeItemsStashed)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Active));
        }

        foreach (Item item in passiveItemsStashed)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Passive));
        }

        foreach (Item item in activeItemsEquipped)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Active));
        }

        foreach (Item item in passiveItemsEquipped)
        {
            list.Add(new ItemData(item.name, ItemData.Type.Passive));
        }

        return list;
    }

    public void SetStashedItems(List<ItemData> listOfItems)
    {
        activeItemsStashed = new List<ActiveItem>();
        passiveItemsStashed = new List<PassiveItem>();

        foreach(ItemData itemData in listOfItems)
        {
            Item item = GetItem(itemData);
            if (itemData.type == ItemData.Type.Active)
                activeItemsStashed.Add((ActiveItem)item);
            else
                passiveItemsStashed.Add((PassiveItem)item);
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
