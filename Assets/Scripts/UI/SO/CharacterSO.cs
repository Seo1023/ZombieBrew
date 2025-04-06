using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharactersType;

[CreateAssetMenu(fileName = "New Character", menuName = "Inventory/character")]

public class CharacterSO : ScriptableObject
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    public charactersType charactersType;
    public int? hp;
    public int speed;
    public int? level;

    public override string ToString()
    {
        return $"[{id}] {name} ({type}) - ·¹º§ : {level}";
    }
    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? name : nameEng; }
    }
}
