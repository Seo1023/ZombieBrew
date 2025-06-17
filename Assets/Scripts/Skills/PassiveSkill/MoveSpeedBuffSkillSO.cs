using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/MoveSpeedBuff")]
public class MoveSpeedBuffSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        float bonus = GetLevelData(level).effectValue;
        var stats = caster.GetComponent<PlayerMove>();
        if (stats != null)
        {
            stats.moveSpeedBonusPercent = bonus;
        }

        Debug.Log($"[전투준비태세] 이동속도 보너스 {bonus}%");
    }
}
