using UnityEngine;
using TMPro;

public class BuyItemButton : MonoBehaviour
{    
    public Item item;

    public TextMeshProUGUI buttonText;
    public GameObject badge;

    public float itemYRotation = 100f;

    private Inventory _inventory;
    private GameParametersSingleton _parameters;

    private TextMeshProUGUI _badgeText;

    private float itemOffsetZ = -10f;

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

        // create 3d item
        GameObject itemGO = Instantiate(item.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
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
