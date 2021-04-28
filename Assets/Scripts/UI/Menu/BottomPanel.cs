using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    public AnimatedObject equipmentPanel;

    private AnimatedObject _animatedObject;
    private Menu _menu;

    private void Awake()
    {
        _animatedObject = GetComponent<AnimatedObject>();
        _menu = FindObjectOfType<Menu>();
    }

    public void ChooseLocation(int locationIndex)
    {
        _menu.ChooseLocationIndex(locationIndex);
        
        if (_menu.inventory.IsEmpty == false)
        {
            _animatedObject.Animate(1);
            equipmentPanel.Animate(0);
        }
    }
}
