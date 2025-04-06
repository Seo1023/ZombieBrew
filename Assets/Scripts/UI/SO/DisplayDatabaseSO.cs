using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisplaysType;

[CreateAssetMenu(fileName = "DisplayDatabase", menuName = "Inventory/Database")]
public class DisplayDatabaseSO : ScriptableObject
{
    public List<DisplaySO> displays = new List<DisplaySO>();

    private Dictionary<int, DisplaySO> displayById;
    private Dictionary<string, DisplaySO> displayByName;

    public void Initialize()
    {
        displayById = new Dictionary<int, DisplaySO>();
        displayByName = new Dictionary<string, DisplaySO>();

        foreach (var structure in displays)
        {
            displayById[structure.id] = structure;
            displayByName[structure.name] = structure;
        }
    }

    public DisplaySO GetDisplayByld(int id)
    {
        if (displayById == null)
        {
            Initialize();
        }
        if (displayById.TryGetValue(id, out DisplaySO item))
            return item;

        return null;
    }
    public DisplaySO GetDisplayByName(string name)
    {
        if (displayByName == null)
        {
            Initialize();
        }
        if (displayByName.TryGetValue(name, out DisplaySO item))
            return item;
        return null;
    }

    public List<DisplaySO> GetDisplayByType(displaysType type)
    {
        return displays.FindAll(item => item.displaysType == type);
    }
}
