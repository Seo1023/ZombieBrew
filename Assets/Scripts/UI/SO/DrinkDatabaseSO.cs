using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DrinksType;

[CreateAssetMenu(fileName = "DrinkDatabase", menuName = "Inventory/Database")]
public class DrinkDatabaseSO : ScriptableObject
{
    public List<DrinkSO> drinks = new List<DrinkSO>();

    private Dictionary<int, DrinkSO> drinkById;
    private Dictionary<string, DrinkSO> drinkByName;

    public void Initialize()
    {
        drinkById = new Dictionary<int, DrinkSO>();
        drinkByName = new Dictionary<string, DrinkSO>();

        foreach (var drink in drinks)
        {
            drinkById[drink.id] = drink;
            drinkByName[drink.name] = drink;
        }
    }

    public DrinkSO GetStructureByld(int id)
    {
        if (drinkById == null)
        {
            Initialize();
        }
        if (drinkById.TryGetValue(id, out DrinkSO item))
            return item;

        return null;
    }
    public DrinkSO GetStructureByName(string name)
    {
        if (drinkByName == null)
        {
            Initialize();
        }
        if (drinkByName.TryGetValue(name, out DrinkSO item))
            return item;
        return null;
    }

    public List<DrinkSO> GetStructureByType(drinksType type)
    {
        return drinks.FindAll(item => item.drinksType == type);
    }
}
