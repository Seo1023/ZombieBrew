using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisplaysType;

[SerializeField]
public class DisplayData : MonoBehaviour
{
    public int id;
    public string name;
    public string nameEng;
    public string description;
    public string type;
    [NonSerialized]
    public displaysType displaysType;
    public string interaction;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out displaysType Type))
        {
            displaysType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 아이템 타입 : {type}");
            displaysType = displaysType.UI;
        }
    }
}
