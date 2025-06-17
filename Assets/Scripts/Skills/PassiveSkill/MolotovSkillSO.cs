using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/Molotov")]
public class MolotovSkillSO : PassiveSkillSO
{
    public GameObject fireAreaPrefab;

    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        var data = GetLevelData(level);
        Vector3 spawnPos = caster.transform.position + new Vector3(
            Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));

        var fireArea = Instantiate(fireAreaPrefab, spawnPos, Quaternion.identity);
        var effect = fireArea.GetComponent<MolotovEffect>();
        if (effect != null)
        {
            effect.Initialize(data.damage, data.effectValue); // 데미지, 지속시간 등
        }

        Debug.Log($"[화염병] Lv.{level} 위치 {spawnPos}");
    }
}
