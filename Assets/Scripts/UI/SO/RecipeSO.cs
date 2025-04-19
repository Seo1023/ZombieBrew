using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Coffee/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public IngredientSO[] ingredients = new IngredientSO[3];
    public DrinkSO resultDrink;

    public bool Match(IngredientSO[] input)
    {
        if (input.Length != ingredients.Length)
            return false;

        var inputList = new System.Collections.Generic.List<IngredientSO>(input);
        var recipeList = new System.Collections.Generic.List<IngredientSO>(ingredients);

        foreach (var ingredient in recipeList)
        {
            if (!inputList.Remove(ingredient))
                return false;
        }

        return true;
    }
}
