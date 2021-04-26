using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : MonoBehaviour
{
    public float scaleMultiplier = 0.5f;
    public GameObject EquipButtonPrefab;
    public Inventory inventory;
    public ItemSlotGroup itemSlotGroup;

    private ItemData.Type _type;
    
    private void Start()
    {
        _type = itemSlotGroup.type;
        itemSlotGroup.equipPanel = this;
        UpdateStash();
    }

    public void UpdateStash()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        switch (_type)
        {
            case ItemData.Type.Active:
                FillPanel(inventory.activeItemsStashed);
                break;
            case ItemData.Type.Passive:
                FillPanel(inventory.passiveItemsStashed);
                break;
            default:
                Debug.LogError("wat");
                break;
        }
    }

    private void FillPanel(IList list)
    {
        foreach (Item item in list)
        {
            GameObject equipButtonGO = Instantiate(EquipButtonPrefab, transform);
            EquipButton equipButton = equipButtonGO.GetComponent<EquipButton>();
            equipButton.item = item;
            equipButton.type = _type;
            equipButton.inventory = inventory;
            equipButton.scaleMultiplier = scaleMultiplier;
            equipButton.SpawnItem();
        }
    }

}
