using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public new string name;
    public int price;
    public GameObject prefab;
    public List<ItemEffect> effects;
}

