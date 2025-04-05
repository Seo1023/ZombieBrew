using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DrinksType;

[SerializeField]
public class DrinkData 
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    [NonSerialized]
    public drinksType drinksType;
    public int curedlevel;
    public int price;
    public string iconPath;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out drinksType Type))
        {
            drinksType = Type;
        }
        else
        {
            Debug.LogError($"������ '{name}'�� ��ȿ���� ���� ������ Ÿ�� : {type}");
            drinksType = drinksType.coffee;
        }
    }
}
