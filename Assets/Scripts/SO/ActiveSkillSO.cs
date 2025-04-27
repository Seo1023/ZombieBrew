using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkill", menuName = "ActiveSkills/ActiveSkillSO")]
public class ActiveSkillSO : ScriptableObject
{
    public int id;
    public string skillName;
    public string description;
    public float cooldownTime;
    public float damage;
    public float range;
    public Sprite icon;
    public string iconpath;
    public SkillType skillType;

    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff
    }
}
