using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/LuckyCoin")]
public class LuckyCoinSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        // 이 스킬은 발동형이 아님. 상태값 전달만.
    }

    public float GetEvasionChance(int level)
    {
        return GetLevelData(level).effectValue;
    }
}
