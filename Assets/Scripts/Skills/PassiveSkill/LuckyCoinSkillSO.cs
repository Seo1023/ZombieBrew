using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/LuckyCoin")]
public class LuckyCoinSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        // �� ��ų�� �ߵ����� �ƴ�. ���°� ���޸�.
    }

    public float GetEvasionChance(int level)
    {
        return GetLevelData(level).effectValue;
    }
}
