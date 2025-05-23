using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillExecutor
{
    public static void Execute(ActiveSkillSO skill, GameObject caster, Vector3 target)
    {
        switch (skill.id)
        {
            case 1001:
                AirBomb.Execute(skill, caster, target);
                break;

            default:
                Debug.Log($"[��ų] �� �� ���� ��ų ID : {skill.id}");
                break;
        }
    }
}