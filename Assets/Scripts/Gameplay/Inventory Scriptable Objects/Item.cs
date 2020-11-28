using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public new string name;
    public int price;
    public Image image;
    public List<ItemEffect> effects;
}

public class ActiveItem : Item
{

}

[CreateAssetMenu(fileName = "New Passive Item", menuName = "ScriptableObjects/Passive Item", order = 6)]
public class PassiveItem : Item
{

}
