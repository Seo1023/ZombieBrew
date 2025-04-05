using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientsType;
using static UnityEditor.Progress;


[CreateAssetMenu(fileName = "IngredientDatabase", menuName = "Inventory/Database")]
public class IngredientDatabaseSO : ScriptableObject
{
    public List<IngredientSO> ingredients = new List<IngredientSO>();

    private Dictionary<int, IngredientSO> ingredientById;
    private Dictionary<string, IngredientSO> ingredientByName;

    public void Initialize()
    {
        ingredientById = new Dictionary<int, IngredientSO>();
        ingredientByName = new Dictionary<string, IngredientSO>();

        foreach (var ingredient in ingredients)
        {
            ingredientById[ingredient.id] = ingredient;
            ingredientByName[ingredient.name] = ingredient;
        }
    }

    public IngredientSO GetItemByld(int id)
    {
        if (ingredientById == null)
        {
            Initialize();
        }
        if (ingredientById.TryGetValue(id, out IngredientSO item))
            return item;

        return null;
    }
    public IngredientSO GetItemByName(string name)
    {
        if (ingredientByName == null)
        {
            Initialize();
        }
        if (ingredientByName.TryGetValue(name, out IngredientSO item))
            return item;
        return null;
    }

    public List<IngredientSO> GetItemByType(ingredientsType type)
    {
        return ingredients.FindAll(item => item.ingredientsType == type);
    }
}
