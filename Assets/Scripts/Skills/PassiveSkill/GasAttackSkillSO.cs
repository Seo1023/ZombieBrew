using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/GasAttack")]
public class GasAttackSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        var data = GetLevelData(level);

        // 플레이어 캐릭터 내부의 "GasArea" 오브젝트 찾기
        Transform gasArea = caster.transform.Find("GasArea");
        if (gasArea == null)
        {
            Debug.LogWarning("[GasAttackSO] GasArea 오브젝트를 찾을 수 없습니다.");
            return;
        }

        // 오브젝트 활성화
        gasArea.gameObject.SetActive(true);

        // 트리거 컴포넌트 설정
        var trigger = gasArea.GetComponent<GasAttackTrigger>();
        if (trigger == null)
        {
            Debug.LogWarning("[GasAttackSO] GasAttackTrigger 스크립트가 없습니다.");
            return;
        }

        // 데미지와 범위 적용
        trigger.SetDamage((int)data.damage);
        trigger.SetRadius(data.range);

        Debug.Log($"[GasAttackSO] 스킬 '{skillName}' Lv.{level} 활성화됨! 데미지 {data.damage}, 범위 {data.range}");
    }
}
