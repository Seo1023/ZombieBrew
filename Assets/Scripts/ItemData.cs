using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public string nameEng;
    public string description;
    public string itemTypeString;
    [NonSerialized]
    public ItemType itemtype;
    public int? price;
    public int? level;
    public bool? isStackable;
    public string iconPath;

    
}
