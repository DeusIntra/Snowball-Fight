using UnityEngine;

public class ActiveItemButton : MonoBehaviour
{
    public ActiveItem activeItem;
    public int itemIndex;
    public GameParametersSingleton parameters;
    public float itemOffsetZ = -500;
    public float scaleMultiplier = 1f;
    public float itemYRotation = 100f;

    [HideInInspector] public InventoryButton inventoryButton;
    private bool spawned = false;

    public void UseItem()
    {
        if (inventoryButton == null)
        {
            Debug.LogError("Inventory button was not found");
            return;
        }

        Inventory inventory = parameters.inventory;

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

        inventory.activeItemsEquipped.RemoveAt(itemIndex);
        parameters.Save();

        inventoryButton.OnPress();

        foreach (Transform button in transform.parent)
        {
            Destroy(button.gameObject);
        }
    }

    public void SpawnItem()
    {
        if (spawned) return;
        spawned = true;

        GameObject itemGO = Instantiate(activeItem.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);
        itemGO.transform.localScale *= scaleMultiplier;
        GameObjectUtil.IterateChildren(itemGO, SetUILayer, true);

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
    }

    private void SetUILayer(GameObject obj)
    {
        obj.layer = 5;
    }

    #region Active effects
    private void Heal(int value)
    {
        Debug.Log("heal " + value);
    }
    #endregion
}
