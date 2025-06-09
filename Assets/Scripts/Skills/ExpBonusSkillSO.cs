using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBonusSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        float bonus = GetLevelData(level).effectValue;
        GameManager.Instance.expBonusPercent = bonus;

        Debug.Log($"[³ë·ÃÇÔ] °æÇèÄ¡ È¹µæ·® + {bonus}% Àû¿ëµÊ");
    }
}
