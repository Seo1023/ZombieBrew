using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkill", menuName = "PassiveSkill/PassiveSkillSO")]
public class PassiveSkillSO : ScriptableObject
{
    public int id;
    public string skillName;
    public string description;
    public int cooldownTime;
    public int damage;
    public int range;
    public int effectValue;
    public Sprite icon;
    public string iconpath;
    public PassiveSkillType passiveSkillType;

    public enum PassiveSkillType
    {
        Area,
        MouseClick,
        Target,
        Buff,
        Spawn
    }

    public virtual void Activate(GameObject caster, Vector3 targetPosition)
    {
        Debug.Log($"스킬 발동 : {skillName} - 타겟위치 : {targetPosition}");
    }
}
