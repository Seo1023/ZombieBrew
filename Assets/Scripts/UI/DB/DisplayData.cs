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
            Debug.LogError($"������ '{name}'�� ��ȿ���� ���� ������ Ÿ�� : {type}");
            displaysType = displaysType.UI;
        }
    }
}
