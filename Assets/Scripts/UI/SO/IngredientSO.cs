using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientsType;
using static StructuresType;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Inventory/ingredient")]
public class IngredientSO : ScriptableObject
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    public ingredientsType ingredientsType;
    public int level;
    public int? Quantity;
    public string iconPath;
    public Sprite icon;

    public override string ToString()
    {
        return $"[{id}] {name} ({type}) - ·¹º§ : {level}";
    }
    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? name : nameEng; }
    }
}
