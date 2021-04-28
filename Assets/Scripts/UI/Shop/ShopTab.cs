using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTab : MonoBehaviour
{
    public List<GameObject> setInactive;
    public GameObject setActive;

    private Button _button;
    private List<ShopTab> _otherTabs;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _otherTabs = new List<ShopTab>();
        foreach (Transform tabTransform in transform.parent)
        {
            ShopTab tab = tabTransform.GetComponent<ShopTab>();

            if (tab == null || tab == this) continue;

            _otherTabs.Add(tab);
        }
    }

    public void Open()
    {
        foreach (var obj in setInactive)
        {
            obj.SetActive(false);
        }

        setActive.SetActive(true);

        foreach (var tab in _otherTabs)
        {
            Button button = tab.GetComponent<Button>();

            if (button == null)
            {
                Debug.LogError("wat");
                continue;
            }

            button.interactable = true;
        }
        _button.interactable = false;
    }
}
