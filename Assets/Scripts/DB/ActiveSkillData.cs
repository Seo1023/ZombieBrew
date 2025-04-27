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
    public float cooldownTime;
    public float damage;
    public float range;
    public string iconpath;
    public SkillType skillType;

    public void InitalizEnums()
    {
        if (Enum.TryParse(type, out SkillType Type))
        {
            skillType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 타입 : {type}");
        }
    }
}
