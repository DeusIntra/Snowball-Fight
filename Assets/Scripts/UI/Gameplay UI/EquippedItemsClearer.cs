using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsClearer : MonoBehaviour
{
    public GameParametersSingleton parameters;

    private Inventory _inventory;

    private void Awake()
    {
        _inventory = parameters.inventory;
    }

    public void OnReturnToMenu()
    {
        _inventory.UnequipActiveItems();
        parameters.Save();
    }
}
