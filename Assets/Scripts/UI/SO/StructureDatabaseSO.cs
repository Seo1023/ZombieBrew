using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructuresType;

[CreateAssetMenu(fileName = "StructureDatabase", menuName = "Inventory/Database")]
public class StructureDatabaseSO : ScriptableObject
{
    public List<StructureSO> structures = new List<StructureSO>();

    private Dictionary<int, StructureSO> structureById;
    private Dictionary<string, StructureSO> structureByName;

    public void Initialize()
    {
        structureById = new Dictionary<int, StructureSO>();
        structureByName = new Dictionary<string, StructureSO>();

        foreach (var structure in structures)
        {
            structureById[structure.id] = structure;
            structureByName[structure.name] = structure;
        }
    }

    public StructureSO GetStructureByld(int id)
    {
        if (structureById == null)
        {
            Initialize();
        }
        if (structureById.TryGetValue(id, out StructureSO item))
            return item;

        return null;
    }
    public StructureSO GetStructureByName(string name)
    {
        if (structureByName == null)
        {
            Initialize();
        }
        if (structureByName.TryGetValue(name, out StructureSO item))
            return item;
        return null;
    }

    public List<StructureSO> GetStructureByType(structuresType type)
    {
        return structures.FindAll(item => item.structurestype == type);
    }
}
