using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemButton : MonoBehaviour
{
    public ActiveItem activeItem;
    public int itemIndex;
    public Inventory inventory;

    public void UseItem()
    {
        foreach (ItemEffect effect in activeItem.effects)
        {
            switch (effect.name)
            {
                case "Heal":
                    Heal((int)effect.value);
                    break;
                default:
                    Debug.LogError("Effect " + effect.name + " was not found");
                    break;
            }
        }

        inventory.activeItems.RemoveAt(itemIndex);
    }

    #region Active effects
    private void Heal(int value)
    {
        Debug.Log("heal " + value);
    }
    #endregion

}
