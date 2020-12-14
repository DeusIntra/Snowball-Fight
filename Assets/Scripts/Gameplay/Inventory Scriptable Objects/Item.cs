﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public new string name;
    public int price;
    public Image image;
    public List<ItemEffect> effects;
}

