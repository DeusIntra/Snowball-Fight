using UnityEngine;
using TMPro;

public class BuyItemButton : MonoBehaviour
{    
    public Item item;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;
    public TextMeshProUGUI badge;

    public float scaleMultiplier = 1f;
    public float itemOffsetZ = -10f;
    public float itemYRotation = 100f;

    private Inventory _inventory;
    private GameParametersSingleton _parameters;

    private TextMeshProUGUI _moneyAmountText;
    private EquipPanel _equipPanel;


    private int counter;

    private void Awake()
    {
        Menu menu = FindObjectOfType<Menu>();
        _inventory = menu.inventory;
        _parameters = menu.parameters;

        ShopPanel shopPanel = GetComponentInParent<ShopPanel>();
        _moneyAmountText = shopPanel.moneyText;

        ShopTabContent shopTabContent = GetComponentInParent<ShopTabContent>();
        _equipPanel = shopTabContent.equipPanel;
    }

    private void Start()
    {
        counter = 0;
        if (item is ActiveItem)
        {
            foreach (ActiveItem stashedItem in _inventory.activeItemsStashed)
            {
                if (item == stashedItem) counter++;
            }
        }
        else if (item is PassiveItem)
        {
            foreach (PassiveItem stashedItem in _inventory.passiveItemsStashed)
            {
                if (item == stashedItem) counter++;
            }
        }

        if (badge)
        {
            if (counter == 0) badge.gameObject.SetActive(false);
            else badge.text = counter.ToString();
        }

        itemName.text = item.name;
        price.text = $"Price: {item.price}";

        // create 3d item
        GameObject itemGO = Instantiate(item.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);
        itemGO.transform.localScale *= scaleMultiplier;

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
    }

    public void Buy()
    {
        if (_parameters.goldAmount < item.price) return;

        _parameters.goldAmount -= item.price;
        _moneyAmountText.text = _parameters.goldAmount.ToString();

        if (badge != null)
        {
            if (counter == 0)
            {
                badge.gameObject.SetActive(true);
            }
            counter++;
            badge.text = counter.ToString();
        }

        _inventory.StashItem(item);

        _parameters.Save();

        _equipPanel.UpdateStash();
    }
}
