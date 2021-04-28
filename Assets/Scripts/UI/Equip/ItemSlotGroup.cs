using System.Collections.Generic;
using UnityEngine;

public class ItemSlotGroup : MonoBehaviour
{
    public ItemData.Type type;
    public Inventory inventory;
    public EquipPanel equipPanel;

    private List<ItemSlot> _itemSlots;
    private int equippedCount;

    private void Start()
    {
        _itemSlots = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());
    }

    public bool Equip(Item item)
    {
        if (equippedCount >= _itemSlots.Count) return false;

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            ItemSlot slot = _itemSlots[i];
            if (slot.occupied) continue;

            slot.Equip(item);
            equippedCount++;
            inventory.Equip(item);
            equipPanel.UpdateStash();
            return true;
        }

        return false;
    }

    public void Unequip(Item item)
    {
        if (equippedCount <= 0) return;
        equippedCount--;

        inventory.Unequip(item);

        equipPanel.UpdateStash();        
    }
}
