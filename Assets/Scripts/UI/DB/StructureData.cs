using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructuresType;

[SerializeField]
public class StructureData
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string Category;
    [NonSerialized]
    public structuresType structurestype;
    public int level;

    public void InitalizEnums()
    {
        if(Enum.TryParse(Category, out structuresType parsedType))
        {
            structurestype = parsedType;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 아이템 타입 : {Category}");
            structurestype = structuresType.Storage;
        }
    }
}
