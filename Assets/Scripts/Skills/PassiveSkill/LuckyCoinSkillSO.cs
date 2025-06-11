using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/LuckyCoin")]
public class LuckyCoinSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        base.Activate(caster, targetPosition, level);
    }

    public float GetEvadChance(int level)
    {
        return GetLevelData(level).effectValue;
    }
}
