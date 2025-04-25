using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkill", menuName = "ActiveSkills/ActiveSkillSO")]
public class ActiveSkillSO : ScriptableObject
{
    public string skillName;
    public string description;
    public float cooldownTime;
    public float damage;
    public float range;

    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff
    }
}
