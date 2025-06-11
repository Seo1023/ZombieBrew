using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/MoveSpeedBuff")]
public class MoveSpeedBuffSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        float bonusPercent = GetLevelData(level).effectValue;

        var stats = caster.GetComponent<PlayerMove>();

        if(stats != null)
        {
            stats.moveSpeedBonusPercent = bonusPercent;
        }
    }
}
