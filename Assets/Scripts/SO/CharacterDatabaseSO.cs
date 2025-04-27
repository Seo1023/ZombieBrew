using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterSO;
using static WeaponSO;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Inventory/Database")]
public class CharacterDatabaseSO : ScriptableObject
{
    public List<CharacterSO> characters = new List<CharacterSO>();

    private Dictionary<int, CharacterSO> characterById;
    private Dictionary<string, CharacterSO> characterByName;

    public void Initialize()
    {
        characterById = new Dictionary<int, CharacterSO>();
        characterByName = new Dictionary<string, CharacterSO>();

        foreach (var character in characters)
        {
            characterById[character.id] = character;
            characterByName[character.name] = character;
        }
    }

    public CharacterSO GetWeaponById(int id)
    {
        if (characterById == null)
        {
            Initialize();
        }
        if (characterById.TryGetValue(id, out CharacterSO character))
        {
            return character;
        }
        return null;
    }

    public CharacterSO GetWeaponByName(string name)
    {
        if (characterByName == null)
        {
            Initialize();
        }
        if (characterByName.TryGetValue(name, out CharacterSO character))
        {
            return character;
        }
        return null;
    }

    public List<CharacterSO> GetWEaponByType(CharacterType type)
    {
        return characters.FindAll(character => character.characterType == type);
    }
}
