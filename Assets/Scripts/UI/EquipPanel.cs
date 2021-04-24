using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : MonoBehaviour
{
    public float scaleMultiplier = 0.5f;
    public GameObject EquipButtonPrefab;
    public Inventory inventory;

    public ItemData.Type type;
    
    void Start()
    {
        UpdateStash();
    }

    public void UpdateStash()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        switch (type)
        {
            case ItemData.Type.Active:
                FillPanel(inventory.stashedActiveItems);
                break;
            case ItemData.Type.Passive:
                FillPanel(inventory.stashedPassiveItems);
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
            equipButton.scaleMultiplier = scaleMultiplier;
            equipButton.SpawnItem();
        }
    }

}
