using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientsType;

[SerializeField]
public class IngredientData 
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    [NonSerialized]
    public ingredientsType ingredientsType;
    public int level;
    public int? Quantity;
    public string iconPath;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out ingredientsType Type))
        {
            ingredientsType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 아이템 타입 : {type}");
            ingredientsType = ingredientsType.ingredient;
        }
    }
}
