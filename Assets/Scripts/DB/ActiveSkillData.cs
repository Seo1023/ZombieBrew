using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActiveSkillSO;

public class ActiveSkillData : MonoBehaviour
{
    public int id;
    public string skillName;
    public string description;
    public string type;
    public int cooldownTime;
    public int damage;
    public int range;
    public int effectValue;
    public string iconpath;
    public ActiveSkillType activeSkillType;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out ActiveSkillType Type))
        {
            activeSkillType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 타입 : {type}");
        }
    }
}
