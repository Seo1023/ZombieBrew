using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static StructuresType;

[CreateAssetMenu(fileName = "New Structure", menuName = "Inventory/structure")]

public class StructureSO : ScriptableObject
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string Category;
    public structuresType structurestype;
    public int level;

    public override string ToString()
    {
        return $"[{id}] {name} ({Category}) - ·¹º§ : {level}";
    }
    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? name : nameEng; }
    }
}
