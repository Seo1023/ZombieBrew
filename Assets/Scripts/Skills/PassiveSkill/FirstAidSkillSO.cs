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

        stats.Heal((int)data.effectValue);
        Debug.Log($"[응급처치] Lv.{level} > {data.effectValue} 회복");
    }
}
