using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharactersType;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Inventory/Database")]
public class CharacterDatabaseSO : ScriptableObject
{
    public List<CharacterSO> structures = new List<CharacterSO>();

    private Dictionary<int, CharacterSO> characterById;
    private Dictionary<string, CharacterSO> characterByName;

    public void Initialize()
    {
        characterById = new Dictionary<int, CharacterSO>();
        characterByName = new Dictionary<string, CharacterSO>();

        foreach (var structure in structures)
        {
            characterById[structure.id] = structure;
            characterByName[structure.name] = structure;
        }
    }

    public CharacterSO GetCharacterByld(int id)
    {
        if (characterById == null)
        {
            Initialize();
        }
        if (characterById.TryGetValue(id, out CharacterSO item))
            return item;

        return null;
    }
    public CharacterSO GetCharacterByName(string name)
    {
        if (characterByName == null)
        {
            Initialize();
        }
        if (characterByName.TryGetValue(name, out CharacterSO item))
            return item;
        return null;
    }

    public List<CharacterSO> GetCharacterByType(charactersType type)
    {
        return structures.FindAll(item => item.charactersType == type);
    }
}

