using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PassiveSkill", menuName = "Skills/PassiveSkill")]
public class PassiveSkillSO : ScriptableObject
{
    public int id;
    public string skillName;
    public Sprite icon;
    [TextArea] public string description;

    public PassiveSkillType passiveSkillType;
    public float tickInterval = 1f;

    public List<SkillLevelData> levelDataList;

    public SkillLevelData GetLevelData(int level)
    {
        level = Mathf.Clamp(level, 1, levelDataList.Count);
        return levelDataList[level - 1];
    }

    public virtual void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);
        Debug.Log($"[PassiveSkill] {skillName} Lv.{level} 발동! 데미지: {data.damage}, 범위: {data.range}");
    }

    public enum PassiveSkillType
    {
        Area,
        MouseClick,
        Target,
        Buff,
        Spawn
    }
}

[System.Serializable]
public class SkillLevelData
{
    public int damage;
    public float range;
    public float cooldown;
    public float effectValue;
    public float duration;
    public float chance;
}
