using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisplaysType;

[CreateAssetMenu(fileName = "New Display", menuName = "Inventory/display")]

public class DisplaySO : ScriptableObject
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    public displaysType displaysType;
    public string interaction;

    public override string ToString()
    {
        return $"[{id}] {name} ({type})";
    }
    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? name : nameEng; }
    }
}
