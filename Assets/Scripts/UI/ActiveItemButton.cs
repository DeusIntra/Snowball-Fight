using UnityEngine;

public class ActiveItemButton : MonoBehaviour
{
    public ActiveItem activeItem;
    public int itemIndex;
    public Inventory inventory;

    private InventoryButton inventoryButton;

    public void UseItem()
    {
        inventoryButton = transform.parent.parent.GetComponentInChildren<InventoryButton>();

        if (inventoryButton == null)
        {
            Debug.LogError("Inventory button was not found in hierarchy");
            return;
        }

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

        inventoryButton.OnPress();

        foreach (Transform button in transform.parent)
        {
            Destroy(button.gameObject);
        }
    }

    #region Active effects
    private void Heal(int value)
    {
        Debug.Log("heal " + value);
    }
    #endregion

}
