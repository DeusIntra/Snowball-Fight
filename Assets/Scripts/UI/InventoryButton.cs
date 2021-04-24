using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public float slowMotion = 0.05f;
    public EnablerDisabler enableOnOpen;
    public EnablerDisabler disableOnOpen;
    public Transform inventoryPanel;
    public GameObject itemButtonPrefab;
    public Inventory inventory;

    public bool isOpen { get; private set; } = false;

    public void OnPress()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Time.timeScale = slowMotion;
            Time.fixedDeltaTime = slowMotion * 0.02f;
            enableOnOpen.EnableObjects();
            disableOnOpen.DisableObjects();
            AddItemButtons();
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            enableOnOpen.DisableObjects();
            disableOnOpen.EnableObjects();
        }
    }

    private void AddItemButtons()
    {
        if (inventoryPanel.childCount == 0)
        {
            List<ActiveItem> activeItems = inventory.activeItemsEquipped;
            for (int i = 0; i < activeItems.Count; i++)
            {
                GameObject go = Instantiate(itemButtonPrefab, inventoryPanel);
                ActiveItemButton itemButton = go.GetComponent<ActiveItemButton>();

                itemButton.activeItem = activeItems[i];
                itemButton.itemIndex = i;
                itemButton.inventory = inventory;
            }
        }
    }
}
