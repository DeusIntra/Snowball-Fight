using UnityEngine;

public class ActiveItemButton : MonoBehaviour
{
    public ActiveItem activeItem;
    public int itemIndex;
    public GameParametersSingleton parameters;

    private Inventory _inventory;
    private InventoryButton inventoryButton;

    private void OnEnable()
    {
        _inventory = parameters.inventory;
    }

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

        if (activeItem is ThrowableItem)
        {
            Player player = FindObjectOfType<Player>();
            PlayerShooter shooter = player.GetComponent<PlayerShooter>();
            shooter.nextSnowballPrefab = ((ThrowableItem)activeItem).throwablePrefab;
        }

        _inventory.activeItemsEquipped.RemoveAt(itemIndex);
        parameters.Save();

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
