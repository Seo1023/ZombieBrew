using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/ExpBonus")]
public class ExpBonusSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        float bonus = GetLevelData(level).effectValue;
        GameManager.Instance.expBonusPercent = bonus;

        Debug.Log($"[노련함] 경험치 보너스 {bonus}% 적용됨");
    }
}
