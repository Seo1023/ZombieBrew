using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DrinksType;

[CreateAssetMenu(fileName = "New Drink", menuName = "Inventory/drink")]

public class DrinkSO : ScriptableObject
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    public drinksType drinksType;
    public int curedlevel;
    public int price;
    public Sprite iconPath;

    [Tooltip("아이콘 리소스 경로 (Resources 폴더 내의 경로)")]
    public string iconResourcesPath;

    public override string ToString()
    {
        return $"[{id}] {name} ({type}) - 가격 : {price}";
    }
    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? name : nameEng; }
    }
}
