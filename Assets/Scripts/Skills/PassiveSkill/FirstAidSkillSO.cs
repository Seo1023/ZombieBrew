using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Passive/FirstAid")]
public class FirstAidSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);

        var stats = caster.GetComponent<ChracterStats>();
        if (stats == null) return;

        PassiveSkill mySkill = stats.GetComponent<PassiveSkillManager>()?.GetSkill(this);
        if (mySkill == null) return;

        if (Time.time - mySkill.lastActivationTime < data.cooldown) return;

        stats.Heal((int)data.effectValue);
        mySkill.lastActivationTime = Time.time;
    }
}
