using UnityEngine;
using TMPro;

public class BuyItemButton : MonoBehaviour
{
    
    public Item item;

    public TextMeshProUGUI buttonText;
    public GameObject badge;

    private Inventory _inventory;
    private GameParametersSingleton _parameters;

    private TextMeshProUGUI _badgeText;

    private int counter;

    private void Awake()
    {
        Menu menu = FindObjectOfType<Menu>();
        _inventory = menu.inventory;
        _parameters = menu.parameters;

        _badgeText = badge.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        counter = 0;
        if (item is ActiveItem)
        {
            foreach (ActiveItem stashedItem in _inventory.stashedActiveItems)
            {
                if (item == stashedItem) counter++;
            }
        }
        else if (item is PassiveItem)
        {
            foreach (PassiveItem stashedItem in _inventory.stashedPassiveItems)
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
        _parameters.goldAmount -= item.price;

        if (counter == 0)
        {
            badge.SetActive(true);
        }
        counter++;
        _badgeText.text = counter.ToString();

        _inventory.StashItem(item);

        _parameters.Save();
    }
}
