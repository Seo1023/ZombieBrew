using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/GasAttack")]
public class GasAttackSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        var data = GetLevelData(level);

        // �÷��̾� ĳ���� ������ "GasArea" ������Ʈ ã��
        Transform gasArea = caster.transform.Find("GasArea");
        if (gasArea == null)
        {
            Debug.LogWarning("[GasAttackSO] GasArea ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ������Ʈ Ȱ��ȭ
        gasArea.gameObject.SetActive(true);

        // Ʈ���� ������Ʈ ����
        var trigger = gasArea.GetComponent<GasAttackTrigger>();
        if (trigger == null)
        {
            Debug.LogWarning("[GasAttackSO] GasAttackTrigger ��ũ��Ʈ�� �����ϴ�.");
            return;
        }

        // �������� ���� ����
        trigger.SetDamage((int)data.damage);
        trigger.SetRadius(data.range);

        Debug.Log($"[GasAttackSO] ��ų '{skillName}' Lv.{level} Ȱ��ȭ��! ������ {data.damage}, ���� {data.range}");
    }
}
