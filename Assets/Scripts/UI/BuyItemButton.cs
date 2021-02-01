using UnityEngine;
using TMPro;

public class BuyItemButton : MonoBehaviour
{
    public Inventory inventory;
    public Item item;

    public TextMeshProUGUI buttonText;
    public GameObject badge;

    private TextMeshProUGUI _badgeText;

    private int counter;

    private void Awake()
    {
        _badgeText = badge.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        counter = 0;
        if (item is ActiveItem)
        {
            foreach (ActiveItem stashedItem in inventory.stashedActiveItems)
            {
                if (item == stashedItem) counter++;
            }
        }
        else if (item is PassiveItem)
        {
            foreach (PassiveItem stashedItem in inventory.stashedPassiveItems)
            {
                if (item == stashedItem) counter++;
            }
        }

        if (counter == 0) badge.SetActive(false);
        else _badgeText.text = counter.ToString();

        buttonText.text = item.name;
    }

    public void Buy()
    {
        Debug.Log("TODO: remove money");

        if (counter == 0)
        {
            badge.SetActive(true);
        }
        counter++;
        _badgeText.text = counter.ToString();

        if (item is ActiveItem)
        {
            inventory.stashedActiveItems.Add((ActiveItem)item);
        }
        else if (item is PassiveItem)
        {
            inventory.stashedPassiveItems.Add((PassiveItem)item);
        }
    }
}
