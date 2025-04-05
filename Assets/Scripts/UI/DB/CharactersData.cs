using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharactersType;

[SerializeField]
public class CharactersData : MonoBehaviour
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    [NonSerialized]
    public charactersType charactersType;
    public int? hp;
    public int speed;
    public int? level;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out charactersType Type))
        {
            charactersType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 아이템 타입 : {type}");
            charactersType = charactersType.Character;
        }
    }
}
